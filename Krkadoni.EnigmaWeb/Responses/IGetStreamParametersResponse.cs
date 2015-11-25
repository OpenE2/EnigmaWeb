using System;
using Krkadoni.Enigma.Commands;

namespace Krkadoni.Enigma.Responses
{
    public interface IGetStreamParametersResponse : IResponse<IGetStreamParametersCommand>
    {
        string StreamUrl { get; set;}

        string m3uFileContent { get; set;}
    }
}

