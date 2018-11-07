using System;

namespace T.Evaluators
{
    public class CacheOptionsAttribute : Attribute
    {
        public int Timeout { get; set; }
    }
}