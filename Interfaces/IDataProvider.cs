using PBITracker.Models;
using PBITracker.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PBITracker.ApiClients
{
    public interface IDataProvider<T> where T : EntityBase
    {
        public Task<IEnumerable<T>> GetData(IDictionary<string,string> filters);
    }
}