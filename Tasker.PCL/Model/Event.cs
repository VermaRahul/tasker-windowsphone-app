using System;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using Tasker.PCL.Utils;

namespace Tasker.PCL.Model
{
    public class Event
    {
        [DataMember, JsonProperty(PropertyName = JsonConstants.Title)]
        public string Title { get; set; }

        [DataMember, JsonProperty(PropertyName = JsonConstants.Description)]
        public string Description { get; set; }

        [DataMember, JsonProperty(PropertyName = JsonConstants.Date)]
        public DateTime Date { get; set; }

        [DataMember, JsonProperty(PropertyName = JsonConstants.Category)]
        public Category Category { get; set; }
    }
}
