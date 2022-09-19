using Newtonsoft.Json;
using System.Collections.Generic;

namespace PBITracker.Models
{
    public class WorkitemsQueryResponseModel
    {
        [JsonProperty("@odata.context")]
        public string OdataContext { get; set; }

        [JsonProperty("value")]
        public IEnumerable<WorkItemModel> Value { get; set; }
    }
}