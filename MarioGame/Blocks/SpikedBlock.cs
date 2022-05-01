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
