using System.Threading;
using System.Threading.Tasks;
using Krkadoni.Enigma.Enums;
using Krkadoni.Enigma.Responses;

namespace Krkadoni.Enigma.Commands
{
    public interface IMessageCommand : ICommand
    {
        Task<IResponse<IMessageCommand>> ExecuteAsync(IProfile profile, CancellationToken token, MessageType type, string message, int timeout);
    }
}