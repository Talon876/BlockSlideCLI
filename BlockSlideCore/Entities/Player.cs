using BlockSlideCore.DataStructures;

namespace BlockSlideCore.Entities
{
    public class Player
    {
        public Player()
        {
            mLocation = Vector2.Zero;
        }

        private readonly Vector2 mLocation;

        public Vector2 Location
        {
            get { return mLocation; }
            set
            {
                mLocation.X = value.X;
                mLocation.Y = value.Y;
            } 
            
        }
    }
}
