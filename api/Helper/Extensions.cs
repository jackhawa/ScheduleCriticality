using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchedulePath.Helper
{
    public static class Extensions
    {
        public static List<T> CloneLists<T>(this List<T> oldList)
        {
            var newList = new List<T>();
            foreach (var item in oldList)
            {
                newList.Add(item);
            }
            return newList;
        }
    }
}
