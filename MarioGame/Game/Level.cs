using SplashKitSDK;
using System.Collections.Generic;

namespace MarioGame
{
    public class Level : GameObject, IDrawable
    {
        //illustration fields
        private Bitmap _backgroundBitmap;
        private List<Block> _blocks;
        private Door _door;
        private Key _key;
        private string _desc;

        /// <summary>
        /// Level default constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="desc"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Level(string name, string desc, double x, double y) : base(name, x, y)
        {
            _backgroundBitmap = SplashKit.LoadBitmap(name, name + ".png");
            _blocks = new List<Block>();
            _desc = desc;
        }

        /// <summary>
        /// Key propetry (used in game to set the key of the level)
        /// </summary>
        public Key Key
        {
            set
            {
                _key = value;
            }
        }

        /// <summary>
        /// Bitmap property from IDrawable interface
        /// </summary>
        public Bitmap Bitmap
        {
            get
            {
                return _backgroundBitmap;
            }
        }

        /// <summary>
        /// to add blocks to the list of blocks to be drawn on the screen
        /// </summary>
        /// <param name="block"></param>
        public void AddBlock(Block block)
        {
            _blocks.Add(block);
        }

        /// <summary>
        /// Blocks property (used in Resize class)
        /// </summary>
        public List<Block> Blocks
        {
            get
            {
                return _blocks;
            }
        }

        /// <summary>
        /// Retruns the description of the level
        /// </summary>
        public string Description
        {
            get
            {
                return _desc.ToUpper();
            }
        }

        /// <summary>
        /// Door propetry (used in game to set the door of the level)
        /// </summary>
        public Door Door
        {
            set
            {
                _door = value;
            }
        }

        /// <summary>
        /// Calls the PlayerBlockCollision method for each of the blocks in the level
        /// </summary>
        /// <param name="p"></param>
        public void CollisionsResponder(Player p)
        {
            foreach (Block block in _blocks)
            {
                block.PlayerBlockCollision(p);
            }
        }

        /// <summary>
        /// Draws the level, including, the background, blocks, door and key
        /// </summary>
        public void Draw()
        {
            SplashKit.DrawBitmap(_backgroundBitmap, X, Y);
            if (_blocks != null)
            {
                foreach (Block block in _blocks)
                {
                    block.Draw();
                }
            }
            if (_door != null)
            {
                _door.Draw();
            }
            if (_key != null)
            {
                if (!_key.Disappear)
                {
                    _key.Draw();
                }
            }
        }

        /// <summary>
        /// Calls the methods that will need to be run continuously
        /// </summary>
        /// <param name="p"></param>
        public void Run(Player p)
        {
            Draw();
            CollisionsResponder(p);
            if (_key != null)
            {
                _key.TakeKey(p);
            }
            if (_door != null)
            {
                _door.ArrivedAtDoor(p);
            }
        }
    }
}
