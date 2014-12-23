using System.Threading;
using System.Threading.Tasks;
using Krkadoni.Enigma.Responses;

namespace Krkadoni.Enigma.Commands
{
    public interface IZapCommand : ICommand
    {
        Task<IResponse<IZapCommand>> ExecuteAsync(IProfile profile, CancellationToken token, IBouquetItemService service);
    }
}