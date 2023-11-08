///Blocks Family Classes
//Block
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

//GlassBlock
using SplashKitSDK;

namespace MarioGame
{
	public class GlassBlock : Block
	{
		/// <summary>
		/// GlassBlock Default constructor
		/// </summary>
		/// <param name="name"></param>
		/// <param name="x"></param>
		/// <param name="y"></param>
		public GlassBlock(string name, double x, double y) : base(name, x, y)
		{
			Type = "glass"; //setting the type of the block
			Bitmap = SplashKit.LoadBitmap(name, "glassBlock.png");
			X = x;
			Y = y;
		}
	}
}

//LavaBlock
using SplashKitSDK;

namespace MarioGame
{
	public class LavaBlock : Block
	{
		/// <summary>
		/// LavaBlock Default constructor
		/// </summary>
		/// <param name="name"></param>
		/// <param name="x"></param>
		/// <param name="y"></param>
		public LavaBlock(string name, double x, double y) : base(name, x, y)
		{
			Type = "lava";
			Bitmap = SplashKit.LoadBitmap(name, "lavaBlock.png");
			X = x;
			Y = y;
		}
	}
}

//MagneticBlock
using SplashKitSDK;

namespace MarioGame
{
	public class MagneticBlock : Block
	{
		/// <summary>
		/// MagneticBlock Default constructor
		/// </summary>
		/// <param name="name"></param>
		/// <param name="x"></param>
		/// <param name="y"></param>
		public MagneticBlock(string name, double x, double y) : base(name, x, y)
		{
			Type = "magnet";
			Bitmap = SplashKit.LoadBitmap(name, "magneticBlock.png");
			X = x;
			Y = y;
		}
	}
}

//NormalBlock
using SplashKitSDK;

namespace MarioGame
{
	public class NormalBlock : Block
	{
		/// <summary>
		/// Normal Blcok Default Constructor
		/// </summary>
		/// <param name="name"></param>
		/// <param name="x"></param>
		/// <param name="y"></param>
		public NormalBlock(string name, double x, double y) : base(name, x, y)
		{
			Type = "normal";
			Bitmap = SplashKit.LoadBitmap(name, "normalBlock.png");
			X = x;
			Y = y;
		}
	}
}

//SpikedBlock
using SplashKitSDK;

namespace MarioGame
{
	public class SpikedBlock : Block
	{
		/// <summary>
		/// SpikedBlock Default Constructor
		/// </summary>
		/// <param name="name"></param>
		/// <param name="x"></param>
		/// <param name="y"></param>
		public SpikedBlock(string name, double x, double y) : base(name, x, y)
		{
			Type = "spiked";
			Bitmap = SplashKit.LoadBitmap(name, "spikedBlock.png");
			X = x;
			Y = y;
		}
	}
}

///Collisions Family Classes
//CollisionProcessor
using System.Collections.Generic;

namespace MarioGame
{
    public class CollisionProcessor : ICollision
    {
        private List<ICollision> _collisions = new List<ICollision>(); //to register the different collisions

        private VerticalCollision _verticalCollision = new VerticalCollision();
        private HorizontalCollision _horizontalCollision = new HorizontalCollision();

        /// <summary>
        /// Default CollisionProcessor constructor to register the different collisions
        /// </summary>
        public CollisionProcessor()
        {
            _collisions.Add(_verticalCollision);
            _collisions.Add(_horizontalCollision);
        }

        /// <summary>
        /// Runs the collisions check taking in the player and the block as parameters
        /// Implements the Check method from the ICollision interface
        /// </summary>
        /// <param name="p"></param>
        /// <param name="block"></param>
        public void Check(Player p, Block block)
        {
            foreach (ICollision c in _collisions)
            {
                c.Check(p, block);
            }
        }
    }
}

//HorizontalCollision
using SplashKitSDK;

namespace MarioGame
{
    public class HorizontalCollision : ICollision
    {
        public HorizontalCollision()
        {

        }

