using Krkadoni.Enigma.Commands;

namespace Krkadoni.Enigma.Responses
{
    public interface ISignalResponse : IResponse<ISignalCommand>
    {
        ISignal Signal { get; }
    }
}