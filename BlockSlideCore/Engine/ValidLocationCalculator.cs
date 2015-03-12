using System.Collections.Generic;
using System.Linq;
using BlockSlideCore.DataStructures;
using BlockSlideCore.Entities;
using BlockSlideCore.Utilities;

namespace BlockSlideCore.Engine
{
    public class ValidLocationCalculator
    {
        private readonly List<Direction> mDirections = new List<Direction>
        {
            Direction.Up,
            Direction.Down,
            Direction.Left,
            Direction.Right,
        };

        public IEnumerable<Vector2> BuildValidLocations(Level level)
        {
            var visitedLocations = new List<Node<Vector2>>();
            var startNode = new Node<Vector2>(level.StartLocation);
            visitedLocations.Add(startNode);

            while (visitedLocations.Any(node => !node.Visited))
            {
                var currentNode = visitedLocations.First(node => !node.Visited);
                mDirections.Select(direction => level.MovementCalculator.CalculateNewLocation(level.LevelGrid, currentNode.Value, direction))
                    .Where(neighbor => !visitedLocations.Contains(new Node<Vector2>(neighbor)))
                    .ForEach(neighbor => visitedLocations.Add(new Node<Vector2>(neighbor)));
                currentNode.Visited = true;
            }
            return visitedLocations.Select(node => node.Value);
        }
    }
}
