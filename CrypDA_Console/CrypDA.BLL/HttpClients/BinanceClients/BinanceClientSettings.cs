using CrypDA.Infrastructure.HttpClients.Interfaces;

namespace CrypDA.BLL.HttpClients.BinanceClients
{
    public class BinanceClientSettings : IHttpClientSettings
    {
        public const string SectionName = "BinanceClient";

        public string Host { get; set; }
    }
}
