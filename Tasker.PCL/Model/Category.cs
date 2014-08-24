using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Tasker.PCL.Utils;

namespace Tasker.PCL.Model
{
    public class Category
    {
        [DataMember, JsonProperty(PropertyName = JsonConstants.Name)]
        public string Name { get; set; }
    }
}
