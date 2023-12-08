using Taxually.TechnicalTest.Models;

namespace Taxually.TechnicalTest.Services
{
    public abstract class VatRegistrationProcessorBase : IVatRegistrationProcessor
    {
        protected readonly VatRegistrationRequest request;
        protected VatRegistrationProcessorBase(VatRegistrationRequest request) {
            this.request = request;
        }

        public abstract Task SaveDataToDestinationAsync();

        
    }
}
