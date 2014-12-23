using System.Collections.Generic;

namespace Krkadoni.Enigma.Responses
{
    public class GetBouquetsResponse : IGetBouquetsResponse
    {
        public GetBouquetsResponse(IList<IBouquetItemBouquet> bouquets)
        {
            Bouquets = bouquets;
        }

        public GetBouquetsResponse()
        {
            Bouquets = new List<IBouquetItemBouquet>();
        }

        public IList<IBouquetItemBouquet> Bouquets { get; private set; }
    }
}