using Microsoft.Extensions.DependencyInjection;
using RentalCompany.Application.Borrower.Commands.CreateBorrower;
using RentalCompany.Domain.Handler;
using System.Reflection;

namespace RentalCompany.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services
            .AddScoped<CreateCollectionItemCommandHandler>()
            .AddScoped<CreateLocationCommandHandler>()
            .AddScoped<LendCollectionItemCommandHandler>()
            .AddScoped<AddLocationInCollectionCommandHandler>()
            .AddScoped<CreateBorrowerCommandHandler>();

        return services;
    }
}
