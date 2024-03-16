using Market.Models.DTO;
using Market.Models.Payment.PaymentResponse;

namespace Market.Services.IServices
{
    public interface IVnPayService
    {
        string CreatePaymentUrl(OrderDTO model, HttpContext context);
        string CreatePaymentUrlForPackage(PackageOFCreatorDTO model, HttpContext context);
        PaymentResponse PaymentExecute(IQueryCollection collections);
    }
}
