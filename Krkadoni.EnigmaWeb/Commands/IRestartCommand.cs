﻿using System.Threading;
using System.Threading.Tasks;
using Krkadoni.Enigma.Responses;

namespace Krkadoni.Enigma.Commands
{
    public interface IRestartCommand : ICommand
    {
        Task<IResponse<IRestartCommand>> ExecuteAsync(IProfile profile, CancellationToken token);
    }
}