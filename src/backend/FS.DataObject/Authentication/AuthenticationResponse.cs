namespace FS.DataObject.Authentication
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public class AuthenticationResponse
    {
        [JsonPropertyName("error")]
        public bool Error { get; private set; }

        [JsonPropertyName("messages")]
        public IEnumerable<string> Messages { get; private set; }
        
        [JsonPropertyName("token")]
        public string Token { get; private set; }

        public AuthenticationResponse(IEnumerable<string> errors)
        {
            Error = true;
            Messages = errors;
        }

        public AuthenticationResponse(string token)
        {
            Error = false;
            Token = token;
        }
    }
}