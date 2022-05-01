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

