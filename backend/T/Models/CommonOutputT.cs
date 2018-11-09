namespace T.Models
{
    public class CommonOutputT<T>
    {
        public bool success
        {
            get;
            set;
        }
        public string message
        {
            get;
            set;
        }
        public T data
        {
            get;
            set;
        }
    }
}