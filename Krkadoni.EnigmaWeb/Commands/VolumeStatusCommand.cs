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
    public class VolumeStatusCommand : EnigmaCommand<IVolumeStatusCommand, IVolumeStatusResponse>, IVolumeStatusCommand
    {
        private readonly IResponseParser<IVolumeStatusCommand, IVolumeStatusResponse> _parser;

        public VolumeStatusCommand([NotNull] IFactory factory)
            : base(factory)
        {
            _parser = Factory.VolumeStatusParser();
        }

        public async Task<IVolumeStatusResponse> ExecuteAsync(IProfile profile, CancellationToken token)
        {
            string url = profile.Enigma == EnigmaType.Enigma1 ? @"data" : "web/vol";
            return await base.ExecuteAsync(profile, url, _parser, token);
        }
    }
}