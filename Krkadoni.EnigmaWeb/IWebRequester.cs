using System.Threading;
using System.Threading.Tasks;

namespace Krkadoni.Enigma
{
    public interface IWebRequester
    {
        Task<string> GetResponseAsync(string url, IProfile profile, CancellationToken token);

        Task<byte[]> GetBinaryResponseAsync(string url, IProfile profile, CancellationToken token);
    }
}