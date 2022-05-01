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
