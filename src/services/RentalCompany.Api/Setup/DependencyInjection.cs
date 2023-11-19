using RentalCompany.Application;
using RentalCompany.Data;
using RentalCompany.MessageBus;

namespace RentalCompany.Api.Setup;
public static class DependencyInjection
{
    public static void AddDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddData(configuration)
            .AddApplication()
            .AddMessageBus(configuration);
    }
}