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
