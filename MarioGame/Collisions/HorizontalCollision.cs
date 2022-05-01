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
