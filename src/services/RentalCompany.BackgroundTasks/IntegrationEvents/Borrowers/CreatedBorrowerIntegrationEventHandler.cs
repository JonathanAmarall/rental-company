using Microsoft.Extensions.Logging;
using RentalCompany.Application.Borrower.Events;
using RentalCompany.Core.Contracts;
using RentalCompany.Core.Email;
using RentalCompany.Core.Models;
using RentalCompany.Domain.Repositories;

namespace RentalCompany.BackgroundTasks.IntegrationEvents.Borrowers
{
    internal class CreatedBorrowerIntegrationEventHandler : IIntegrationEventHandler<CreatedBorrowerIntegrationEvent>
    {
        private readonly IBorrowerRepository _borrowerRepository;
        private readonly IEmailService _emailService;
        private readonly ILogger<CreatedBorrowerIntegrationEventHandler> _logger;

        public CreatedBorrowerIntegrationEventHandler(IBorrowerRepository borrowerRepository, IEmailService emailService, ILogger<CreatedBorrowerIntegrationEventHandler> logger)
        {
            _borrowerRepository = borrowerRepository;
            _emailService = emailService;
            _logger = logger;
        }

        public async Task Handle(CreatedBorrowerIntegrationEvent notification, CancellationToken cancellationToken)
        {
            var borrower = await _borrowerRepository.GetByIdAsync(notification.BorrowerId);
            var (subject, body) = MailTemplates.CreateWelcomeBorrowerMail(borrower!.Email.Value, borrower.FullName);
            var mailRequest = new MailRequest(borrower.Email.Value, subject, body);
            await _emailService.SendEmailAsync(mailRequest);

            _logger.LogInformation("Enviando email para {email} do Borrower {fullName}", borrower.Email, borrower.FullName);
        }
    }
}
