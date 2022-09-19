using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using PBITracker.ApiClients;
using PBITracker.Clients;
using PBITracker.Interfaces;
using PBITracker.Models;
using PBITracker.Models.Entities;
using PBITracker.Repositories;

[assembly: FunctionsStartup(typeof(PBITracker.Startup))]
namespace PBITracker
{
    class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<IDataProvider, WorkItemsApiProvider>();
            builder.Services.AddSingleton<IRepository<EntityBase>, WorkItemRepository>();
            builder.Services.AddSingleton<INotifier, MailNotifier>();
        }
    }
}