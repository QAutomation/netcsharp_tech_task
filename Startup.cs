using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using PBITracker.ApiClients;
using PBITracker.Clients;
using PBITracker.Interfaces;
using PBITracker.Models;
using PBITracker.Models.Entities;
using PBITracker.Repositories;
using System;
using System.Collections;

[assembly: FunctionsStartup(typeof(PBITracker.Startup))]
namespace PBITracker
{
    class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<IDataProvider<WorkItemModel>, WorkItemsApiProvider>();
            builder.Services.AddSingleton<IRepository<WorkItemModel>, WorkItemRepository>();
            builder.Services.AddSingleton<INotifier, MailNotifier>();
            builder.Services.AddTransient(a=>(Hashtable)Environment.GetEnvironmentVariables());
        }
    }
}