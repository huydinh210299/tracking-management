using Newtonsoft.Json;
using System.Collections.Generic;

namespace TrackingManagement.DTOs
{
    public class RequestFilter
    {
        [JsonProperty("field")]
        public string Field { get; set; }
        [JsonProperty("displayName")]
        public string DisplayName { get; set; }
        [JsonProperty("value")]
        public List<FilterValue> Value { get; set; }
    }
}
