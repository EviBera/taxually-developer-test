using Taxually.TechnicalTest.Models;

namespace Taxually.TechnicalTest.Services
{
    public interface IVatRegistrationServiceFactory
    {
        IVatRegistrationProcessor CreateProcessorInstance(VatRegistrationRequest request);
    }
}
