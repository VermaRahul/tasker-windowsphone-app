using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.PCL.Enumerations;
using Tasker.PCL.Extensions;

namespace Tasker.PCL.Model
{
    public class CustomKeyGroup<T> : List<T>
    {

        public static IEnumerable<Event> GetEventList(List<Event> items)
        {
            List<Event> eventList = new List<Event>();
            eventList = items;
            return eventList;
        }

        public static List<Group<Event>> GetEventGroups(List<Event> items)
        {
            IEnumerable<Event> list = GetEventList(items);
            return GetItemGroupsByTimeFrame(list, c => ((int)c.Date.GetTimeFrame()).ToString());
        }

        public static List<Group<T>> GetItemGroups<T>(IEnumerable<T> itemList, Func<T, string> getKeyFunc)
        {
            IEnumerable<Group<T>> groupList = from item in itemList
                                              group item by getKeyFunc(item)
                                                  into g
                                                  orderby g.Key
                                                  select new Group<T>((DateExtensions.FromUnixTimeMilliseconds(Convert.ToInt64(g.Key)).GetTimeFrameString()), g);
            return groupList.ToList();
        }

        public static List<Group<T>> GetItemGroupsByTimeFrame<T>(IEnumerable<T> itemList, Func<T, string> getKeyFunc)
        {
            IEnumerable<Group<T>> groupList = from item in itemList
                                              group item by getKeyFunc(item)
                                                  into g
                                                  orderby g.Key
                                                  select new Group<T>(DateExtensions.GetEnumAsString((TimeFrame)Enum.Parse(typeof(TimeFrame), g.Key)), g);
            return groupList.ToList();
        }

        public class Group<T> : List<T>
        {
            public Group(string name, IEnumerable<T> items)
                : base(items)
            {
                this.Title = name;
            }
            public string Title { get; set; }
        }
    }
}
