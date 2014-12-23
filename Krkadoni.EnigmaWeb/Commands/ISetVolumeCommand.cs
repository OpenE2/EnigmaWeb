using System.Threading;
using System.Threading.Tasks;
using Krkadoni.Enigma.Responses;

namespace Krkadoni.Enigma.Commands
{
    public interface ISetVolumeCommand : ICommand
    {
        Task<IResponse<ISetVolumeCommand>> ExecuteAsync(IProfile profile, CancellationToken token, int level);
    }
}