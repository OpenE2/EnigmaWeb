namespace Krkadoni.Enigma.Responses
{
    public class PowerStateResponse : IPowerStateResponse
    {
        public PowerStateResponse(bool standby)
        {
            Standby = standby;
        }

        public bool Standby { get; private set; }
    }
}