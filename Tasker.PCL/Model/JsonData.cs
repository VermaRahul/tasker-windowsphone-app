using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Tasker.PCL.Utils;

namespace Tasker.PCL.Model
{
    public class JsonData
    {
        [DataMember, JsonProperty(PropertyName = JsonConstants.Categories)]
        public List<Category> Categories;

        public JsonData DeepCopy()
        {
            var ser = JsonConvert.SerializeObject(this);
            var des = JsonConvert.DeserializeObject<JsonData>(ser);

            return des;
        }
    }
}
