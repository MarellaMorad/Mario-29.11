namespace MarioGame
{
    public class SuperJump : Superpower
    {
        /// <summary>
        /// SuperJump default constructor, sets the name and description of the superpower
        /// </summary>
        /// <param name="name"></param>
        public SuperJump(string name)
        {
            Name = name;
            Description = "YOU HAVE THE POWER TO SUPER JUMP\nTO USE THE SUPER JUMP, PRESS SPACE + CTRL (LEFT OR RIGHT)" +
                "\nTHIS WILL ALLOW YOU TO REACH THE HIGHER BLOCKS";
        }

        /// <summary>
        /// overridden Use method
        /// </summary>
        /// <param name="p"></param>
        public override void Use(Player p)
        {
            p.DY = -1.2; //changes the vertical speed of the player to be -1.2 (higher jump)
        }
    }
}
