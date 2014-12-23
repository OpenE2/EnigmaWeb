using Krkadoni.Enigma.Commands;

namespace Krkadoni.Enigma.Responses
{
    public interface IGetCurrentServiceResponse : IResponse<IGetCurrentServiceCommand>
    {
        IBouquetItemService CurrentService { get; }
    }
}