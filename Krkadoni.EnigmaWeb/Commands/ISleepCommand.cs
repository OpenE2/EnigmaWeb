using System.Threading;
using System.Threading.Tasks;
using Krkadoni.Enigma.Responses;

namespace Krkadoni.Enigma.Commands
{
    public interface ISleepCommand : ICommand
    {
        Task<IResponse<ISleepCommand>> ExecuteAsync(IProfile profile, CancellationToken token);
    }
}