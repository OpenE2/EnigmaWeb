namespace Krkadoni.Enigma
{
    public interface IVolumeStatus
    {
        int Level { get; set; }
        bool Mute { get; set; }
    }
}