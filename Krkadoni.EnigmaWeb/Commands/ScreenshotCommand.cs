using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Krkadoni.Enigma.Enums;
using Krkadoni.Enigma.Properties;
using Krkadoni.Enigma.Responses;

namespace Krkadoni.Enigma.Commands
{
    [ComVisible(false)]
    public class ScreenshotCommand : EnigmaCommand<IScreenshotCommand, IScreenshotResponse>, IScreenshotCommand
    {
        public ScreenshotCommand([NotNull] IFactory factory)
            : base(factory)
        {
        }

        public async Task<IScreenshotResponse> ExecuteAsync([NotNull] IProfile profile, CancellationToken token, ScreenshotType type)
        {
            if (profile == null) throw new ArgumentNullException("profile");

            try
            {
                string url;
                switch (type)
                {
                    case ScreenshotType.All:
                        url = profile.Enigma == EnigmaType.Enigma1
                            ? "body?mode=controlScreenShot&blendtype=2"
                            : "grab?format=jpg&mode=all&filename=/tmp/" + UnixTimeStamp() + ".jpg";
                        break;
                    case ScreenshotType.Picture:
                        url = profile.Enigma == EnigmaType.Enigma1
                            ? "body?mode=controlScreenShot"
                            : "grab?format=jpg&mode=video&v=&filename=/tmp/" + UnixTimeStamp() + ".jpg";
                        break;
                    case ScreenshotType.Osd:
                        url = profile.Enigma == EnigmaType.Enigma1
                            ? "body?mode=controlFBShot"
                            : "grab?format=jpg&mode=osd&o=&n=&filename=/tmp/" + UnixTimeStamp() + ".jpg";
                        break;
                    default:
                        throw new NotSupportedException();
                }

                if (profile.Enigma == EnigmaType.Enigma2)
                    return Factory.ScreenshotResponse(await Requester.GetBinaryResponseAsync(url, profile, token));

                //Enigma 1
                await Requester.GetResponseAsync(url, profile, token);

                switch (type)
                {
                    case ScreenshotType.Osd:
                        url = "root/tmp/osdshot.png";
                        break;
                    default:
                        url = "root/tmp/screenshot.bmp";
                        break;
                }

                string response = await Requester.GetResponseAsync(url, profile, token);
                if (response == null)
                    return null;

                token.ThrowIfCancellationRequested();

                return Factory.ScreenshotResponse(await Requester.GetBinaryResponseAsync(url, profile, token));
            }
            catch (Exception ex)
            {
                if (ex is KnownException || ex is OperationCanceledException)
                    throw;

                throw new CommandException(string.Format("Command failed for profile {0}", profile.Name), ex);
            }
        }

        private static string UnixTimeStamp()
        {
            return (DateTime.UtcNow.Millisecond/1000).ToString();
        }
    }
}