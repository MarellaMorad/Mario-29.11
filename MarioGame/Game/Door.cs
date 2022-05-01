using SplashKitSDK;

namespace MarioGame
{
    public class Door : GameObject, IDrawable
    {
        private Bitmap _doorBitmap;
        private Level _nextLevel;
        private Superpower _nextSuperpower;
        private bool _passedLevel;

        /// <summary>
        /// Default constructor for a door
        /// </summary>
        /// <param name="name"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Door(string name, double x, double y) : base(name, x, y)
        {
            _doorBitmap = SplashKit.LoadBitmap(name, "door.png");
            X = x;
            Y = y;
            _passedLevel = false;
        }

        /// <summary>
        /// Constructor with next level parameter, supplied from Game class
        /// </summary>
        /// <param name="name"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="nextLevel"></param>
        public Door(string name, double x, double y, Level nextLevel) : this(name, x, y)
        {
            _nextLevel = nextLevel;
        }

        /// <summary>
        /// Constructor with superpower parameter, supplied from Game class
        /// </summary>
        /// <param name="name"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="nextLevel"></param>
        /// <param name="superpower"></param>
        public Door(string name, double x, double y, Level nextLevel, Superpower superpower) : this(name, x, y, nextLevel)
        {
            _nextSuperpower = superpower;
        }

        /// <summary>
        /// Bitmap proeprty from IDrawable interface
        /// </summary>
        public Bitmap Bitmap
        {
            get
            {
                return _doorBitmap;
            }
        }

        /// <summary>
        /// Checks if the player has arrived at the door and has the key to open it
        /// </summary>
        /// <param name="p"></param>
        public void ArrivedAtDoor(Player p)
        {
            Rectangle playerRec = p.Bitmap.BoundingRectangle(p.X, p.Y); //getting the bounding rectangle of the player
            Rectangle doorRec = _doorBitmap.BoundingRectangle(X, Y); //getting the bounding rectangle of the door

            //checks if the player is at the door and has the key
            if (SplashKit.RectanglesIntersect(playerRec, doorRec) && p.HasKey)
            {
                _passedLevel = true; //turns passed level to true
                SetLevel(p); //sets the player's level to the next level
                p.Reset(); //resets the player position and appearance
                p.HasKey = false; //sets HasKey to false
            }
        }

        /// <summary>
        /// Checks if the player has successfully passed the level and moves him to the next level
        /// also sets the new superpower
        /// </summary>
        /// <param name="p"></param>
        public void SetLevel(Player p)
        {
            if (_passedLevel)
            {
                p.Level = _nextLevel;
                p.Superpower = _nextSuperpower;
            }
        }

        /// <summary>
        /// Implementation of the IDrawable interface Draw method to draw the door on the screen
        /// </summary>
        public void Draw()
        {
            SplashKit.DrawBitmap(Bitmap, X, Y);
        }
    }
}
