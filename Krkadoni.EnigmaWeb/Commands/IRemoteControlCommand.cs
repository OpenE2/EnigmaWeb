using System.Threading;
using System.Threading.Tasks;
using Krkadoni.Enigma.Enums;
using Krkadoni.Enigma.Responses;

namespace Krkadoni.Enigma.Commands
{
    public interface IRemoteControlCommand : ICommand
    {
        Task<IResponse<IRemoteControlCommand>> ExecuteAsync(IProfile profile, CancellationToken token, RemoteControlCode code);
    }
}