using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using Krkadoni.Enigma.Commands;
using Krkadoni.Enigma.Enums;
using Krkadoni.Enigma.Properties;
using Krkadoni.Enigma.Responses;

namespace Krkadoni.Enigma.Parsers
{
    public class GetBouquetsParser : IResponseParser<IGetBouquetsCommand, IGetBouquetsResponse>
    {
        private readonly IFactory _factory;
        private readonly ILog _log;

        public GetBouquetsParser([NotNull] IFactory factory)
        {
            if (factory == null) throw new ArgumentNullException("factory");
            _factory = factory;
            _log = factory.Log();
        }

        public async Task<IGetBouquetsResponse> ParseAsync(string response, EnigmaType enigmaType)
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

        protected virtual IGetBouquetsResponse ParseE1(string response)
        {
            var bouquets = new List<IBouquetItemBouquet>();
            string[] lines = response.Split(new[] {"\n"}, StringSplitOptions.None);

            for (int i = 0; i <= lines.Length - 2; i++)
            {
                IBouquetItemBouquet bq = _factory.BouquetItemBouquet();
                bq.Reference = lines[i].Substring(0, lines[i].IndexOf(";", StringComparison.Ordinal)).Trim();
                bq.Name = lines[i].Substring(lines[i].IndexOf(";", StringComparison.Ordinal) + 1).Trim();
                if (lines[i].IndexOf(";selected", StringComparison.Ordinal) > -1)
                {
                    bq.Name = bq.Name.Substring(0, bq.Name.IndexOf(";selected", StringComparison.Ordinal));
                }

                bouquets.Add(bq);
            }
            return _factory.GetBouquetsResponse(bouquets);
        }

        protected virtual IGetBouquetsResponse ParseE2(string response)
        {
            response = Helpers.SanitizeXmlString(response);
            var bouquets = new List<IBouquetItemBouquet>();
            using (XmlReader reader = XmlReader.Create(new StringReader(response)))
            {
                bool canRead = reader.Read();
                IBouquetItemBouquet bouquet = null;
                while (canRead)
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name.ToLower())
                        {
                            case "e2service":
                                if (bouquet != null)
                                    bouquets.Add(bouquet);
                                bouquet = _factory.BouquetItemBouquet();
                                break;
                            case "e2servicereference":
                                reader.Read();
                                if (bouquet != null)
                                    bouquet.Reference = reader.Value;
                                break;
                            case "e2servicename":
                                reader.Read();
                                if (bouquet != null)
                                    bouquet.Name = reader.Value;
                                break;
                        }
                    }

                    canRead = reader.Read();
                }
            }
            return _factory.GetBouquetsResponse(bouquets);
        }

        //protected virtual IGetBouquetsResponse ParseE2(string response)
        //{
        //    response = Helpers.SanitizeXmlString(response);
        //    var service = _factory.BouquetItemService();

        //    using (var reader = XmlReader.Create(new StringReader(response)))
        //    {
        //        var canRead = reader.Read();
        //        while (canRead)
        //        {
        //            if (reader.IsStartElement())
        //            {
        //                switch (reader.Name.ToLower())
        //                {
        //                    case "e2servicereference":
        //                        reader.Read();
        //                        service.Reference = reader.Value;
        //                        break;
        //                    case "e2servicename":
        //                        reader.Read();
        //                        service.Name = reader.Value;
        //                        break;
        //                }
        //            }

        //            if (service.Reference != null && service.Name != null)
        //                break;
        //            canRead = reader.Read();
        //        }
        //    }
        //    return _factory.GetCurrentServiceResponse(service);
        //}
    }
}