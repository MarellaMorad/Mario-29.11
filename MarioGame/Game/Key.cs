using SplashKitSDK;

namespace MarioGame
{
    public class Key : GameObject, IDrawable
    {
        private Bitmap _keyBitmap;
        private bool _disappear;

        /// <summary>
        /// Default Key Constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Key(string name, double x, double y) : base(name, x, y)
        {
            _keyBitmap = SplashKit.LoadBitmap("key", "key.png");
        }

        /// <summary>
        /// Bitmap property from IDrawable interface
        /// </summary>
        public Bitmap Bitmap
        {
            get
            {
                return _keyBitmap;
            }
        }

        /// <summary>
        /// Checks if the player intersect with the key, if so allows the player to take the key
        /// </summary>
        /// <param name="p"></param>
        public void TakeKey(Player p)
        {
            Rectangle playerRec = p.Bitmap.BoundingRectangle(p.X, p.Y); //getting the bounding rectangle of the player
            Rectangle keyRec = Bitmap.BoundingRectangle(X, Y); //getting the bounding rectangle of the key
            if (SplashKit.RectanglesIntersect(playerRec, keyRec)) //check if the player intersects with the key
            {
                _disappear = true; //to remove the key from the display
                p.HasKey = true; //sets the HasKey property of the player to true
            }
        }

        /// <summary>
        /// Property to return the _disapper boolean
        /// </summary>
        public bool Disappear
        {
            get
            {
                return _disappear;
            }
        }

        /// <summary>
        /// Implementation of the IDrawable interface Draw method to draw the block on the screen
        /// </summary>
        public void Draw()
        {
            SplashKit.DrawBitmap(Bitmap, X, Y);
        }
    }
}
