using System.Collections.Generic;
using System.Linq;

namespace BlockSlideCore.DataStructures
{
    public class Node<T>
    {
        public Node(T value)
        {
            Value = value;
            Neighbors = new List<Node<T>>();
        }

        public T Value { get; private set; }

        public bool Visited { get; set; }

        public IList<Node<T>> Neighbors { get; private set; }
        
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            var nodeObj = (Node<T>)obj;
            return nodeObj.Value.Equals(Value);
        }

        public override string ToString()
        {
            return string.Format("[{0}]->{{{1}}}", Value, string.Join(",", Neighbors.Select(neighbor => neighbor.Value)));
        }
    }
}
