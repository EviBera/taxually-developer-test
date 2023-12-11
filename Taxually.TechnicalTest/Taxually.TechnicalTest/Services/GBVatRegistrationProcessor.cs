using System.Xml.Linq;
using Taxually.TechnicalTest.Models;

namespace Taxually.TechnicalTest.Services
{
    public class GBVatRegistrationProcessor : VatRegistrationProcessorBase
    {
        private readonly ITaxuallyHttpClient _taxuallyHttpClient;
        private readonly IConfiguration _configuration;
        public GBVatRegistrationProcessor(VatRegistrationRequest request, 
            ITaxuallyHttpClient taxuallyHttpClient,
            IConfiguration configuration) : base(request)
        {
            _taxuallyHttpClient = taxuallyHttpClient;   
            _configuration = configuration;
        }
        public override async Task SaveDataToDestinationAsync()
        {
            // UK has an API to register for a VAT number
            var url = _configuration["GBVatRegistrationURL"];
            await _taxuallyHttpClient.PostAsync(url, request);
        }
    }
}
