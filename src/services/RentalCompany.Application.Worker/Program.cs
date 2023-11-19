using RentalCompany.Application.Worker;
using RentalCompany.BackgroundTasks;
using RentalCompany.Data;
using RentalCompany.MessageBus;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddMemoryCache();
        services.AddHostedService<Worker>();
        services
        .AddData(hostContext.Configuration)
        .AddBackgroundTasks(hostContext.Configuration)
        .AddMessageBus(hostContext.Configuration);
    })
    .Build();

host.Run();
