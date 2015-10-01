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
    public class GetBouquetItemsParser : IResponseParser<IGetBouquetItemsCommand, IGetBouquetItemsResponse>
    {
        private readonly IFactory _factory;
        private readonly ILog _log;

        public GetBouquetItemsParser([NotNull] IFactory factory)
        {
            if (factory == null) throw new ArgumentNullException("factory");
            _factory = factory;
            _log = factory.Log();
        }

        public async Task<IGetBouquetItemsResponse> ParseAsync(string response, EnigmaType enigmaType)
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

        protected virtual IGetBouquetItemsResponse ParseE1(string response)
        {
            var items = new List<IBouquetItem>();
            string[] lines = response.Split(new[] {"\n"}, StringSplitOptions.None);

            for (int i = 0; i <= lines.Length - 2; i++)
            {
                string reference = lines[i].Substring(0, lines[i].IndexOf(";", StringComparison.Ordinal)).Trim();
                IBouquetItem item = InitializeItem(reference, EnigmaType.Enigma1);
                if (item == null) continue;
                string name = lines[i].Substring(lines[i].IndexOf(";", StringComparison.Ordinal) + 1).Trim();
                if (name.IndexOf(";", StringComparison.Ordinal) > -1)
                    name = name.Substring(0, name.IndexOf(";", StringComparison.Ordinal)).Trim();
                item.Reference = reference;
                item.Name = name;
                items.Add(item);
            }
            return _factory.GetBouquetItemsResponse(items);
        }

        protected virtual IGetBouquetItemsResponse ParseE2(string response)
        {
            response = Helpers.SanitizeXmlString(response);
            var items = new List<IBouquetItem>();
            using (XmlReader reader = XmlReader.Create(new StringReader(response)))
            {
                bool canRead = reader.Read();
                IBouquetItem item = null;
                while (canRead)
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name.ToLower())
                        {
                            case "e2servicereference":
                                reader.Read();

                                if (item != null)
                                    items.Add(item);

                                item = InitializeItem(reader.Value, EnigmaType.Enigma2);

                                if (item != null)
                                    item.Reference = reader.Value;

                                break;
                            case "e2servicename":
                                reader.Read();

                                if (item != null)
                                    item.Name = reader.Value;

                                break;
                        }
                    }

                    canRead = reader.Read();
                }
            }
            return _factory.GetBouquetItemsResponse(items);
        }

        private IBouquetItem InitializeItem(string reference, EnigmaType enigmaType)
        {
            if (String.IsNullOrEmpty(reference))
                return null;

            if (reference.StartsWith("1:0:1"))
            {
                return enigmaType == EnigmaType.Enigma2 ? _factory.BouquetItemService() : _factory.BouquetItemServiceE1();
            }           

            if (reference.StartsWith("1:64"))
            {
                return _factory.BouquetItemMarker();
            }

            return _factory.BouquetItemBouquet();
        }
    }
}