using BlockSlideCore.DataStructures;
using BlockSlideCore.Engine;
using BlockSlideCore.Levels;

namespace BlockSlideCore.Entities
{
    public class Level
    {
        private Grid<TileType> mLevelGrid;
        private readonly ILevelBuilder mLevelBuilder;
        private readonly IMovementCalculator mMovementCalculator;
        private readonly Player mPlayer;

        public Level(int level) :
            this(level, new CampaignLevelBuilder(), new MovementCalculator())
        {
        }

        public Level(int level, ILevelBuilder levelBuilder) :
            this(level, levelBuilder, new MovementCalculator())
        {
        }

        public Level(int level, ILevelBuilder levelBuilder, IMovementCalculator movementCalculator)
        {
            LevelNumber = level;
            mLevelBuilder = levelBuilder;
            mMovementCalculator = movementCalculator;

            SetupLevel();

            mPlayer = new Player {Location = StartLocation.Clone()};
        }

        public int LevelNumber { get; private set; }

        public Grid<TileType> LevelGrid
        {
            get { return mLevelGrid; }
        }

        private Vector2 mStart;

        public Vector2 StartLocation
        {
            get { return mStart ?? (mStart = FindTile(TileType.Start)); }
        }

        private Vector2 mFinish;

        public Vector2 FinishLocation
        {
            get { return mFinish ?? (mFinish = FindTile(TileType.Finish)); }
        }

        public Vector2 PlayerLocation
        {
            get { return mPlayer.Location; }
        }

        public IMovementCalculator MovementCalculator
        {
            get { return mMovementCalculator; }
        }

        public Vector2 Move(Direction direction)
        {
            var newLocation = mMovementCalculator.CalculateNewLocation(mLevelGrid, mPlayer.Location.Clone(), direction);
            mPlayer.Location = newLocation.Clone();
            return newLocation.Clone();
        }

        private Vector2 FindTile(TileType tileType)
        {
            Vector2 vector = null;
            mLevelGrid.ForEach((x, y, value) =>
            {
                if (value == tileType)
                {
                    vector = new Vector2(x, y);
                }
            });
            return vector;
        }

        private void SetupLevel()
        {
            mLevelGrid = mLevelBuilder.CreateLevel(LevelNumber);
        }

        public void SaveToFile(string fileLocation)
        {
            mLevelGrid.SaveToFile(fileLocation, tile => tile.ToCharacter().ToString());
        }

    }
}
