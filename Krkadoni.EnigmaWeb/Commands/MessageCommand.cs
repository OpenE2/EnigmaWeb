using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Krkadoni.Enigma.Enums;
using Krkadoni.Enigma.Parsers;
using Krkadoni.Enigma.Properties;
using Krkadoni.Enigma.Responses;

namespace Krkadoni.Enigma.Commands
{
    [ComVisible(false)]
    public class MessageCommand : EnigmaCommand<IMessageCommand, IResponse<IMessageCommand>>, IMessageCommand
    {
        private readonly IResponseParser<IMessageCommand, IResponse<IMessageCommand>> _parser;

        public MessageCommand([NotNull] IFactory factory)
            : base(factory)
        {
            _parser = Factory.MessageParser();
        }

        public async Task<IResponse<IMessageCommand>> ExecuteAsync(IProfile profile, CancellationToken token, MessageType type, string message, int timeout)
        {
            string url;
            if (profile.Enigma == EnigmaType.Enigma1)
            {
                string caption;
                switch (type)
                {
                    case MessageType.Info:
                        caption = "Info";
                        break;
                    case MessageType.Warning:
                        caption = "Warning";
                        break;
                    case MessageType.Question:
                        caption = "Question";
                        break;
                    default:
                        caption = "Message";
                        break;
                }
                url = string.Format("cgi-bin/xmessage?caption={0}&timeout={1}&body={2}", caption, timeout, Uri.EscapeUriString(message).Replace(" ", "+"));
            }
            else
            {
                url = string.Format("web/message?text={0}&type={1}&timeout={2}", Uri.EscapeUriString(message), (int) type, timeout);
            }
            return await base.ExecuteAsync(profile, url, _parser, token);
        }
    }
}