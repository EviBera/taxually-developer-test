using Taxually.TechnicalTest.Models;

namespace Taxually.TechnicalTest.Services
{
    public class VatRegistrationServiceFactory : IVatRegistrationServiceFactory
    {
        private readonly ITaxuallyHttpClient _taxuallyHttpClient;
        private readonly IConfiguration _configuration;
        private readonly ITaxuallyQueueClient _taxuallyQueueClient;

        public VatRegistrationServiceFactory(ITaxuallyHttpClient taxuallyHttpClient, 
            IConfiguration configuration,
            ITaxuallyQueueClient taxuallyQueueClient)
        {
            _taxuallyHttpClient = taxuallyHttpClient;
            _configuration = configuration;
            _taxuallyQueueClient = taxuallyQueueClient;
        }

        public IVatRegistrationProcessor CreateProcessorInstance(VatRegistrationRequest request)
        {
            switch(request.Country)
            {
                case "GB":
                    return new GBVatRegistrationProcessor(request, _taxuallyHttpClient, _configuration);
                case "FR":
                    return new FRVatRegistrationProcessor(request, _taxuallyQueueClient);
                case "DE":
                    return new DEVatRegistrationProcessor(request, _taxuallyQueueClient);
                default:
                    throw new ArgumentException("Invalid argument");
            }
        }
    }
}
