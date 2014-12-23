using Krkadoni.Enigma.Commands;

namespace Krkadoni.Enigma.Responses
{
    public interface IScreenshotResponse : IResponse<IScreenshotCommand>
    {
        byte[] Screenshot { get; }
    }
}