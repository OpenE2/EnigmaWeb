using System.Threading;
using System.Threading.Tasks;
using Krkadoni.Enigma.Responses;

namespace Krkadoni.Enigma.Commands
{
    public interface ISignalCommand : ICommand
    {
        Task<ISignalResponse> ExecuteAsync(IProfile profile, CancellationToken token);
    }
}