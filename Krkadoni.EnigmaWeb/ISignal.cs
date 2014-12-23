using System.ComponentModel;

namespace Krkadoni.Enigma
{
    public interface ISignal : INotifyPropertyChanged
    {
        int Snr { get; set; }
        int Acg { get; set; }
        int Ber { get; set; }
    }
}