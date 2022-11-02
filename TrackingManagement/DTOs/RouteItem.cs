using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackingManagement.DTOs
{
    public class RouteItem
    {
        [JsonProperty("transactionPointId")]
        public int TransactionPointId { get; set; }
        [JsonProperty("order")]
        public int Order { get; set; }
        [JsonProperty("time")]
        public string Time { get; set; }
    }
}
