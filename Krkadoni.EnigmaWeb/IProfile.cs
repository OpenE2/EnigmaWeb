using Krkadoni.Enigma.Enums;

namespace Krkadoni.Enigma
{
    public interface IProfile
    {
        string Name { get; set; }
        string Username { get; set; }
        string Password { get; set; }
        EnigmaType Enigma { get; set; }
        string Address { get; set; }
        int HttpPort { get; set; }
        bool UseSsl { get; set; }
    }
}