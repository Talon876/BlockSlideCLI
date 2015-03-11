using System;
using System.Collections.Generic;
using System.Linq;
using BlockSlideCore.DataStructures;
using BlockSlideCore.Entities;
using BlockSlideCore.Utilities;

namespace BlockSlideCLI
{
    public class ValidLocationCalculator
    {
        private readonly List<Vector2> mDirections = new List<Vector2>
        {
            Direction.Up.ToVector(),
            Direction.Down.ToVector(),
            Direction.Left.ToVector(),
            Direction.Right.ToVector(),
        };

        public IEnumerable<Vector2> BuildValidLocations(Vector2 startLocation,
            Func<Vector2, Vector2, Vector2> getNeighborsFunc)
        {
            var visitedLocations = new List<SimpleNode>();
            var startNode = new SimpleNode(startLocation);
            visitedLocations.Add(startNode);

            while (visitedLocations.Any(node => !node.Visited))
            {
                var currentNode = visitedLocations.First(node => !node.Visited);
                mDirections.Select(direction => getNeighborsFunc(currentNode.Location, direction))
                    .Where(neighbor => !visitedLocations.Contains(new SimpleNode(neighbor)))
                    .ForEach(neighbor => visitedLocations.Add(new SimpleNode(neighbor)));
                currentNode.Visited = true;
            }
            return visitedLocations.Select(node => node.Location);
        }

        private class SimpleNode
        {
            public SimpleNode(Vector2 source)
            {
                Location = source;
            }
            public Vector2 Location { get; private set; }
            public bool Visited { get; set; }

            public override bool Equals(object obj)
            {
                if (obj == null || GetType() != obj.GetType())
                {
                    return false;
                }
                var nodeObj = (SimpleNode)obj;
                return Location.Equals(nodeObj.Location);
            }

            public override int GetHashCode()
            {
                return Location.GetHashCode();
            }
        }
    }
}
