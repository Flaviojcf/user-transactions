using System.Diagnostics.CodeAnalysis;

namespace UserTransactions.Consumer.Constants
{
    [ExcludeFromCodeCoverage]
    public static class KafkaTopics
    {
        public const string TransactionCreated = "transaction-created";
    }
}