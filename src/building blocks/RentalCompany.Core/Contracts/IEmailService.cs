using RentalCompany.Core.Models;

namespace RentalCompany.Core.Contracts
{
    public interface IEmailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
