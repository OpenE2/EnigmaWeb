using System;
using System.Threading.Tasks;
using Krkadoni.Enigma.Commands;
using Krkadoni.Enigma.Enums;
using Krkadoni.Enigma.Responses;

namespace Krkadoni.Enigma.Parsers
{
    public class UnparsedParser<TCommand> : IUnparsedParser<TCommand> where TCommand : ICommand
    {
        public async Task<IResponse<TCommand>> ParseAsync(string response, EnigmaType enigmaType)
        {
            try
            {
                return await Task.Run(() => new UnparsedResponse<TCommand>(response));
            }
            catch (Exception ex)
            {
                if (ex is KnownException || ex is OperationCanceledException)
                    throw;
                throw new ParsingException(string.Format("Failed to parse response{0}{1}", Environment.NewLine, response), ex);
            }
        }
    }
}