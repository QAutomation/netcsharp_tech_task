using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using PBITracker.Clients;
using PBITracker.Interfaces;
using PBITracker.Models;
using PBITracker.Models.Entities;

namespace PBITracker.AzureFunctions
{
    public class PbiTrackerFunc
    {
        private ILogger log;
        private readonly IRepository<EntityBase> repo;
        private readonly INotifier notifier;

        public PbiTrackerFunc(IRepository<EntityBase> repo, INotifier notifier)
        {
            this.repo = repo ?? throw new ArgumentNullException(nameof(repo));
            this.notifier = notifier;
        }

        [FunctionName("PbiTrackerFunc")]
        public async Task Run([TimerTrigger("0 */1 * * * *")] TimerInfo myTimer, ILogger logger)
        {
            log = logger;
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            var items = (IEnumerable<WorkItemModel>)await repo.FindAfterDate(GetTimeWithGap());
            if (items.Count() > 0)
            {
                log.LogWarning($"{items.Count()} items detected");
                items.ToList().ForEach(x => log.LogWarning($"{x.Id} {x.State} {x.Title} {x.WorkItemType}"));
                await notifier.Notify($"{items.Count()} new items detected"); ;
            }
        }

        private DateTime GetTimeWithGap()
        {
            return DateTime.UtcNow.AddMinutes(-1);
        }
    }
}