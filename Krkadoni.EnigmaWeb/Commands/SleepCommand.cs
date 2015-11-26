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
    public class SleepCommand : EnigmaCommand<ISleepCommand, IResponse<ISleepCommand>>, ISleepCommand
    {
        private readonly IResponseParser<ISleepCommand, IResponse<ISleepCommand>> _parser;

        public SleepCommand([NotNull] IFactory factory)
            : base(factory)
        {
            _parser = Factory.SleepParser();
        }

        public async Task<IResponse<ISleepCommand>> ExecuteAsync(IProfile profile, CancellationToken token)
        {
            string url = profile.Enigma == EnigmaType.Enigma1 ? @"cgi-bin/admin?command=standby&requester=webif" : @"web/powerstate?newstate=5";
            return await base.ExecuteAsync(profile, url, _parser, token);
        }
    }
}