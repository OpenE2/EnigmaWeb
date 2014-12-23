using System.Threading;
using System.Threading.Tasks;
using Krkadoni.Enigma.Responses;

namespace Krkadoni.Enigma.Commands
{
    public interface IGetBouquetItemsCommand : ICommand
    {
        Task<IGetBouquetItemsResponse> ExecuteAsync(IProfile profile, CancellationToken token, IBouquetItemBouquet bouquet);
    }
}