using System.Threading;
using System.Threading.Tasks;
using Krkadoni.Enigma.Enums;
using Krkadoni.Enigma.Responses;

namespace Krkadoni.Enigma.Commands
{
    public interface IScreenshotCommand : ICommand
    {
        Task<IScreenshotResponse> ExecuteAsync(IProfile profile, CancellationToken token, ScreenshotType type);
    }
}