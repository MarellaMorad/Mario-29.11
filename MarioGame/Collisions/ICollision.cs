namespace MarioGame
{
    public interface ICollision
    {
        //provides the collision check method to be used in CollisionProcessor, Horizontal and Vertical Collision classes
        public abstract void Check(Player p, Block block);
    }
}
