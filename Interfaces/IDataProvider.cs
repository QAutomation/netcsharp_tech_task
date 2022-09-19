using PBITracker.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PBITracker.ApiClients
{
    public interface IDataProvider
    {
        public Task<IEnumerable<EntityBase>> GetData(IDictionary<string,string> filters);
    }
}