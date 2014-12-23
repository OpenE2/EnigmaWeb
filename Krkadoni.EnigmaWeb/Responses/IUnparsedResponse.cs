using Krkadoni.Enigma.Commands;

namespace Krkadoni.Enigma.Responses
{
    public interface IUnparsedResponse<TCommand> : IResponse<TCommand> where TCommand : ICommand
    {
        string Response { get; }
    }
}