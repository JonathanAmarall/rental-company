using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RentalCompany.BackgroundTasks.Services;
using RentalCompany.BackgroundTasks.Tasks;
using RentalCompany.Core.Contracts;
using RentalCompany.Core.Email;
using RentalCompany.MessageBus;
using System.Reflection;

namespace RentalCompany.BackgroundTasks;

public static class DependencyInjection
{
    public static IServiceCollection AddBackgroundTasks(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.Configure<MessageBusSettings>(configuration.GetSection(MessageBusSettings.SettingsKey));
        services.Configure<MailSettings>(configuration.GetSection(MailSettings.SettingsKey));

        services.AddScoped<IIntegrationEventConsumer, IntegrationEventConsumer>();
        services.AddTransient<IEmailService, EmailService>();
        services.AddHostedService<IntegrationEventConsumerBackgroundService>();

        return services;
    }
}
