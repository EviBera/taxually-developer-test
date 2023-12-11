namespace Taxually.TechnicalTest.Services
{
    public interface ITaxuallyHttpClient
    {
        Task PostAsync<TRequest>(string url, TRequest request);
    }
}
