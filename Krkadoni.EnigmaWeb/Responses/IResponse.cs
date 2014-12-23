using Krkadoni.Enigma.Commands;

namespace Krkadoni.Enigma.Responses
{
    public interface IResponse<TCommand> where TCommand : ICommand
    {
    }
}