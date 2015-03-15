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
            return BuildGraph(level.LevelGrid, level.StartLocation, level.MovementCalculator);
        }

        public Node<Vector2> BuildGraph(Grid<TileType> grid, Vector2 startLocation, IMovementCalculator movementCalculator)
        {
            var validLocationCalculator = new ValidLocationCalculator();
            var validLocations =
                validLocationCalculator.BuildValidLocations(grid, startLocation, movementCalculator).ToList();
            var validNodes = validLocations.Select(location => new Node<Vector2>(location)).ToList();

            validNodes.ForEach(currentNode =>
            {
                var neighbors = GetAllNeighbors(grid, movementCalculator, currentNode.Value)
                    .Select(neighborLocation =>
                        validNodes.FirstOrDefault(node => node.Value.Equals(neighborLocation)))
                    .ToList();
                neighbors.ForEach(currentNode.Neighbors.Add);
            });
            var rootNode = validNodes.FirstOrDefault(node => node.Value.Equals(startLocation));

            return rootNode;
        }

        private IEnumerable<Vector2> GetAllNeighbors(Grid<TileType> grid, IMovementCalculator movementCalculator,
            Vector2 source)
        {
            var neighbors = mDirections.Select(direction =>
                movementCalculator.CalculateNewLocation(grid, source, direction))
                .Where(neighbor => !neighbor.Equals(source)).Distinct()
                .ToList();
            return neighbors;
        }

        private IEnumerable<Vector2> GetAllNeighbors(Level level, Vector2 source)
        {
            return GetAllNeighbors(level.LevelGrid, level.MovementCalculator, source);
        } 
        
    }
}
