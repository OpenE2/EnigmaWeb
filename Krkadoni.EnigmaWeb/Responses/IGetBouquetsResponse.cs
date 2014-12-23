using System.Collections.Generic;
using Krkadoni.Enigma.Commands;

namespace Krkadoni.Enigma.Responses
{
    public interface IGetBouquetsResponse : IResponse<IGetBouquetsCommand>
    {
        IList<IBouquetItemBouquet> Bouquets { get; }
    }
}