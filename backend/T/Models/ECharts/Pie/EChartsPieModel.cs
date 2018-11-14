namespace T.Models.ECharts.Pie
{
    public class EChartsPieModel
    {
        public Title title { get; set; }
        public Tooltip tooltip { get; set; }
        public Legend legend { get; set; }
        public Series[] series { get; set; }
    }

    public class Title
    {
        public string text { get; set; }
        public string x { get; set; }
    }

    public class Tooltip
    {
        public string trigger { get; set; }
        public string formatter { get; set; }
    }

    public class Legend
    {
        public string orient { get; set; }
        public string left { get; set; }
        public string[] data { get; set; }
    }

    public class Series
    {
        public string name { get; set; }
        public string type { get; set; }
        public string radius { get; set; }
        public string[] center { get; set; }
        public Datum[] data { get; set; }
        public Itemstyle itemStyle { get; set; }
    }

    public class Itemstyle
    {
        public Emphasis emphasis { get; set; }
    }

    public class Emphasis
    {
        public int shadowBlur { get; set; }
        public int shadowOffsetX { get; set; }
        public string shadowColor { get; set; }
    }

    public class Datum
    {
        public int value { get; set; }
        public string name { get; set; }
    }

    public class Class1
    {
    }
}