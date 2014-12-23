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
    public class SetVolumeCommand : EnigmaCommand<ISetVolumeCommand, IResponse<ISetVolumeCommand>>, ISetVolumeCommand
    {
        private readonly IResponseParser<ISetVolumeCommand, IResponse<ISetVolumeCommand>> _parser;

        public SetVolumeCommand([NotNull] IFactory factory)
            : base(factory)
        {
            _parser = Factory.SetVolumeParser();
        }

        public async Task<IResponse<ISetVolumeCommand>> ExecuteAsync(IProfile profile, CancellationToken token, int level)
        {
            if (level > 100 || level < 0)
                throw new ArgumentOutOfRangeException("level");

            string url = profile.Enigma == EnigmaType.Enigma1 ? "/setVolume?volume=" : "web/vol?set=set";
            url = url + level;
            return await base.ExecuteAsync(profile, url, _parser, token);
        }
    }
}