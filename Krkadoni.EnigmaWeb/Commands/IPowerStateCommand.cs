using System.Threading;
using System.Threading.Tasks;
using Krkadoni.Enigma.Responses;

namespace Krkadoni.Enigma.Commands
{
    public interface IPowerStateCommand : ICommand
    {
        Task<IPowerStateResponse> ExecuteAsync(IProfile profile, CancellationToken token);
    }
}