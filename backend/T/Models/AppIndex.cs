using System.Linq;
using TEntities.EF;

namespace T.Models
{
    public class AMapSearchPOIResponse
    {
        public string status { get; set; }
        public string count { get; set; }
        public string info { get; set; }
        public string infocode { get; set; }
        public Pois[] pois { get; set; }
    }

    public class Pois
    {
        public string address { get; set; }
        public string location { get; set; }
    }


    public class AppIndex
    {
        public AppIndexConfig config { get; set; }
        public MenuItem[] menu { get; set; }
        public IQueryable<object> notice { get; internal set; }
    }
}