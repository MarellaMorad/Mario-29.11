using SplashKitSDK;

namespace MarioGame
{
    /// <summary>
    /// Abstract class, specific block types inherit from it
    /// </summary>
    public abstract class Block : GameObject, IDrawable
    {
        private Bitmap _blockBitmap;
        private string _type;
        private CollisionProcessor _collision;

        /// <summary>
        /// Block defaul constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Block(string name, double x, double y)  : base(name, x, y)
        {
            _collision = new CollisionProcessor();
        }

        /// <summary>
        /// Bitmap property
        /// </summary>
        public Bitmap Bitmap
        {
            get
            {
                return _blockBitmap;
            }
            set
            {
                _blockBitmap = value;
            }
        }

        /// <summary>
        /// to distinguise between the different types of blocks, set in child classes
        /// </summary>
        public string Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
            }
        }

        /// <summary>
        /// Runs the collision processor to check for collisions between the player and a block
        /// </summary>
        /// <param name="p"></param>
        public void PlayerBlockCollision(Player p)
        {
            _collision.Check(p, this);
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