        /// <summary>
        /// Check for horizontal collisions between the player and a block using bounding rectangles
        /// Applying different treatments to different block types
        /// </summary>
        /// <param name="p"></param>
        /// <param name="block"></param>
        public void Check(Player p, Block block)
        {
            Rectangle playerRec = p.Bitmap.BoundingRectangle(p.X, p.Y); //getting the bounding rectangle of the player
            Rectangle blockRec = block.Bitmap.BoundingRectangle(block.X, block.Y); //get the bounding rectangle of the block
            Rectangle intersection = SplashKit.Intersection(playerRec, blockRec); //get the intersection rectangle and a save it as a rectangle
            if (intersection.Height > intersection.Width) //horizontal collision
            {
                //glass blocks break when the player collides with them horizontally
                if (block.Type == "glass" && p.Sing) //Sing is true when the player uses their superpower (level 3)
                {
                    block.Bitmap = SplashKit.LoadBitmap("brokenGlass", "brokenGlassBlock.png"); //change the block bitmap to broken glass
                    block.Type = "broken"; //change the type to broken
                    block.Y += 160; //lower the block to sit on the ground/platform
                    p.Sing = false; //turn sing to false
                }
                //Checking player intersection with the block from the LHS
                if (SplashKit.RectangleRight(playerRec) > SplashKit.RectangleLeft(blockRec) && SplashKit.RectangleRight(playerRec) < SplashKit.RectangleRight(blockRec))
                {
                    p.X -= intersection.Width; //subtracting the intersection width from the player's X coordinate
                }
                //Checking player intersection with the block from the RHS
                else
                {
                    p.X += intersection.Width; //adding the intersection width to the player's X coordinate
                }
            }
        }
    }
}

//VerticalCollision
using SplashKitSDK;

namespace MarioGame
{
    public class VerticalCollision : ICollision
    {
        public VerticalCollision()
        {
        }

        /// <summary>
        /// Check for vertical collisions between the player and a block using bounding rectangles
        /// Applying different treatments to different block types
        /// </summary>
        /// <param name="p"></param>
        /// <param name="block"></param>
        public void Check(Player p, Block block)
        {
            Rectangle playerRec = p.Bitmap.BoundingRectangle(p.X, p.Y); //getting the bounding rectangle of the player
            Rectangle blockRec = block.Bitmap.BoundingRectangle(block.X, block.Y); //get the bounding rectangle of the block
            Rectangle intersection = SplashKit.Intersection(playerRec, blockRec); //get the intersection rectangle and a save it as a rectangle
            if (intersection.Width > intersection.Height) //vertical collision
            {
                //Checking player intersection with the block from the bottom (players feet)
                if (SplashKit.RectangleBottom(playerRec) > SplashKit.RectangleTop(blockRec) && SplashKit.RectangleBottom(playerRec) < SplashKit.RectangleBottom(blockRec))
                {
                    if (block.Type != "magnet") //landing on magnetic blocks is not allowed
                    {
                        if (intersection.Height > 1)
                        {
                            p.Y -= intersection.Height; //adjust the player Y based on the intersection height
                            p.DY = 0; //stop the player from falling
                            p.Landed = true; //setting landed to true, the player has landed on the platfrom
                        }

                        //collision with a lava or spiked block will lead to resetting the player (player dies)
                        if (block.Type == "lava" || block.Type == "spiked")
                        {
                            p.Reset();
                        }
                    }
                }
                else //collision with a block from top (head of the player)
                {
                    p.Y += intersection.Height; //adjust the player Y based on the intersection height
                    //this collision with magneticBlocks in level two allows the player to be attrackted to the block and hang from it
                    //if the superpower is used (i.e. p.Attract is true)
                    if (block.Type == "magnet" && p.Attract)
                    {
                        p.Y = block.Y;
                        p.Attract = false; //if the player stops using the pwoer, he will fall (stops holding ctrl)
                    }
                }
            }
        }
    }
}

//ICollision
namespace MarioGame
{
    public interface ICollision
    {
        //provides the collision check method to be used in CollisionProcessor, Horizontal and Vertical Collision classes
        public abstract void Check(Player p, Block block);
    }
}

///Game Family Classes
//Door
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

//Game
using SplashKitSDK;
using System;

