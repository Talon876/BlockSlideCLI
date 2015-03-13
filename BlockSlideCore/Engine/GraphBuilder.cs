using System;
using System.Collections.Generic;
using System.Linq;
using BlockSlideCore.DataStructures;
using BlockSlideCore.Entities;

namespace BlockSlideCore.Engine
{
    public class GraphBuilder
    {
        private readonly List<Direction> mDirections = new List<Direction>
        {
            Direction.Up,
            Direction.Down,
            Direction.Left,
            Direction.Right,
        };

        public Node<Vector2> BuildGraph(Level level)
        {
            var validLocationCalculator = new ValidLocationCalculator();
            var validLocations = validLocationCalculator.BuildValidLocations(level).ToList();
            var validNodes = validLocations.Select(location => new Node<Vector2>(location)).ToList();

            validNodes.ForEach(currentNode =>
            {
                var neighbors = GetAllNeighbors(level, currentNode.Value)
                    .Select(neighborLocation =>
                        validNodes.FirstOrDefault(node => node.Value.Equals(neighborLocation)))
                    .ToList();
                neighbors.ForEach(currentNode.Neighbors.Add);
            });
            var rootNode = validNodes.FirstOrDefault(node => node.Value.Equals(level.StartLocation));

            return rootNode;
        }

        private IEnumerable<Vector2> GetAllNeighbors(Level level, Vector2 source)
        {
            var neighbors = mDirections.Select(direction =>
                level.MovementCalculator.CalculateNewLocation(level.LevelGrid, source, direction))
                .Where(neighbor => !neighbor.Equals(source)).Distinct()
                .ToList();
            return neighbors;
        } 
        
    }
}
