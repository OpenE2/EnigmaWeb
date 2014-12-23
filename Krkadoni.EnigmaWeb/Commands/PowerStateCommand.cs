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
    public class PowerStateCommand : EnigmaCommand<IPowerStateCommand, IPowerStateResponse>, IPowerStateCommand
    {
        private readonly IResponseParser<IPowerStateCommand, IPowerStateResponse> _parser;

        public PowerStateCommand([NotNull] IFactory factory)
            : base(factory)
        {
            _parser = Factory.PowerStateParser();
        }

        public async Task<IPowerStateResponse> ExecuteAsync(IProfile profile, CancellationToken token)
        {
            string url = profile.Enigma == EnigmaType.Enigma1 ? @"data" : @"web/powerstate";
            return await base.ExecuteAsync(profile, url, _parser, token);
        }
    }
}