namespace MarioGame
{
    //game class applying the singleton pattern
    class Game
    {
        private static Game _instance; //to create a single instance of the game
        //objects for the contruction of the game
        int windowWidth;
        double ground;
        Player player;
        Level startWindow, exitWindow, levelOne, levelTwo, levelThree;
        Superpower levelOneSuper, levelTwoSuper, levelThreeSuper;
        Key levelOneKey, levelTwoKey, levelThreeKey;
        Door doorOne, doorTwo, doorThree;
        Block NormBlock1_1, NormBlock1_2, NormBlock1_3, NormBlock1_4, NormBlock1_5;
        Block LavaBlock2_0, LavaBlock2_1, LavaBlock2_2;
        Block NormBlock2_1, NormBlock2_2, NormBlock2_3, NormBlock2_4, NormBlock2_5, NormBlock2_6, NormBlock2_7, NormBlock2_8, NormBlock2_9;
        Block MagBlock2_1, MagBlock2_2, MagBlock2_3;
        Block GlassBlock3_1, GlassBlock3_2, GlassBlock3_3;
        Block NormBlock3_1, NormBlock3_2, NormBlock3_3, NormBlock3_4, NormBlock3_5, NormBlock3_6, NormBlock3_7, NormBlock3_8;
        Block SpikeBlock3_1, SpikeBlock3_2;

        /// <summary>
        /// Game defaul constructor applying the singleton design patters (cannot be instantiated outside Game class)
        /// </summary>
        private Game()
        {
        }

        /// <summary>
        /// Used to allow creating one instance only of the game class (i.e. singleton)
        /// If an instance hasn't been created, it creates and returns it
        /// If it has been created, it will return the created one (won't create a new one)
        /// </summary>
        /// <returns></returns>
        public static Game getInstance()
        {
            if (_instance == null)
            {
                _instance = new Game();
            }

            return _instance;
        }

