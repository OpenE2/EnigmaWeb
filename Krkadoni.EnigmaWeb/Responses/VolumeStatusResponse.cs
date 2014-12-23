using System;
using Krkadoni.Enigma.Properties;

namespace Krkadoni.Enigma.Responses
{
    public class VolumeStatusResponse : IVolumeStatusResponse
    {
        public VolumeStatusResponse([NotNull] IVolumeStatus status)
        {
            if (status == null) throw new ArgumentNullException("status");
            Status = status;
        }

        public IVolumeStatus Status { get; private set; }
    }
}