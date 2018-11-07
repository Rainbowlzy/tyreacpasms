using System.Collections.Generic;
using System.Linq;

namespace TEntities1.EF
{
    public class EnumerableParser
    {
        public static IEnumerable<Dictionary<string, string>> Parse(IEnumerable<object> enumerable)
        {
            return enumerable.Select(ObjectParser.Parse);
        }
    }
}