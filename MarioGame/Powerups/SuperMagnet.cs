namespace MarioGame
{
    public class SuperMagnet : Superpower
    {
        /// <summary>
        /// default constructor, sets the name and description of the superpower
        /// </summary>
        /// <param name="name"></param>
        public SuperMagnet(string name)
        {
            Name = name;
            Description = "YOU HAVE THE POWER TO HANG FROM THE MAGNETIC RAIL USING YOUR MAGNETIC HELMET\n" +
                "TO USE YOUR POWER, PRESS AND HOLD CTRL (LEFT OR RIGHT) AND YOU CAN MOVE FORWARD/BACKWARDS USING THE ARROWS";
        }

        /// <summary>
        /// overridden Use method
        /// </summary>
        /// <param name="p"></param>
        public override void Use(Player p)
        {
            if (!p.Attract)
            {
                p.Attract = true; //sets p.Attract to true when this method is called
            }
        }
    }
}
