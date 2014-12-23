using Krkadoni.Enigma.Commands;
using Krkadoni.Enigma.Responses;

namespace Krkadoni.Enigma.Parsers
{
    public interface IUnparsedParser<TCommand> : IResponseParser<TCommand, IResponse<TCommand>> where TCommand : ICommand
    {
    }
}