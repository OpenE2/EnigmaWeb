using System.Threading;
using System.Threading.Tasks;
using Krkadoni.Enigma.Responses;

namespace Krkadoni.Enigma.Commands
{
    public interface IGetCurrentServiceCommand : ICommand
    {
        Task<IGetCurrentServiceResponse> ExecuteAsync(IProfile profile, CancellationToken token);
    }
}