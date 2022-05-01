using SplashKitSDK;

namespace MarioGame
{
    public class Resize
    {
        private bool _shrunk;
        private Direction _direction;

        /// <summary>
        /// Resize Default constructor
        /// </summary>
        public Resize()
        {
            _shrunk = false;
            _direction = new Direction();
        }

        /// <summary>
        /// Shrink method, to shrink the player when called (player presses S)
        /// </summary>
        /// <param name="p"></param>
        public void Shrink(Player p)
        {
            if (!_shrunk) //checks if the player is not already shrunk
            {
                //checks the direction the player is facing to set the corresponding bitmap
                if (p.Left)
                {
                    p.Bitmap = _direction.LeftShrunkBitmap;
                }
                else
                {
                    p.Bitmap = _direction.RightShrunkBitmap;
                }
                p.Y += 35; //lower the player by 35 (difference in height between englarged and shrunk bitmaps)
                _shrunk = true; //set shrunk to true
            }
        }

        /// <summary>
        /// Boolean Shrunk property, returns _shrunk bool
        /// </summary>
        public bool Shrunk
        {
            get
            {
                return _shrunk;
            }
        }

        /// <summary>
        /// Enlarge method, to enlarge the player when called (player presses E)
        /// </summary>
        /// <param name="p"></param>
        public void Enlarge(Player p)
        {
            if (_shrunk && !BlockEnlarge(p)) //checks if the player is shrunk and enlarging is not blocked
            {
                //checks the direction the player is facing to set the corresponding bitmap
                if (p.Left)
                {
                    p.Bitmap = _direction.LeftBitmap;
                }
                else
                {
                    p.Bitmap = _direction.RightBitmap;
                }

                p.Y -= 35; //rises the player by 35 pixels
                _shrunk = false;
            }
        }

        /// <summary>
        /// Checks if the player will collide with a block if requests enlrage and blocks it if that's the case
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public bool BlockEnlarge(Player p)
        {
            foreach (Block block in p.Level.Blocks)
            {
                Rectangle playerEnlargeRec = SplashKit.RectangleFrom(p.X, p.Y - 36, p.Bitmap.Width, _direction.RightBitmap.Height);
                Rectangle blockRec = block.Bitmap.BoundingRectangle(block.X, block.Y); //get the bounding rectangle of the block

                //checks if the enlarged player will intersect with a block (before allowing enlarge)
                if (SplashKit.RectanglesIntersect(playerEnlargeRec, blockRec))
                {
                    //if the player is already shrunk, and an enlrage will lead to a collision
                    if (_shrunk && (SplashKit.RectangleTop(playerEnlargeRec) < SplashKit.RectangleBottom(blockRec) || SplashKit.RectangleTop(playerEnlargeRec) < SplashKit.RectangleTop(blockRec)))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
