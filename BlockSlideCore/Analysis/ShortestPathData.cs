using System.Collections.Generic;
using System.Linq;
using BlockSlideCore.DataStructures;

namespace BlockSlideCore.Analysis
{
    public class ShortestPathData
    {
        private readonly Dictionary<Vector2, int> mDistanceMap;
        private readonly Dictionary<Vector2, Vector2> mPreviousMap;

        public ShortestPathData(Dictionary<Vector2, int> distanceMap, Dictionary<Vector2, Vector2> previousMap)
        {
            mDistanceMap = distanceMap;
            mPreviousMap = previousMap;
        }

        public Dictionary<Vector2, int> DistanceMap
        {
            get { return mDistanceMap; }
        }

        public Dictionary<Vector2, Vector2> PreviousMap
        {
            get { return mPreviousMap; }
        }

        public List<Vector2> GetPath(Vector2 goal)
        {
            var path = new Stack<Vector2>();
            var current = goal;
            while (mPreviousMap[current] != null)
            {
                path.Push(current);
                current = mPreviousMap[current];
            }
            return path.ToList();
        }
    }
}