        /// <summary>
        /// Holds the construction and instantiation of the game (player, levels and their objects)
        /// </summary>
        public void CreateGame()
        {
            windowWidth = 800;
            //creating the player
            player = new Player("mario", 0, 467);
            ground = SplashKit.WindowHeight("Mario Game") - player.Bitmap.Height - 25;

            //Creating start window
            startWindow = new Level("startWindow", "The start window", 0, 0);
            player.Level = startWindow; //setting the player's level to the start window

            //Creating Level 1
            levelOne = new Level("levelOne", "Level One - Easy", 0, 0);
            levelOneSuper = new SuperJump("Super jump"); //level one's super power is the super jump

            //creating the construction blocks of level 1 (All normal blocks - easy level)
            NormBlock1_1 = new NormalBlock("norm1.1", 150, 440);
            NormBlock1_2 = new NormalBlock("norm1.2", 400, 400);
            NormBlock1_3 = new NormalBlock("norm1.3", 650, 225);
            NormBlock1_4 = new NormalBlock("norm1.4", 200, 225);
            NormBlock1_5 = new NormalBlock("norm1.5", 0, 125);

            //Adding the blocks to the level to allow the player to interact with them
            levelOne.AddBlock(NormBlock1_1);
            levelOne.AddBlock(NormBlock1_2);
            levelOne.AddBlock(NormBlock1_3);
            levelOne.AddBlock(NormBlock1_4);
            levelOne.AddBlock(NormBlock1_5);

            //creating and adding the key to level 1
            levelOneKey = new Key("levelOneKey", 670, 170);
            levelOne.Key = levelOneKey;

            //Creatig Level 2
            levelTwo = new Level("levelTwo", "Level Two - Medium", 0, 0);
            levelTwoSuper = new SuperMagnet("Super Magnet");

            //creating the construction blocks of level 2 (lava, magnetic and normal blocks - medium level)
            LavaBlock2_0 = new LavaBlock("lava2.0", 135, 565);
            LavaBlock2_1 = new LavaBlock("lava2.1", 190, 565);
            LavaBlock2_2 = new LavaBlock("lava2.2", 650, 565);
            NormBlock2_1 = new NormalBlock("norm2.1", 700, 420);
            NormBlock2_2 = new NormalBlock("norm2.2", 0, 300);
            NormBlock2_3 = new NormalBlock("norm2.3", 150, 300);
            NormBlock2_4 = new NormalBlock("norm2.4", 0, 125);
            NormBlock2_5 = new NormalBlock("norm2.5", 620, 168);
            NormBlock2_6 = new NormalBlock("norm2.6", 700, 30);
            NormBlock2_7 = new NormalBlock("norm2.7", 300, 300);
            NormBlock2_8 = new NormalBlock("norm2.7", 470, 168);
            NormBlock2_9 = new NormalBlock("norm2.7", 770, 168);
            MagBlock2_1 = new MagneticBlock("mag2.1", 65, 332);
            MagBlock2_2 = new MagneticBlock("mag2.2", 470, 200);
            MagBlock2_3 = new MagneticBlock("mag2.3", 120, 0);

            //Adding the blocks to the level to allow the player to interact with them
            levelTwo.AddBlock(LavaBlock2_0);
            levelTwo.AddBlock(LavaBlock2_1);
            levelTwo.AddBlock(LavaBlock2_2);
            levelTwo.AddBlock(NormBlock2_1);
            levelTwo.AddBlock(NormBlock2_2);
            levelTwo.AddBlock(NormBlock2_3);
            levelTwo.AddBlock(NormBlock2_4);
            levelTwo.AddBlock(NormBlock2_5);
            levelTwo.AddBlock(NormBlock2_6);
            levelTwo.AddBlock(NormBlock2_7);
            levelTwo.AddBlock(NormBlock2_8);
            levelTwo.AddBlock(NormBlock2_9);
            levelTwo.AddBlock(MagBlock2_1);
            levelTwo.AddBlock(MagBlock2_2);
            levelTwo.AddBlock(MagBlock2_3);

            //creating and adding the key to level 2
            levelTwoKey = new Key("levelTwoKey", 725, 80);
            levelTwo.Key = levelTwoKey;

            //Creating level 3
            levelThree = new Level("levelThree", "Level Three - Hard", 0, 0);
            levelThreeSuper = new SuperVoice("Super Voice");

            //creating the construction blocks of level 3 (glass, spiked and normal blocks - hard level)
            GlassBlock3_1 = new GlassBlock("glass3.1", 200, 390);
            GlassBlock3_2 = new GlassBlock("glass3.2", 650, 172);
            GlassBlock3_3 = new GlassBlock("glass3.3", 225, -55);
            NormBlock3_1 = new NormalBlock("norm2.4", 650, 125);
            NormBlock3_2 = new NormalBlock("norm3.2", 650, 460);
            NormBlock3_3 = new NormalBlock("norm3.3", 530, 352);
            NormBlock3_4 = new NormalBlock("norm3.4", 354, 352);
            NormBlock3_5 = new NormalBlock("norm3.5", 150, 250);
            NormBlock3_6 = new NormalBlock("norm3.6", 0, 250);
            NormBlock3_7 = new NormalBlock("norm3.7", 120, 125);
            NormBlock3_8 = new NormalBlock("norm3.8", 380, 80);
            SpikeBlock3_1 = new SpikedBlock("spike3.1", 334, 317);
            SpikeBlock3_2 = new SpikedBlock("spike3.2", 505, 317);

            //Adding the blocks to the level to allow the player to interact with them
            levelThree.AddBlock(GlassBlock3_1);
            levelThree.AddBlock(GlassBlock3_2);
            levelThree.AddBlock(GlassBlock3_3);
            levelThree.AddBlock(NormBlock3_1);
            levelThree.AddBlock(NormBlock3_2);
            levelThree.AddBlock(NormBlock3_3);
            levelThree.AddBlock(NormBlock3_4);
            levelThree.AddBlock(NormBlock3_5);
            levelThree.AddBlock(NormBlock3_6);
            levelThree.AddBlock(NormBlock3_7);
            levelThree.AddBlock(NormBlock3_8);
            levelThree.AddBlock(SpikeBlock3_1);
            levelThree.AddBlock(SpikeBlock3_2);

            //creating and adding the key to level 3
            levelThreeKey = new Key("levelThreeKey", 700, 500);
            levelThree.Key = levelThreeKey;

            //creating the exit/finish window
            exitWindow = new Level("exitWindow", "The exit window", 0, 0);

            //creating and adding the door to level 1
            //passing in level two as the next level, and levelTwoSuper as the next superpower
            doorOne = new Door("doorOne", 0, 0, levelTwo, levelTwoSuper);
            levelOne.Door = doorOne;

            //creating and adding the door to level 2
            //passing in level three as the next level, and levelThreeSuper as the next superpower
            doorTwo = new Door("doorTwo", 0, 0, levelThree, levelThreeSuper);
            levelTwo.Door = doorTwo;

            //creating and adding the door to level 3
            //passing in the exit window as the next level
            doorThree = new Door("doorTwo", 700, 0, exitWindow);
            levelThree.Door = doorThree;

            Console.WriteLine("WELCOME TO SUPER MARIO 11.29\nPRESS ENTER TO START\nMOVING KEYS:\n\tARROWS TO MOVE LEFT AND RIGHT" +
                "\n\tSPACE OR UP ARROW TO JUMP\n\tS KEY TO SHRINK\n\tE KEY TO ENLARGE\n\tCTRL TO USE YOUR SUPERPOWER\nPRESS I FOR LEVEL INSTRUCTIONS");
        }

