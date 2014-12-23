using Krkadoni.Enigma.Commands;

namespace Krkadoni.Enigma.Responses
{
    public class UnparsedResponse<TCommand> : IUnparsedResponse<TCommand> where TCommand : ICommand
    {
        public UnparsedResponse(string response)
        {
            Response = response;
        }

        public string Response { get; protected set; }
    }
}