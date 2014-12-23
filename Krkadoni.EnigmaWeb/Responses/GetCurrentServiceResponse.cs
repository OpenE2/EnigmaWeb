using Krkadoni.Enigma.Properties;

namespace Krkadoni.Enigma.Responses
{
    public class GetCurrentServiceResponse : IGetCurrentServiceResponse
    {
        public GetCurrentServiceResponse([NotNull] IBouquetItemService currentService)
        {
            CurrentService = currentService;
        }

        public IBouquetItemService CurrentService { get; private set; }
    }
}