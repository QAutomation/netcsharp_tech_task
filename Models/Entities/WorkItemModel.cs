using Newtonsoft.Json;
using PBITracker.Models.Entities;

namespace PBITracker.Models
{
    public class WorkItemModel : EntityBase
    {
        [JsonProperty("WorkItemId")]
        public new int Id { get; set; }

        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty("WorkItemType")]
        public string WorkItemType { get; set; }

        [JsonProperty("State")]
        public string State { get; set; }
    }
}
