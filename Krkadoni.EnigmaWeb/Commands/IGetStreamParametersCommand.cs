using System.Threading.Tasks;
using Krkadoni.Enigma.Responses;
using Krkadoni.Enigma;
using System.Threading;

namespace Krkadoni.Enigma.Commands
{
    public interface IGetStreamParametersCommand : ICommand
    {
        Task<IGetStreamParametersResponse> ExecuteAsync(IProfile profile, IBouquetItemService service, CancellationToken token);
    }
}

