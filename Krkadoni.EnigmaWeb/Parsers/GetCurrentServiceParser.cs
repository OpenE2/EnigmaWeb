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
    public class GetCurrentServiceParser : IResponseParser<IGetCurrentServiceCommand, IGetCurrentServiceResponse>
    {
        private readonly IFactory _factory;
        private readonly ILog _log;

        public GetCurrentServiceParser([NotNull] IFactory factory)
        {
            if (factory == null) throw new ArgumentNullException("factory");
            _factory = factory;
            _log = factory.Log();
        }

        public async Task<IGetCurrentServiceResponse> ParseAsync(string response, EnigmaType enigmaType)
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

        protected virtual IGetCurrentServiceResponse ParseE1(string response)
        {
            IBouquetItemService service = _factory.BouquetItemService();
            service.Name = GetE1StatusValue(response, @"var serviceName = """);
            service.Reference = GetE1StatusValue(response, @"var serviceReference = """);
            return _factory.GetCurrentServiceResponse(service);
        }

        protected virtual string GetE1StatusValue(string response, string searchFor)
        {
            string tmp = response.Substring(response.IndexOf(searchFor, StringComparison.Ordinal) + searchFor.Length);
            return tmp.Substring(0, tmp.IndexOf(";", StringComparison.Ordinal)).Trim();
        }

        protected virtual IGetCurrentServiceResponse ParseE2(string response)
        {
            response = Helpers.SanitizeXmlString(response);
            IBouquetItemService service = _factory.BouquetItemService();

            using (XmlReader reader = XmlReader.Create(new StringReader(response)))
            {
                bool canRead = reader.Read();
                while (canRead)
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name.ToLower())
                        {
                            case "e2servicereference":
                                reader.Read();
                                service.Reference = reader.Value;
                                break;
                            case "e2servicename":
                                reader.Read();
                                service.Name = reader.Value;
                                break;
                        }
                    }

                    if (service.Reference != null && service.Name != null)
                        break;
                    canRead = reader.Read();
                }
            }
            return _factory.GetCurrentServiceResponse(service);
        }
    }
}