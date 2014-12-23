using Krkadoni.Enigma.Commands;

namespace Krkadoni.Enigma.Responses
{
    public interface IPowerStateResponse : IResponse<IPowerStateCommand>
    {
        bool Standby { get; }
    }
}