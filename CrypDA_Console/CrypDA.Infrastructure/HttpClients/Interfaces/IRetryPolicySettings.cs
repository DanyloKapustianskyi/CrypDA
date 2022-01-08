namespace CrypDA.Infrastructure.HttpClients.Interfaces
{
    public interface IRetryPolicySettings
    {
        public int RetryAttempts { get; set; }

        public int FirstDelayMs { get; set; }

        public int MaxDelayMs { get; set; }
    }
}
