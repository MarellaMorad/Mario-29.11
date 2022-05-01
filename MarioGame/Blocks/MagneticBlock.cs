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
