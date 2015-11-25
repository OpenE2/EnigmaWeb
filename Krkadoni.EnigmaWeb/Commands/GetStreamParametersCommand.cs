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
    public class GetStreamParametersCommand : EnigmaCommand<IGetStreamParametersCommand, IGetStreamParametersResponse>, IGetStreamParametersCommand
    {
        private readonly IResponseParser<IGetStreamParametersCommand, IGetStreamParametersResponse> _parser;

        public GetStreamParametersCommand([NotNull] IFactory factory)
            : base(factory)
        {
            _parser = Factory.GetStreamParametersParser();
        }
            
        public async Task<IGetStreamParametersResponse> ExecuteAsync(IProfile profile, IBouquetItemService service, CancellationToken token)
        {
            if (profile == null) throw new ArgumentNullException("profile");
            if (service == null) throw new ArgumentNullException("service");
            string url = profile.Enigma == EnigmaType.Enigma1 ? @"video.m3u?ref=" : @"web/video.m3u?sRef=";
            url = url + service.Reference;
            return await base.ExecuteAsync(profile, url, _parser, token);
        }


    }
}