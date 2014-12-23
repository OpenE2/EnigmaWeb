namespace Krkadoni.Enigma
{
    public interface IE1Signal : ISignal
    {
        bool Lock { get; set; }

        bool Sync { get; set; }

        decimal CalculatedDb { get; }
    }
}