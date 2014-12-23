using System.Threading.Tasks;
using Krkadoni.Enigma.Commands;
using Krkadoni.Enigma.Enums;
using Krkadoni.Enigma.Responses;

namespace Krkadoni.Enigma.Parsers
{
    public interface IResponseParser<TCommand, TResult>
        where TCommand : ICommand
        where TResult : IResponse<TCommand>
    {
        Task<TResult> ParseAsync(string response, EnigmaType enigmaType);
    }
}