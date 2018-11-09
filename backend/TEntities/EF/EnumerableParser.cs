using System.Collections.Generic;
using System.Linq;

namespace TENtities.EF
{
    public class EnumerableParser
    {
        public static IEnumerable<Dictionary<string, string>> Parse(IEnumerable<object> enumerable)
        {
            return enumerable.Select(ObjectParser.Parse);
        }
    }
}