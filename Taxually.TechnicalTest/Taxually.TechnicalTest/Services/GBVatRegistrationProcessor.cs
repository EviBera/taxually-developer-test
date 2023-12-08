using System.Xml.Linq;
using Taxually.TechnicalTest.Models;

namespace Taxually.TechnicalTest.Services
{
    public class GBVatRegistrationProcessor : VatRegistrationProcessorBase
    {
        public GBVatRegistrationProcessor(VatRegistrationRequest request) : base(request)
        {
        }
        public override async Task SaveDataToDestinationAsync()
        {
            // UK has an API to register for a VAT number
            var httpClient = new TaxuallyHttpClient();
            await httpClient.PostAsync("https://api.uktax.gov.uk", request);
        }
    }
}
