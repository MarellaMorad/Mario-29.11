namespace MarioGame
{
    public class SuperVoice : Superpower
    {
        /// <summary>
        /// default constructor, sets the name and description of the superpower
        /// </summary>
        /// <param name="name"></param>
        public SuperVoice(string name)
        {
            Name = name;
            Description = "YOU HAVE THE POWER TO BREAK GLASS USING YOUR BEAUTIFUL OPERA VOICE\n" +
                "TO USE YOUR POWER, PRESS AND HOLD CTRL (LEFT OR RIGHT) AS YOU APPROACH THE GLASS DOOR TO BREAK IT";
        }

        /// <summary>
        /// overridden Use method
        /// </summary>
        /// <param name="p"></param>
        public override void Use(Player p)
        {
            if (!p.Sing)
            {
                p.Sing = true; //sets p.Sing to true when this method is called
            }
        }
    }
}
