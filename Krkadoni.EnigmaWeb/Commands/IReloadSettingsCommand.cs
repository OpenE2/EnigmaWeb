using System.Threading;
using System.Threading.Tasks;
using Krkadoni.Enigma.Enums;
using Krkadoni.Enigma.Responses;

namespace Krkadoni.Enigma.Commands
{
    public interface IReloadSettingsCommand : ICommand
    {
        Task<IResponse<IReloadSettingsCommand>> ExecuteAsync(IProfile profile, CancellationToken token, ReloadSettingsType type);
    }
}