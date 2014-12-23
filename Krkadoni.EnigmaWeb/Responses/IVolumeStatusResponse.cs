using Krkadoni.Enigma.Commands;

namespace Krkadoni.Enigma.Responses
{
    public interface IVolumeStatusResponse : IResponse<IVolumeStatusCommand>
    {
        IVolumeStatus Status { get; }
    }
}