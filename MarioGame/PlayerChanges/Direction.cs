using SplashKitSDK;

namespace MarioGame
{
    public class Direction
    {
        private Bitmap _leftBitmap, _rightBitmap, _leftShrunkBitmap, _rightShrunkBitmap;

        /// <summary>
        /// Direction default constructor, loads the four bitmaps
        /// </summary>
        public Direction()
        {
            _leftBitmap = SplashKit.LoadBitmap("mario_left", "mario_left.png");
            _leftShrunkBitmap = SplashKit.LoadBitmap("mario_shrunk_left", "mario_shrunk_left.png");
            _rightBitmap = SplashKit.LoadBitmap("mario", "mario.png");
            _rightShrunkBitmap = SplashKit.LoadBitmap("mario_shrunk", "mario_shrunk.png");
        }

        /// <summary>
        /// LeftBitmap readonly property, retunrs the player bitmap facing left (enlarged)
        /// </summary>
        public Bitmap LeftBitmap
        {
            get
            {
                return _leftBitmap;
            }
        }

        /// <summary>
        /// LeftShrunkBitmap readonly property, retunrs the player bitmap facing left (shrunk)
        /// </summary>
        public Bitmap LeftShrunkBitmap
        {
            get
            {
                return _leftShrunkBitmap;
            }
        }

        /// <summary>
        /// RightBitmap readonly property, retunrs the player bitmap facing right (enlarged)
        /// </summary>
        public Bitmap RightBitmap
        {
            get
            {
                return _rightBitmap;
            }
        }

        /// <summary>
        /// RightShrunkBitmap readonly property, retunrs the player bitmap facing right (shrunk)
        /// </summary>
        public Bitmap RightShrunkBitmap
        {
            get
            {
                return _rightShrunkBitmap;
            }
        }

        /// <summary>
        /// checks if the player is not already facing left, and sets the player's bitmap to the left bitmap
        /// </summary>
        /// <param name="p"></param>
        public void FacingLeft(Player p)
        {
            if (!p.Left)
            {
                p.Bitmap = _leftBitmap;
            }
        }

        /// <summary>
        /// checks if the player is not already facing left, and sets the player's bitmap to the left bitmap (shrunk)
        /// </summary>
        /// <param name="p"></param>
        public void FacingLeftShrunk(Player p)
        {
            if (!p.Left)
            {
                p.Bitmap = _leftShrunkBitmap;
            }
        }

        /// <summary>
        ///  checks if the player is not already facing right, and sets the player's bitmap to the right bitmap
        /// </summary>
        /// <param name="p"></param>
        public void FacingRight(Player p)
        {
            if (p.Left)
            {
                p.Bitmap = _rightBitmap;
            }
        }

        /// <summary>
        ///  checks if the player is not already facing right, and sets the player's bitmap to the right bitmap (shrunk)
        /// </summary>
        /// <param name="p"></param>
        public void FacingRightShrunk(Player p)
        {
            if (p.Left)
            {
                p.Bitmap = _rightShrunkBitmap;
            }
        }
    }
}
