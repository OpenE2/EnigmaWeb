using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Krkadoni.Enigma.Enums;
using Krkadoni.Enigma.Parsers;
using Krkadoni.Enigma.Properties;
using Krkadoni.Enigma.Responses;

namespace Krkadoni.Enigma.Commands
{
    [ComVisible(false)]
    public class ReloadSettingsCommand : EnigmaCommand<IReloadSettingsCommand, IResponse<IReloadSettingsCommand>>, IReloadSettingsCommand
    {
        private readonly IResponseParser<IReloadSettingsCommand, IResponse<IReloadSettingsCommand>> _parser;

        public ReloadSettingsCommand([NotNull] IFactory factory)
            : base(factory)
        {
            _parser = Factory.ReloadSettingsParser();
        }

        public async Task<IResponse<IReloadSettingsCommand>> ExecuteAsync(IProfile profile, CancellationToken token, ReloadSettingsType type)
        {
            if (profile.Enigma == EnigmaType.Enigma1)
            {
                if (type == ReloadSettingsType.All)
                {
                    await base.ExecuteAsync(profile, "cgi-bin/reloadSettings", _parser, token);
                    return await base.ExecuteAsync(profile, "cgi-bin/reloadUserBouquets", _parser, token);
                }

                if (type == ReloadSettingsType.Services)
                {
                    return await base.ExecuteAsync(profile, "cgi-bin/reloadSettings", _parser, token);
                }

                if (type == ReloadSettingsType.Bouquets)
                {
                    return await base.ExecuteAsync(profile, "cgi-bin/reloadUserBouquets", _parser, token);
                }

                throw new NotSupportedException("ReloadSettingsType");
            }

            return await base.ExecuteAsync(profile, "web/servicelistreload?mode=" + (int) type, _parser, token);
        }
    }
}