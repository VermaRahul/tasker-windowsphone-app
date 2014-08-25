using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.PCL.Enumerations;

namespace Tasker.PCL.Model
{
    public class PageNavigationData
    {
        public ObjectType Mode { get; set; }
        public List<Category> Categories { get; set; }
        public List<Event> Events { get; set; }
        public Category Category { get; set; }

    }
}
