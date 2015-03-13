using System;
using System.Collections.Generic;
using BlockSlideCore.Utilities;

namespace BlockSlideCore.DataStructures
{
    public static class NodeUtilities
    {
        public static void BreadthFirstSearch<T>(this Node<T> rootNode, Action<Node<T>> action )
        {
            var visited = new HashSet<Node<T>>();
            var queue = new Queue<Node<T>>();
            queue.Enqueue(rootNode);
            visited.Add(rootNode);

            while (queue.Count > 0)
            {
                var currentNode = queue.Dequeue();
                action(currentNode);

                currentNode.Neighbors.ForEach(neighbor =>
                {
                    if (!visited.Contains(neighbor))
                    {
                        queue.Enqueue(neighbor);
                        visited.Add(neighbor);
                    }
                });
            }
        }

        public static void DepthFirstSearch<T>(this Node<T> rootNode, Action<Node<T>> action)
        {
            var visited = new HashSet<Node<T>>();
            var stack = new Stack<Node<T>>();
            stack.Push(rootNode);
            visited.Add(rootNode);

            while (stack.Count > 0)
            {
                var currentNode = stack.Pop();
                action(currentNode);

                currentNode.Neighbors.ForEach(neighbor =>
                {
                    if (!visited.Contains(neighbor))
                    {
                        stack.Push(neighbor);
                        visited.Add(neighbor);
                    }
                });
            }
        }
    }
}
