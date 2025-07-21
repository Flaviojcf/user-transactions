using Confluent.Kafka;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using UserTransactions.Consumer.Constants;
using UserTransactions.Consumer.Events;
using UserTransactions.Consumer.Services.EmailService;

namespace UserTransactions.Consumer.Services.Messaging
{
    [ExcludeFromCodeCoverage]
    public class KafkaMessageConsumer : BackgroundService
    {
        private readonly IConsumer<string, string> _consumer;
        private readonly ILogger<KafkaMessageConsumer> _logger;
        private readonly IEmailService _emailService;

        public KafkaMessageConsumer(KafkaConsumerFactory consumerFactory, ILogger<KafkaMessageConsumer> logger, IEmailService emailService)
        {
            _consumer = consumerFactory.CreateConsumer("transaction-email-service");
            _logger = logger;
            _emailService = emailService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _consumer.Subscribe(KafkaTopics.TransactionCreated);

            _logger.LogInformation("Kafka consumer started and subscribed to topic: {Topic}", KafkaTopics.TransactionCreated);

            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        var result = _consumer.Consume(stoppingToken);

                        if (result?.Message?.Value != null)
                        {
                            await ProcessTransactionCreatedEvent(result.Message.Value);

                            _consumer.Commit(result);

                            _logger.LogInformation("Message processed and committed: {MessageKey}", result.Message.Key);
                        }
                    }
                    catch (OperationCanceledException)
                    {
                        _logger.LogInformation("Consumer operation was cancelled");
                        break;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error processing message from Kafka");
                        await Task.Delay(5000, stoppingToken);
                    }
                }
            }
            finally
            {
                _consumer.Close();
                _logger.LogInformation("Kafka consumer stopped");
            }
        }

        private async Task ProcessTransactionCreatedEvent(string messageValue)
        {
            var transactionEvent = JsonSerializer.Deserialize<TransactionCreatedEvent>(messageValue, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            if (transactionEvent != null)
            {
                var subject = "Transação Recebida";
                var message = $"Você recebeu uma transação de R$ {transactionEvent.Amount:F2} em {transactionEvent.CreatedAt:dd/MM/yyyy HH:mm:ss}";

                await _emailService.SendAsync(transactionEvent.ReceiverEmail, subject, message);

                _logger.LogInformation("Email sent to {Email} for transaction {TransactionId}",
                    transactionEvent.ReceiverEmail, transactionEvent.TransactionId);
            }
        }
        public override void Dispose()
        {
            _consumer?.Dispose();
            base.Dispose();
        }
    }
}
