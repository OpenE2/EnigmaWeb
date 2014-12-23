using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Krkadoni.Enigma.Enums;
using Krkadoni.Enigma.Parsers;
using Krkadoni.Enigma.Properties;
using Krkadoni.Enigma.Responses;

namespace Krkadoni.Enigma.Commands
{
    [ComVisible(false)]
    public class GetBouquetItemsCommand : EnigmaCommand<IGetBouquetItemsCommand, IGetBouquetItemsResponse>, IGetBouquetItemsCommand
    {
        private readonly IResponseParser<IGetBouquetItemsCommand, IGetBouquetItemsResponse> _parser;

        public GetBouquetItemsCommand([NotNull] IFactory factory)
            : base(factory)
        {
            _parser = Factory.GetBouquetItemsParser();
        }

        public async Task<IGetBouquetItemsResponse> ExecuteAsync(IProfile profile, CancellationToken token, [NotNull] IBouquetItemBouquet bouquet)
        {
            if (bouquet == null) throw new ArgumentNullException("bouquet");
            string url = profile.Enigma == EnigmaType.Enigma1 ? @"cgi-bin/getServices?ref=" : @"web/getservices?sRef=";
            url = url + bouquet.Reference;
            return await base.ExecuteAsync(profile, url, _parser, token);
        }
    }
}