using System.Collections.Generic;

namespace Krkadoni.Enigma.Responses
{
    public class GetBouquetItemsResponse : IGetBouquetItemsResponse
    {
        public GetBouquetItemsResponse(IList<IBouquetItem> items)
        {
            Items = items;
        }

        public GetBouquetItemsResponse()
        {
            Items = new List<IBouquetItem>();
        }

        public IList<IBouquetItem> Items { get; private set; }
    }
}