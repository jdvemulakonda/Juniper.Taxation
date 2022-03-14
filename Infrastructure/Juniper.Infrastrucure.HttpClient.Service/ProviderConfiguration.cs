namespace Juniper.Infrastrutcure.ExternalCommunication.Service
{
    public class ProviderConfiguration
    {
        public string ProviderKey { get; set; }

        public string ProviderBaseEndPoint { get; set; }

        public string AuthToken { get; set; }

        public bool IsTokenAuthRequired { get; set; }
    }
}