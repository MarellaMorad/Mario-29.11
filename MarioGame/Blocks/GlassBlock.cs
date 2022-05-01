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
