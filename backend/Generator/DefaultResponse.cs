using System.Collections.Generic;

namespace Generator
{
    public class DefaultResponse<T>
    {
        public int total { get; set; }
        public string message { get; set; }
        public bool success { get; set; }
        public List<T> rows { get; set; }
    }
}