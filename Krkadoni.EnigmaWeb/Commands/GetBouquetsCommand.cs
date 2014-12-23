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
    public class GetBouquetsCommand : EnigmaCommand<IGetBouquetsCommand, IGetBouquetsResponse>, IGetBouquetsCommand
    {
        private readonly IResponseParser<IGetBouquetsCommand, IGetBouquetsResponse> _parser;

        public GetBouquetsCommand([NotNull] IFactory factory)
            : base(factory)
        {
            _parser = Factory.GetBouquetsParser();
        }

        public async Task<IGetBouquetsResponse> ExecuteAsync(IProfile profile, CancellationToken token)
        {
            string url = profile.Enigma == EnigmaType.Enigma1 ? @"cgi-bin/getServices?ref=4097:7:0:6:0:0:0:0:0:0:" : @"web/getservices";
            return await base.ExecuteAsync(profile, url, _parser, token);
        }
    }
}