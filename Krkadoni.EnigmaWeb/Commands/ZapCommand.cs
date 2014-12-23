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
    public class ZapCommand : EnigmaCommand<IZapCommand, IResponse<IZapCommand>>, IZapCommand
    {
        private readonly IResponseParser<IZapCommand, IResponse<IZapCommand>> _parser;

        public ZapCommand([NotNull] IFactory factory)
            : base(factory)
        {
            _parser = Factory.ZapParser();
        }

        public async Task<IResponse<IZapCommand>> ExecuteAsync(IProfile profile, CancellationToken token, [NotNull] IBouquetItemService service)
        {
            if (service == null) throw new ArgumentNullException("service");
            string url = profile.Enigma == EnigmaType.Enigma1 ? @"cgi-bin/zapTo?path=" : @"web/zap?sRef=";
            url = url + service.Reference;
            return await base.ExecuteAsync(profile, url, _parser, token);
        }
    }
}