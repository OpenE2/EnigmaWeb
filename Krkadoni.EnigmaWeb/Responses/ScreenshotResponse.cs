namespace Krkadoni.Enigma.Responses
{
    public class ScreenshotResponse : IScreenshotResponse
    {
        public ScreenshotResponse(byte[] screenshot)
        {
            Screenshot = screenshot;
        }

        public byte[] Screenshot { get; private set; }
    }
}