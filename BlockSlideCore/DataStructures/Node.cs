namespace BlockSlideCore.DataStructures
{
    public class Node<T>
    {
        public Node(T value)
        {
            Value = value;
        }

        public T Value { get; private set; }

        public bool Visited { get; set; }

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
    }
}
