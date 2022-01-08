using CrypDA.Infrastructure.HttpClients.Interfaces;

namespace CrypDA.BLL.HttpClients.RetryPolicies
{
    public class DefaultRetryPolicySettings : IRetryPolicySettings
    {
        public const string SectionName = "DefaultRetryPolicy";

        public int RetryAttempts { get; set; }

        public int FirstDelayMs { get; set; }

        public int MaxDelayMs { get; set; }
    }
}
