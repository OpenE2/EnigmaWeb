using System.Collections.Generic;
using Krkadoni.Enigma.Commands;

namespace Krkadoni.Enigma.Responses
{
    public interface IGetBouquetItemsResponse : IResponse<IGetBouquetItemsCommand>
    {
        IList<IBouquetItem> Items { get; }
    }
}