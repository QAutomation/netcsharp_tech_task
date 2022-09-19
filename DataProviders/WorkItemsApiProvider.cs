using Newtonsoft.Json;
using PBITracker.Models;
using PBITracker.Models.Entities;
using RestSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PBITracker.ApiClients
{
    public class WorkItemsApiProvider : IDataProvider
    {
        private const string WORKITEMS = "WorkItems";
        private readonly Hashtable config = (Hashtable)Environment.GetEnvironmentVariables();
        private readonly RestClient client;

        public WorkItemsApiProvider()
        {
            client = new RestClient($"{config["host"]}/{config["Organization"]}/{config["Project"]}/_odata/v4.0-preview/")
            { AcceptedContentTypes = new string[] { "application/json" } }
                        .AddDefaultHeader("Authorization", $"Basic {Get64BitValue(string.Format("{0}:{1}", "", config["PersonalToken"]))}");
        }

        public async Task<IEnumerable<EntityBase>> GetData(IDictionary<string, string> filters)
        {
            var request = new RestRequest($"{WORKITEMS}") { Method = Method.Get };
            request.AddQueryParameter("$select", "WorkItemId,WorkItemType,Title,State");
            foreach (var kvp in filters)
            {
                request.AddQueryParameter(kvp.Key, kvp.Value);
            }

            var response = await client.ExecuteAsync(request);
            WorkitemsQueryResponseModel result = null;
            if (response.IsSuccessful)
            {
                result = JsonConvert.DeserializeObject<WorkitemsQueryResponseModel>(response.Content);
            }
            return result.Value;
        }

        private string Get64BitValue(string valueForConvertion)
        {
            return Convert.ToBase64String(Encoding.ASCII.GetBytes(valueForConvertion));
        }
    }
}