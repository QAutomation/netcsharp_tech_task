using PBITracker.ApiClients;
using PBITracker.Interfaces;
using PBITracker.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PBITracker.Repositories
{
    public class WorkItemRepository : IRepository<WorkItemModel>
    {
        private readonly IDataProvider<WorkItemModel> dataProvider;

        public WorkItemRepository(IDataProvider<WorkItemModel> dataProvider)
        {
            this.dataProvider = dataProvider;
        }

        public async Task<IEnumerable<WorkItemModel>> FindAll()
        {
            return await dataProvider.GetData(new Dictionary<string, string>());
        }

        public async Task<IEnumerable<WorkItemModel>> FindAfterDate(DateTime dateTime)
        {
            return await dataProvider.GetData(GetQueryParameters(dateTime));
        }

        public async Task<IEnumerable<WorkItemModel>> FindById(params int[] values)
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