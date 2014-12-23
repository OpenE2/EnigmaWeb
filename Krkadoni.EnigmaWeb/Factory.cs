using System;
using System.Collections.Generic;
using Krkadoni.Enigma.Commands;
using Krkadoni.Enigma.Parsers;
using Krkadoni.Enigma.Responses;

namespace Krkadoni.Enigma
{
    public class Factory : IFactory
    {
        private ILog _log;

        public virtual ILog Log()
        {
            return _log ?? (_log = new ConsoleLog());
        }

        public virtual IGetCurrentServiceCommand GetCurrentServiceCommand()
        {
            return new GetCurrentServiceCommand(this);
        }

        public virtual IGetCurrentServiceResponse GetCurrentServiceResponse(IBouquetItemService service)
        {
            return new GetCurrentServiceResponse(service);
        }

        public virtual IGetCurrentServiceResponse GetCurrentServiceResponse()
        {
            throw new NotImplementedException();
        }

        public virtual IResponseParser<IGetCurrentServiceCommand, IGetCurrentServiceResponse> GetCurrentServiceParser()
        {
            return new GetCurrentServiceParser(this);
        }

        public virtual IBouquetItemService BouquetItemService()
        {
            return new BouquetItemService();
        }

        public virtual IBouquetItemMarker BouquetItemMarker()
        {
            return new BouquetItemMarker();
        }

        public virtual IWebRequester WebRequester()
        {
            return new WebRequester(_log);
        }

        public virtual IWakeUpCommand WakeUpCommand()
        {
            return new WakeUpCommand(this);
        }

        public virtual IResponse<IWakeUpCommand> WakeUpResponse()
        {
            return new UnparsedResponse<IWakeUpCommand>(null);
        }

        public virtual IResponse<IWakeUpCommand> WakeUpResponse(string response)
        {
            return new UnparsedResponse<IWakeUpCommand>(response);
        }

        public virtual IResponseParser<IWakeUpCommand, IResponse<IWakeUpCommand>> WakeUpParser()
        {
            return new UnparsedParser<IWakeUpCommand>();
        }

        public virtual IPowerStateCommand PowerStateCommand()
        {
            return new PowerStateCommand(this);
        }

        public virtual IPowerStateResponse PowerStateResponse(bool standby)
        {
            return new PowerStateResponse(standby);
        }

        public virtual IResponseParser<IPowerStateCommand, IPowerStateResponse> PowerStateParser()
        {
            return new PowerStateParser(this);
        }

        public virtual IGetBouquetsCommand GetBouquetsCommand()
        {
            return new GetBouquetsCommand(this);
        }

        public virtual IGetBouquetsResponse GetBouquetsResponse()
        {
            return new GetBouquetsResponse();
        }

        public virtual IGetBouquetsResponse GetBouquetsResponse(IList<IBouquetItemBouquet> bouquets)
        {
            return new GetBouquetsResponse(bouquets);
        }

        public virtual IResponseParser<IGetBouquetsCommand, IGetBouquetsResponse> GetBouquetsParser()
        {
            return new GetBouquetsParser(this);
        }

        public virtual IBouquetItemBouquet BouquetItemBouquet()
        {
            return new BouquetItemBouquet();
        }

        public virtual IGetBouquetItemsCommand GetBouquetItemsCommand()
        {
            return new GetBouquetItemsCommand(this);
        }

        public virtual IGetBouquetItemsResponse GetBouquetItemsResponse()
        {
            return new GetBouquetItemsResponse();
        }

        public virtual IGetBouquetItemsResponse GetBouquetItemsResponse(IList<IBouquetItem> items)
        {
            return new GetBouquetItemsResponse(items);
        }

        public virtual IResponseParser<IGetBouquetItemsCommand, IGetBouquetItemsResponse> GetBouquetItemsParser()
        {
            return new GetBouquetItemsParser(this);
        }

        public virtual ISignalCommand SignalCommand()
        {
            return new SignalCommand(this);
        }

        public virtual ISignalResponse SignalResponse(ISignal signal)
        {
            return new SignalResponse(signal);
        }

        public virtual IE1Signal E1Signal()
        {
            return new E1Signal();
        }

        public virtual IE2Signal E2Signal()
        {
            return new E2Signal();
        }

