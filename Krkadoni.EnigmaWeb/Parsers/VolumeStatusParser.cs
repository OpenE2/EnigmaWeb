using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using Krkadoni.Enigma.Commands;
using Krkadoni.Enigma.Enums;
using Krkadoni.Enigma.Properties;
using Krkadoni.Enigma.Responses;

namespace Krkadoni.Enigma.Parsers
{
    public class VolumeStatusParser : IResponseParser<IVolumeStatusCommand, IVolumeStatusResponse>
    {
        private readonly IFactory _factory;
        private readonly ILog _log;

        public VolumeStatusParser([NotNull] IFactory factory)
        {
            if (factory == null) throw new ArgumentNullException("factory");
            _factory = factory;
            _log = factory.Log();
        }

        public async Task<IVolumeStatusResponse> ParseAsync(string response, EnigmaType enigmaType)
        {
            try
            {
                if (enigmaType == EnigmaType.Enigma1)
                    return await Task.Run(() => ParseE1(response));

                return await Task.Run(() => ParseE2(response));
            }
            catch (Exception ex)
            {
                if (ex is KnownException || ex is OperationCanceledException)
                    throw;
                throw new ParsingException(string.Format("Failed to parse response{0}{1}", Environment.NewLine, response), ex);
            }
        }

        protected virtual IVolumeStatusResponse ParseE1(string response)
        {
            bool mute;
            int current;
            IVolumeStatus status = _factory.VolumeStatus();

            string muteString = GetE1StatusValue(response, @"var mute = ");
            string currentString = GetE1StatusValue(response, @"var volume = ");

            if (!bool.TryParse(muteString, out mute))
                throw new ParsingException(string.Format("Enigma1 volume status parsing failed. Unable to convert {0} to boolean value.", muteString));
            status.Mute = mute;

            if (!int.TryParse(currentString, out current))
                throw new ParsingException(string.Format("Enigma1 volume status parsing failed. Unable to convert {0} to integer value.", currentString));
            status.Mute = mute;

            return _factory.VolumeStatusResponse(status);
        }

        protected virtual string GetE1StatusValue(string response, string searchFor)
        {
            string tmp = response.Substring(response.IndexOf(searchFor, StringComparison.Ordinal) + searchFor.Length);
            return tmp.Substring(0, tmp.IndexOf(";", StringComparison.Ordinal)).Trim();
        }

        protected virtual IVolumeStatusResponse ParseE2(string response)
        {
            response = Helpers.SanitizeXmlString(response);
            IVolumeStatus status = _factory.VolumeStatus();

            using (XmlReader reader = XmlReader.Create(new StringReader(response)))
            {
                bool canRead = reader.Read();
                while (canRead)
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name.ToLower())
                        {
                            case "e2ismuted":
                                reader.Read();
                                bool muted;
                                if (!bool.TryParse(reader.Value.Trim(), out muted))
                                    throw new ParsingException(
                                        string.Format("Enigma2 volume status parsing failed. Unable to convert {0} to boolean value.", reader.Value));
                                status.Mute = muted;
                                break;
                            case "e2current":
                                reader.Read();
                                int current;
                                if (!int.TryParse(reader.Value.Trim(), out current))
                                    throw new ParsingException(
                                        string.Format("Enigma2 volume status parsing failed. Unable to convert {0} to integer value.", reader.Value));
                                status.Level = current;
                                break;
                        }
                    }
                    canRead = reader.Read();
                }
            }
            return _factory.VolumeStatusResponse(status);
        }
    }
}