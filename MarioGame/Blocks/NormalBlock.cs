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
