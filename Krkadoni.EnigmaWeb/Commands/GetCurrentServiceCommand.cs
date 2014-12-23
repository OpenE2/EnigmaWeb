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
    public class GetCurrentServiceCommand : EnigmaCommand<IGetCurrentServiceCommand, IGetCurrentServiceResponse>, IGetCurrentServiceCommand
    {
        private readonly IResponseParser<IGetCurrentServiceCommand, IGetCurrentServiceResponse> _parser;

        public GetCurrentServiceCommand([NotNull] IFactory factory) : base(factory)
        {
            _parser = Factory.GetCurrentServiceParser();
        }

        public async Task<IGetCurrentServiceResponse> ExecuteAsync(IProfile profile, CancellationToken token)
        {
            string url = profile.Enigma == EnigmaType.Enigma1 ? @"data" : @"web/getcurrent";
            return await base.ExecuteAsync(profile, url, _parser, token);
        }
    }
}