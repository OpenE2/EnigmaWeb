namespace Krkadoni.Enigma
{
    public class VolumeStatus : IVolumeStatus
    {
        public int Level { get; set; }

        public bool Mute { get; set; }
    }
}