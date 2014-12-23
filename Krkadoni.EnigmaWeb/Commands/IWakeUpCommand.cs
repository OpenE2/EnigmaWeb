using System.Threading;
using System.Threading.Tasks;
using Krkadoni.Enigma.Responses;

namespace Krkadoni.Enigma.Commands
{
    public interface IWakeUpCommand : ICommand
    {
        Task<IResponse<IWakeUpCommand>> ExecuteAsync(IProfile profile, CancellationToken token);
    }
}