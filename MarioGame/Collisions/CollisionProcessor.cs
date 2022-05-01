using System.Collections.Generic;

namespace MarioGame
{
    public class CollisionProcessor : ICollision
    {
        private List<ICollision> _collisions = new List<ICollision>(); //to register the different collisions

        private VerticalCollision _verticalCollision = new VerticalCollision();
        private HorizontalCollision _horizontalCollision = new HorizontalCollision();

        /// <summary>
        /// Default CollisionProcessor constructor to register the different collisions
        /// </summary>
        public CollisionProcessor()
        {
            _collisions.Add(_verticalCollision);
            _collisions.Add(_horizontalCollision);
        }

        /// <summary>
        /// Runs the collisions check taking in the player and the block as parameters
        /// Implements the Check method from the ICollision interface
        /// </summary>
        /// <param name="p"></param>
        /// <param name="block"></param>
        public void Check(Player p, Block block)
        {
            foreach (ICollision c in _collisions)
            {
                c.Check(p, block);
            }
        }
    }
}
