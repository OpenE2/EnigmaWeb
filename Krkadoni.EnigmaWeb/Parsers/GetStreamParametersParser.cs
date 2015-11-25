using System;
using System.Threading.Tasks;
using Krkadoni.Enigma.Commands;
using Krkadoni.Enigma.Enums;
using Krkadoni.Enigma.Properties;
using Krkadoni.Enigma.Responses;
using System.Linq;


namespace Krkadoni.Enigma.Parsers
{
    public class GetStreamParametersParser : IResponseParser<IGetStreamParametersCommand, IGetStreamParametersResponse>
    {
        private readonly IFactory _factory;
        private readonly ILog _log;

        public GetStreamParametersParser([NotNull] IFactory factory)
        {
            if (factory == null) throw new ArgumentNullException("factory");
            _factory = factory;
            _log = factory.Log();
        }

        public async Task<IGetStreamParametersResponse> ParseAsync(string response, EnigmaType enigmaType)
        {
            try
            {
                if (enigmaType == EnigmaType.Enigma1)
                    return await Task.Run(() => ParseE1(response));

                return await Task.Run(() => ParseE2(response));
            }
            catch (Exception ex)
            {
                if (ex is KnownException || ex is OperationCanceledException)
                    throw;
                throw new ParsingException(string.Format("Failed to parse response{0}{1}", Environment.NewLine, response), ex);
            }
        }

        protected virtual IGetStreamParametersResponse ParseE1(string response)
        {
            return ParseE2(response);
        }
            
        protected virtual IGetStreamParametersResponse ParseE2(string response)
        {
            const char LF = '\n';
            var spr = _factory.GetStreamParametersResponse(response);
            spr.m3uFileContent = response;

            var lines = response.Split(LF);
            if (lines != null && lines.Any() )
            {
                var link = lines.FirstOrDefault(x => x.ToLower().StartsWith("http"));
                if (!string.IsNullOrEmpty(link))
                    spr.StreamUrl = link.Trim();           
            }
            return spr;
        }

    }
}