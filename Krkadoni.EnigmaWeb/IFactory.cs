using System.Collections.Generic;
using Krkadoni.Enigma.Commands;
using Krkadoni.Enigma.Parsers;
using Krkadoni.Enigma.Responses;

namespace Krkadoni.Enigma
{
    public interface IFactory
    {
        ILog Log();

        IGetCurrentServiceCommand GetCurrentServiceCommand();

        IGetCurrentServiceResponse GetCurrentServiceResponse(IBouquetItemService service);

        IGetCurrentServiceResponse GetCurrentServiceResponse();

        IResponseParser<IGetCurrentServiceCommand, IGetCurrentServiceResponse> GetCurrentServiceParser();

        IBouquetItemService BouquetItemService();

        IBouquetItemServiceE1 BouquetItemServiceE1();

        IBouquetItemMarker BouquetItemMarker();

        IWebRequester WebRequester();

        IWakeUpCommand WakeUpCommand();

        IResponse<IWakeUpCommand> WakeUpResponse();

        IResponse<IWakeUpCommand> WakeUpResponse(string response);

        IResponseParser<IWakeUpCommand, IResponse<IWakeUpCommand>> WakeUpParser();

        IPowerStateCommand PowerStateCommand();

        IPowerStateResponse PowerStateResponse(bool standby);

        IResponseParser<IPowerStateCommand, IPowerStateResponse> PowerStateParser();

        IGetBouquetsCommand GetBouquetsCommand();

        IGetBouquetsResponse GetBouquetsResponse();

        IGetBouquetsResponse GetBouquetsResponse(IList<IBouquetItemBouquet> bouquets);

        IResponseParser<IGetBouquetsCommand, IGetBouquetsResponse> GetBouquetsParser();

        IBouquetItemBouquet BouquetItemBouquet();

        IGetBouquetItemsCommand GetBouquetItemsCommand();

        IGetBouquetItemsResponse GetBouquetItemsResponse();

        IGetBouquetItemsResponse GetBouquetItemsResponse(IList<IBouquetItem> items);

        IResponseParser<IGetBouquetItemsCommand, IGetBouquetItemsResponse> GetBouquetItemsParser();

        ISignalCommand SignalCommand();

        ISignalResponse SignalResponse(ISignal signal);

        IE1Signal E1Signal();

        IE2Signal E2Signal();

        IResponseParser<ISignalCommand, ISignalResponse> SignalParser();

        IZapCommand ZapCommand();

        IResponse<IZapCommand> ZapResponse();

        IResponse<IZapCommand> ZapResponse(string response);

        IResponseParser<IZapCommand, IResponse<IZapCommand>> ZapParser();

        IScreenshotCommand ScreenshotCommand();

        IScreenshotResponse ScreenshotResponse();

        IScreenshotResponse ScreenshotResponse(byte[] screenshot);

        IVolumeStatus VolumeStatus();

        IVolumeStatusCommand VolumeStatusCommand();

        IVolumeStatusResponse VolumeStatusResponse();

        IVolumeStatusResponse VolumeStatusResponse(IVolumeStatus status);

        IResponseParser<IVolumeStatusCommand, IVolumeStatusResponse> VolumeStatusParser();

        ISetVolumeCommand SetVolumeCommand();

        IResponse<ISetVolumeCommand> SetVolumeResponse();

        IResponse<ISetVolumeCommand> SetVolumeResponse(string response);

        IResponseParser<ISetVolumeCommand, IResponse<ISetVolumeCommand>> SetVolumeParser();

        IRemoteControlCommand RemoteControlCommand();

        IResponse<IRemoteControlCommand> RemoteControlResponse();

        IResponse<IRemoteControlCommand> RemoteControlResponse(string response);

        IResponseParser<IRemoteControlCommand, IResponse<IRemoteControlCommand>> RemoteControlParser();

        IMessageCommand MessageCommand();

        IResponse<IMessageCommand> MessageResponse();

        IResponse<IMessageCommand> MessageResponse(string response);

        IResponseParser<IMessageCommand, IResponse<IMessageCommand>> MessageParser();

        IReloadSettingsCommand ReloadSettingsCommand();

        IResponse<IReloadSettingsCommand> ReloadSettingsResponse();

        IResponse<IReloadSettingsCommand> ReloadSettingsResponse(string response);

        IResponseParser<IReloadSettingsCommand, IResponse<IReloadSettingsCommand>> ReloadSettingsParser();

        IGetStreamParametersCommand GetStreamParametersCommand();

        IGetStreamParametersResponse GetStreamParametersResponse();

        IGetStreamParametersResponse GetStreamParametersResponse(string response);

        IResponseParser<IGetStreamParametersCommand, IGetStreamParametersResponse> GetStreamParametersParser();

    }
}