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
    public class PowerStateParser : IResponseParser<IPowerStateCommand, IPowerStateResponse>
    {
        private readonly IFactory _factory;
        private readonly ILog _log;

        public PowerStateParser([NotNull] IFactory factory)
        {
            if (factory == null) throw new ArgumentNullException("factory");
            _factory = factory;
            _log = factory.Log();
        }

        public async Task<IPowerStateResponse> ParseAsync(string response, EnigmaType enigmaType)
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

        protected virtual IPowerStateResponse ParseE1(string response)
        {
            bool standby;
            const string searchFor = @"var standby = ";
            string value = response.Substring(response.IndexOf(searchFor, StringComparison.Ordinal) + searchFor.Length);
            value = value.Substring(0, value.IndexOf(";", StringComparison.Ordinal)).Trim();
            if (!bool.TryParse(value, out standby))
                throw new ParsingException(string.Format("Enigma1 powerstate parsing failed. Unable to convert {0} to boolean value.", value));
            return _factory.PowerStateResponse(standby);
        }

        protected virtual IPowerStateResponse ParseE2(string response)
        {
            response = Helpers.SanitizeXmlString(response);

            using (XmlReader reader = XmlReader.Create(new StringReader(response)))
            {
                bool canRead = reader.Read();
                while (canRead)
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name.ToLower())
                        {
                            case "e2instandby":
                                reader.Read();
                                bool standby;
                                if (!bool.TryParse(reader.Value.Trim(), out standby))
                                    throw new ParsingException(string.Format("Enigma2 powerstate parsing failed. Unable to convert {0} to boolean value.",
                                        reader.Value));
                                return _factory.PowerStateResponse(standby);
                        }
                    }
                    canRead = reader.Read();
                }
            }
            throw new ParsingException("Failed to parse Enigma2 powerstate. Xml tag <e2instandby> not found!");
        }
    }
}