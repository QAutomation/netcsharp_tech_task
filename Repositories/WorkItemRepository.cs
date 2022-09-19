using PBITracker.ApiClients;
using PBITracker.Interfaces;
using PBITracker.Models;
using PBITracker.Models.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PBITracker.Repositories
{
    public class WorkItemRepository : IRepository<EntityBase>
    {
        private readonly IDataProvider dataProvider;

        public WorkItemRepository(IDataProvider dataProvider)
        {
            this.dataProvider = dataProvider;
        }

        public async Task<IEnumerable<EntityBase>> FindAll()
        {
            return await dataProvider.GetData(new Dictionary<string, string>());
        }

        public async Task<IEnumerable<EntityBase>> FindAfterDate(DateTime dateTime)
        {
            return await dataProvider.GetData(GetQueryParameters(dateTime));
        }

        public async Task<IEnumerable<EntityBase>> FindById(params int[] values)
        {
            return await dataProvider.GetData(GetQueryParameters(values));
        }

        private IDictionary<string, string> GetQueryParameters(DateTime startingFromDate)
        {
            return new Dictionary<string, string> {
                { "$filter", $"CreatedDate ge {startingFromDate:yyyy-MM-ddTHH:mm:ssZ}"}
            };
        }
        
        private IDictionary<string, string> GetQueryParameters(params int[] values)
        {
            string ids = $"WorkItemId eq {string.Join(" or WorkItemId eq ", values)}";
            return new Dictionary<string, string> {
                { "$filter", $"{ids}" }
            };
        }
    }
}