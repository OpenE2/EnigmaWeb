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
    public class WakeUpCommand : EnigmaCommand<IWakeUpCommand, IResponse<IWakeUpCommand>>, IWakeUpCommand
    {
        private readonly IResponseParser<IWakeUpCommand, IResponse<IWakeUpCommand>> _parser;

        public WakeUpCommand([NotNull] IFactory factory)
            : base(factory)
        {
            _parser = Factory.WakeUpParser();
        }

        public async Task<IResponse<IWakeUpCommand>> ExecuteAsync(IProfile profile, CancellationToken token)
        {
            string url = profile.Enigma == EnigmaType.Enigma1 ? @"cgi-bin/admin?command=wakeup&requester=webif" : @"web/powerstate?newstate=4";
            return await base.ExecuteAsync(profile, url, _parser, token);
        }
    }
}