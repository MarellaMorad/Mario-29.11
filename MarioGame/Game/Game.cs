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
