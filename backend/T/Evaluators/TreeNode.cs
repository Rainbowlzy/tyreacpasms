using System.Collections.Generic;

namespace T.Evaluators
{
    public class TreeNode<T> where T : class, new()
    {
        public string title { get; set; }
        public T data { get; set; }
        public List<TreeNode<T>> children { get; set; }
    }
}