        public virtual IResponseParser<ISignalCommand, ISignalResponse> SignalParser()
        {
            return new SignalParser(this);
        }

        public virtual IZapCommand ZapCommand()
        {
            return new ZapCommand(this);
        }

        public virtual IResponse<IZapCommand> ZapResponse()
        {
            return new UnparsedResponse<IZapCommand>(null);
        }

        public virtual IResponse<IZapCommand> ZapResponse(string response)
        {
            return new UnparsedResponse<IZapCommand>(response);
        }

        public virtual IResponseParser<IZapCommand, IResponse<IZapCommand>> ZapParser()
        {
            return new UnparsedParser<IZapCommand>();
        }

        public virtual IScreenshotCommand ScreenshotCommand()
        {
            return new ScreenshotCommand(this);
        }

        public virtual IScreenshotResponse ScreenshotResponse()
        {
            return new ScreenshotResponse(null);
        }

        public virtual IScreenshotResponse ScreenshotResponse(byte[] screenshot)
        {
            return new ScreenshotResponse(screenshot);
        }

        public virtual IVolumeStatus VolumeStatus()
        {
            return new VolumeStatus();
        }

        public virtual IVolumeStatusCommand VolumeStatusCommand()
        {
            return new VolumeStatusCommand(this);
        }

        public virtual IVolumeStatusResponse VolumeStatusResponse()
        {
            throw new NotImplementedException();
        }

        public virtual IVolumeStatusResponse VolumeStatusResponse(IVolumeStatus status)
        {
            return new VolumeStatusResponse(status);
        }

        public virtual IResponseParser<IVolumeStatusCommand, IVolumeStatusResponse> VolumeStatusParser()
        {
            return new VolumeStatusParser(this);
        }

        public virtual ISetVolumeCommand SetVolumeCommand()
        {
            return new SetVolumeCommand(this);
        }

        public virtual IResponse<ISetVolumeCommand> SetVolumeResponse()
        {
            return new UnparsedResponse<ISetVolumeCommand>(null);
        }

        public virtual IResponse<ISetVolumeCommand> SetVolumeResponse(string response)
        {
            return new UnparsedResponse<ISetVolumeCommand>(response);
        }

        public virtual IResponseParser<ISetVolumeCommand, IResponse<ISetVolumeCommand>> SetVolumeParser()
        {
            return new UnparsedParser<ISetVolumeCommand>();
        }

        public virtual IRemoteControlCommand RemoteControlCommand()
        {
            return new RemoteControlCommand(this);
        }

        public virtual IResponse<IRemoteControlCommand> RemoteControlResponse()
        {
            return new UnparsedResponse<IRemoteControlCommand>(null);
        }

        public virtual IResponse<IRemoteControlCommand> RemoteControlResponse(string response)
        {
            return new UnparsedResponse<IRemoteControlCommand>(response);
        }

        public virtual IResponseParser<IRemoteControlCommand, IResponse<IRemoteControlCommand>> RemoteControlParser()
        {
            return new UnparsedParser<IRemoteControlCommand>();
        }

        public IMessageCommand MessageCommand()
        {
            return new MessageCommand(this);
        }

        public IResponse<IMessageCommand> MessageResponse()
        {
            return new UnparsedResponse<IMessageCommand>(null);
        }

        public IResponse<IMessageCommand> MessageResponse(string response)
        {
            return new UnparsedResponse<IMessageCommand>(response);
        }

        public IResponseParser<IMessageCommand, IResponse<IMessageCommand>> MessageParser()
        {
            return new UnparsedParser<IMessageCommand>();
        }

        public IReloadSettingsCommand ReloadSettingsCommand()
        {
            return new ReloadSettingsCommand(this);
        }

        public IResponse<IReloadSettingsCommand> ReloadSettingsResponse()
        {
            return new UnparsedResponse<IReloadSettingsCommand>(null);
        }

        public IResponse<IReloadSettingsCommand> ReloadSettingsResponse(string response)
        {
            return new UnparsedResponse<IReloadSettingsCommand>(response);
        }

        public IResponseParser<IReloadSettingsCommand, IResponse<IReloadSettingsCommand>> ReloadSettingsParser()
        {
            return new UnparsedParser<IReloadSettingsCommand>();
        }
    }
}