        /// <summary>
        /// Handles keyboard input from the user
        /// will be run in the main loop of the game
        /// </summary>
        public void Run()
        {
            //Checking if the player's current level is the start window
            //if the user presses enter, move to the first level
            if (player.Level == startWindow && SplashKit.KeyTyped(KeyCode.ReturnKey))
            {
                player.Reset();
                player.Level = levelOne;
                player.Superpower = levelOneSuper;
            }

            //Checking if the player's current level is the exit window
            //if the user presses enter, exit the game
            if (player.Level == exitWindow && SplashKit.KeyTyped(KeyCode.ReturnKey))
            {
                Console.WriteLine("\nNICE WORK, GOOD BYE!");
                Environment.Exit(0);
            }

            //jump if the user presses Space or up key
            if (SplashKit.KeyTyped(KeyCode.SpaceKey) || SplashKit.KeyTyped(KeyCode.UpKey))
            {
                player.Jump();
                //if the player presses ctrl left or right (check if they are in level one)
                if ((SplashKit.KeyDown(KeyCode.LeftCtrlKey) || SplashKit.KeyDown(KeyCode.RightCtrlKey) && player.Level == levelOne))
                {
                    player.Superpower.Use(player); //use the superpower (super jump for level 1)
                }
            }

            //Display specific level instructions for levels 1, 2 and 3 (not start window or exit window)
            //infromation is displayed when the i key is pressed
            if ((player.Level == levelOne || player.Level == levelTwo || player.Level == levelThree) && SplashKit.KeyTyped(KeyCode.IKey))
            {
                Console.WriteLine(player.Instructions);
            }

            //if the player is in levels two or three, pressing ctrl will activate the super power
            //this is different from level one, because in level one the super power is associated with the jump
            if (player.Level == levelTwo || player.Level == levelThree)
            {
                if (SplashKit.KeyDown(KeyCode.LeftCtrlKey) || SplashKit.KeyDown(KeyCode.RightCtrlKey))
                {
                    player.Superpower.Use(player);
                }
            }

            //moving the player to the right
            if (SplashKit.KeyDown(KeyCode.RightKey))
            {
                player.Move("right", windowWidth);
            }

            //moving the player to the left
            if (SplashKit.KeyDown(KeyCode.LeftKey))
            {
                player.Move("left", windowWidth);
            }

            //shrink the player using S
            if (SplashKit.KeyTyped(KeyCode.SKey))
            {
                player.Resize.Shrink(player);
            }

            //Enlarge the player using E
            if (SplashKit.KeyTyped(KeyCode.EKey))
            {
                player.Resize.Enlarge(player);
            }

            //run the level methods
            player.Level.Run(player);
            //run the player methods
            player.Run(ground);
        }
    }
}

//GameObject
namespace MarioGame
{
    public abstract class GameObject
    {
        //private fields to hold the object's coordinates in the game + their width and height
        private double _x, _y;
        private string _name;

        /// <summary>
        /// Default GameObject constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public GameObject(string name, double x, double y)
        {
            _x = x;
            _y = y;
            _name = name;
        }

        /// <summary>
        /// The x coordinate of the game object
        /// </summary>
        public double X
        {
            get
            {
                return _x;
            }
            set
            {
                _x = value;
            }
        }

