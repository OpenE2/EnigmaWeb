using System.Threading;
using System.Threading.Tasks;
using Krkadoni.Enigma.Responses;

namespace Krkadoni.Enigma.Commands
{
    public interface IGetBouquetsCommand : ICommand
    {
        Task<IGetBouquetsResponse> ExecuteAsync(IProfile profile, CancellationToken token);
    }
}