using System.Collections.Generic;
using T.Models;

namespace T.Evaluators
{
    public class CommonOutputList<T> : CommonOutputT<T>
    {
        public ICollection<T> rows { get; set; }

        public int total { get; internal set; }
    }
}