using System;
using System.Threading;
using System.Threading.Tasks;
using Krkadoni.Enigma.Parsers;
using Krkadoni.Enigma.Properties;
using Krkadoni.Enigma.Responses;

namespace Krkadoni.Enigma.Commands
{
    public class EnigmaCommand<TCommand, TResponse>
        where TCommand : ICommand
        where TResponse : IResponse<TCommand>
    {
        protected readonly IFactory Factory;
        protected readonly IWebRequester Requester;
        protected ILog Log;

        public EnigmaCommand([NotNull] IFactory factory)
        {
            if (factory == null) throw new ArgumentNullException("factory");
            Factory = factory;
            Log = Factory.Log();
            Requester = Factory.WebRequester();
        }

        public virtual async Task<TResponse> ExecuteAsync(
            [NotNull] IProfile profile,
            [NotNull] string url,
            [NotNull] IResponseParser<TCommand, TResponse> parser,
            CancellationToken token)
        {
            if (profile == null) throw new ArgumentNullException("profile");
            if (url == null) throw new ArgumentNullException("url");
            if (parser == null) throw new ArgumentNullException("parser");

            try
            {
                string response = await Requester.GetResponseAsync(url, profile, token);
                if (response == null)
                    return default(TResponse);

                token.ThrowIfCancellationRequested();

                return await parser.ParseAsync(response, profile.Enigma);
            }
            catch (Exception ex)
            {
                if (ex is KnownException || ex is OperationCanceledException)
                    throw;

                throw new CommandException(string.Format("Command failed for profile {0}", profile.Name), ex);
            }
        }
    }
}