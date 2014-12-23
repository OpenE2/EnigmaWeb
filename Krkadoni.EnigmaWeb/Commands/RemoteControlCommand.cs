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
    public class RemoteControlCommand : EnigmaCommand<IRemoteControlCommand, IResponse<IRemoteControlCommand>>, IRemoteControlCommand
    {
        private readonly IResponseParser<IRemoteControlCommand, IResponse<IRemoteControlCommand>> _parser;

        public RemoteControlCommand([NotNull] IFactory factory)
            : base(factory)
        {
            _parser = Factory.RemoteControlParser();
        }

        public async Task<IResponse<IRemoteControlCommand>> ExecuteAsync(IProfile profile, CancellationToken token, RemoteControlCode code)
        {
            string url = profile.Enigma == EnigmaType.Enigma1 ? "cgi-bin/rc?" : "web/remotecontrol?command=";
            url = url + (int) code;
            return await base.ExecuteAsync(profile, url, _parser, token);
        }
    }
}