namespace T.Models.ECharts.Bar
{
    public class EChartsBarModel
    {
        public Legend legend { get; set; }
        public Tooltip tooltip { get; set; }
        public Dataset dataset { get; set; }
        public Xaxis xAxis { get; set; }
        public Yaxis yAxis { get; set; }
        public Series[] series { get; set; }
    }

    public class Legend
    {
    }

    public class Tooltip
    {
    }

    public class Dataset
    {
        public string[][] source { get; set; }
    }

    public class Xaxis
    {
        public string type { get; set; }
    }

    public class Yaxis
    {
    }

    public class Series
    {
        public string type { get; set; }
    }

    public class Bar
    {
    }
}