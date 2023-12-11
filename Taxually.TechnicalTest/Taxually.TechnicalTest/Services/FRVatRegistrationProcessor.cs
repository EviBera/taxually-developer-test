using System.Text;
using Taxually.TechnicalTest.Models;

namespace Taxually.TechnicalTest.Services
{
    public class FRVatRegistrationProcessor : VatRegistrationProcessorBase
    {
        private readonly ITaxuallyQueueClient _excelQueueClient;
        public FRVatRegistrationProcessor(VatRegistrationRequest request, 
            ITaxuallyQueueClient taxuallyQueueClient) : base(request)
        {
            _excelQueueClient = taxuallyQueueClient;
        }

        public override async Task SaveDataToDestinationAsync()
        {
            // France requires an excel spreadsheet to be uploaded to register for a VAT number
            var csvBuilder = new StringBuilder();
            csvBuilder.AppendLine("CompanyName,CompanyId");
            csvBuilder.AppendLine($"{request.CompanyName}{request.CompanyId}");
            var csv = Encoding.UTF8.GetBytes(csvBuilder.ToString());
            // Queue file to be processed
            await _excelQueueClient.EnqueueAsync("vat-registration-csv", csv);
        }
    }
}
