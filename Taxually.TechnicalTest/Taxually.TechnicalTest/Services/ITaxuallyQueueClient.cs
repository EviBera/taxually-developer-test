namespace Taxually.TechnicalTest.Services
{
    public interface ITaxuallyQueueClient
    {
        Task EnqueueAsync<TPayload>(string queueName, TPayload payload);
    }
}
