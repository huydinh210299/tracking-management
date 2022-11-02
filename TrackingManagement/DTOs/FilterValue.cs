using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackingManagement.DTOs
{
    public class FilterValue
    {
        [JsonProperty("valueName")]
        public string ValueName { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
