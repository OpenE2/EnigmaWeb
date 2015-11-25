namespace Krkadoni.Enigma.Responses
{
    public class GetStreamParametersResponse : IGetStreamParametersResponse
    {
        
        public GetStreamParametersResponse()
        {
            StreamUrl = string.Empty;
            m3uFileContent = string.Empty;
        }

        public GetStreamParametersResponse(string response)
        {
            StreamUrl = string.Empty;
            m3uFileContent = response;
        }

        public string StreamUrl { get; set;}
       
        public string m3uFileContent { get; set;}
                    
    }
}