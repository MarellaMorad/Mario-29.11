using SplashKitSDK;

namespace MarioGame
{
    public class Player : GameObject, IDrawable
    {
        private Bitmap _playerBitmap; //, _DieBitmap; Add die bitmap later
        private double _dy, _dx, _gravity;
        private bool _jumping, _hasKey, _landed, _attract, _sing, _left;
        private Level _level;
        private Resize _resize;
        private Superpower _superpower;
        private Direction _direction;

        /// <summary>
        /// Default constructor of the player, responsible for initialising the fields, such as,
        /// setting the default player bitmap.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Player(string name, double x, double y) : base(name, x, y)
        {
            _playerBitmap = SplashKit.LoadBitmap(name, name + ".png");
            _dx = 0.2;
            _gravity = 0.003;
            _jumping = false;
            _hasKey = false;
            _resize = new Resize();
            _direction = new Direction();
            _landed = false;
            _attract = false;
            _sing = false;
        }

        /// <summary>
        /// Bitmap property used in other classes to get and set the displayed player bitmap
        /// </summary>
        public Bitmap Bitmap
        {
            get
            {
                return _playerBitmap;
            }
            set
            {
                _playerBitmap = value;
            }
        }

        /// <summary>
        /// Resize property applying the resize class which is responsible for player enlarge and shrink
        /// </summary>
        public Resize Resize
        {
            get
            {
                return _resize;
            }
        }

        /// <summary>
        /// Superpower property, used to get the player's currecnt superpower, and set the next one (next level)
        /// </summary>
        public Superpower Superpower
        {
            get
            {
                return _superpower;
            }
            set
            {
                _superpower = value;
            }
        }

        /// <summary>
        /// Level property, used to get and set the player's level (in game and door classes)
        /// </summary>
        public Level Level
        {
            get
            {
                return _level;
            }
            set
            {
                _level = value;
            }
        }

        /// <summary>
        /// Landed bool property, is used when the player lands on a platform to stop him from falling
        /// (Vertical collision class)
        /// </summary>
        public bool Landed
        {
            set
            {
                _landed = value;
            }
        }

        /// <summary>
        /// Attract property, used with the supermagnet super power to get and set the player's
        /// attraction status to the magnetic block
        /// </summary>
        public bool Attract
        {
            get
            {
                return _attract;
            }
            set
            {
                _attract = value;
            }
        }

        /// <summary>
        /// Left property, used to get and set the player's facing directoin to determine which
        /// bitmap to be displayed on the screen (works with the Resize class)
        /// </summary>
        public bool Left
        {
            get
            {
                return _left;
            }
            set
            {
                _left = value;
            }
        }

        /// <summary>
        /// Sing property, used in level 3, when the player has the power to break glass using his voice
        /// Used in Supervoice class
        /// </summary>
        public bool Sing
        {
            get
            {
                return _sing;
            }
            set
            {
                _sing = value;
            }
        }

        /// <summary>
        /// Moves the player in 1D (left or right only)
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="width"></param>
        public void Move(string dir, int width)
        {
            //making sure the player stays in the borders of the game
            if (dir == "left" && X > 0)
            {
                //checking which Bitmap to display
                if (_resize.Shrunk)
                {
                    _direction.FacingLeftShrunk(this);
                }
                else
                {
                    _direction.FacingLeft(this);
                }
                _left = true;
                X -= _dx;
            }
            else if (dir == "right" && X < width - SplashKit.BitmapWidth(_playerBitmap))
            {
                if (_resize.Shrunk)
                {
                    _direction.FacingRightShrunk(this);
                }
                else
                {
                    _direction.FacingRight(this);
                }
                _left = false;
                X += _dx;
            }
        }

        /// <summary>
        /// To allow the player to jump if he is not already jumping or has landed on a platform
        /// </summary>
        public void Jump()
        {
            if (!_jumping || _landed)
            {
                _dy = -1; //setting the player's vertical speed, which will allow for the projectile motion
                          //of the player jumping up and falling down
                _jumping = true; //set jump to true, which will be used in update method
                _landed = false; //only allow one jump
            }
        }

        /// <summary>
        /// This method will keep running throughout the game to update the vertical postion of the player
        /// </summary>
        /// <param name="ground"></param>
        public void Update(double ground)
        {
            //make the jumping suitable for both mario sizes (normal and shrunk)
            if (_resize.Shrunk)
            {
                ground += 35;
            }

            if (_jumping)
            {
                _dy += _gravity; //change the vertical speed by a factor of gravity
            }

            Y += _dy; //change the player's vertical position by the dy value

            if (Y > ground) //to prevent the player from falling below ground
            {
                Y = ground;
                _jumping = false;
            }
        }

        /// <summary>
        /// DY writeonly property used in the superjump class
        /// </summary>
        public double DY
        {
            set
            {
                _dy = value;
            }
        }

        /// <summary>
        /// bool property used to check if the player has the key of the level
        /// </summary>
        public bool HasKey
        {
            get
            {
                return _hasKey;
            }
            set
            {
                _hasKey = value;
            }
        }

        /// <summary>
        /// To print instructions for the user in console
        /// </summary>
        public string Instructions
        {
            get
            {
                string instructions;
                //the instructions includes infromation about the current level, superpower, and general infromation about how the game works
                instructions = Level.Description.ToUpper() + ":\n" + Superpower.Description +
                    "\nYOUR ROLE IS TO AVOID THE OBSTACLES, COLLECT THE KEY AND USE IT TO OPEN THE DOOR TO THE NEXT LEVEL\n\tENJOY!";

                return instructions;
            }
        }

        /// <summary>
        /// Implementation of the IDrawable interface Draw method to draw the player on the screen
        /// </summary>
        public void Draw()
        {
            SplashKit.DrawBitmap(_playerBitmap, X, Y);
        }

        /// <summary>
        /// Resets the player its starting postions
        /// Also resets the player's superpower status
        /// Sets the player back to being enlarge (default bitmap)
        /// </summary>
        public void Reset()
        {
            X = 0;
            Y = 467;
            _attract = false;
            _sing = false;
            _resize.Enlarge(this);
        }

        /// <summary>
        /// Calls the methods that will need to be run continuously
        /// </summary>
        /// <param name="ground"></param>
        public void Run(double ground)
        {
            Draw();
            Update(ground);
        }
    }
}
