namespace Krkadoni.Enigma.Responses
{
    public class SignalResponse : ISignalResponse
    {
        public SignalResponse(ISignal signal)
        {
            Signal = signal;
        }

        public ISignal Signal { get; private set; }
    }
}