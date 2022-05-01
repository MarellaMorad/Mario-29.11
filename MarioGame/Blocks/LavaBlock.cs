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
