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
    public class RestartCommand : EnigmaCommand<IRestartCommand, IResponse<IRestartCommand>>, IRestartCommand
    {
        private readonly IResponseParser<IRestartCommand, IResponse<IRestartCommand>> _parser;

        public RestartCommand([NotNull] IFactory factory)
            : base(factory)
        {
            _parser = Factory.RestartParser();
        }

        public async Task<IResponse<IRestartCommand>> ExecuteAsync(IProfile profile, CancellationToken token)
        {
            string url = profile.Enigma == EnigmaType.Enigma1 ? @"cgi-bin/admin?command=restart&requester=webif" : @"web/powerstate?newstate=3";
            return await base.ExecuteAsync(profile, url, _parser, token);
        }
    }
}