        /// <summary>
        /// The y coordinate of the game object
        /// </summary>
        public double Y
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
            }
        }

        /// <summary>
        /// The length property of the game object
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }
    }
}

//IDrawable
using SplashKitSDK;

namespace MarioGame
{
    public interface IDrawable
    {
        /// <summary>
        /// Bitmap property
        /// </summary>
        public Bitmap Bitmap
        {
            get;
        }

        /// <summary>
        /// abstract draw method
        /// </summary>
        public abstract void Draw();
    }
}

//Key
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

//Level
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


//Program
using System;
using SplashKitSDK;

namespace MarioGame
{
    public class Program
    {
        public static void Main()
        {
            int windowHeight = 600;
            int windowWidth = 800;

            //Creating the start window
            new Window("Mario Game", windowWidth, windowHeight);

            Game game = Game.getInstance();
            game.CreateGame(); //create the game

            //leaves the window open unless requested to be closed
            do
            {
                SplashKit.ProcessEvents(); //allows Splashkit to react to user interactions
                SplashKit.ClearScreen();

                game.Run(); //run the game
                
                //refresh
                SplashKit.RefreshScreen();

            } while (!SplashKit.WindowCloseRequested("Mario Game"));
        }
    }
}

///PlayerChanges Family Classes
//Direction
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

//Player
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

//Resize
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

///Powerups Family Classes
//Superpower
namespace MarioGame
{
    public abstract class Superpower
    {
        /// <summary>
        /// Name property
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Description proprety
        /// </summary>
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        /// abstract use method
        /// </summary>
        /// <param name="p"></param>
        public abstract void Use(Player p);
    }
}

//SuperMagnet
namespace MarioGame
{
    public class SuperMagnet : Superpower
    {
        /// <summary>
        /// default constructor, sets the name and description of the superpower
        /// </summary>
        /// <param name="name"></param>
        public SuperMagnet(string name)
        {
            Name = name;
            Description = "YOU HAVE THE POWER TO HANG FROM THE MAGNETIC RAIL USING YOUR MAGNETIC HELMET\n" +
                "TO USE YOUR POWER, PRESS AND HOLD CTRL (LEFT OR RIGHT) AND YOU CAN MOVE FORWARD/BACKWARDS USING THE ARROWS";
        }

        /// <summary>
        /// overridden Use method
        /// </summary>
        /// <param name="p"></param>
        public override void Use(Player p)
        {
            if (!p.Attract)
            {
                p.Attract = true; //sets p.Attract to true when this method is called
            }
        }
    }
}

//SuperVoice
namespace MarioGame
{
    public class SuperVoice : Superpower
    {
        /// <summary>
        /// default constructor, sets the name and description of the superpower
        /// </summary>
        /// <param name="name"></param>
        public SuperVoice(string name)
        {
            Name = name;
            Description = "YOU HAVE THE POWER TO BREAK GLASS USING YOUR BEAUTIFUL OPERA VOICE\n" +
                "TO USE YOUR POWER, PRESS AND HOLD CTRL (LEFT OR RIGHT) AS YOU APPROACH THE GLASS DOOR TO BREAK IT";
        }

        /// <summary>
        /// overridden Use method
        /// </summary>
        /// <param name="p"></param>
        public override void Use(Player p)
        {
            if (!p.Sing)
            {
                p.Sing = true; //sets p.Sing to true when this method is called
            }
        }
    }
}


//SuperJump
namespace MarioGame
{
    public class SuperJump : Superpower
    {
        /// <summary>
        /// SuperJump default constructor, sets the name and description of the superpower
        /// </summary>
        /// <param name="name"></param>
        public SuperJump(string name)
        {
            Name = name;
            Description = "YOU HAVE THE POWER TO SUPER JUMP\nTO USE THE SUPER JUMP, PRESS SPACE + CTRL (LEFT OR RIGHT)" +
                "\nTHIS WILL ALLOW YOU TO REACH THE HIGHER BLOCKS";
        }

        /// <summary>
        /// overridden Use method
        /// </summary>
        /// <param name="p"></param>
        public override void Use(Player p)
        {
            p.DY = -1.2; //changes the vertical speed of the player to be -1.2 (higher jump)
        }
    }
}
