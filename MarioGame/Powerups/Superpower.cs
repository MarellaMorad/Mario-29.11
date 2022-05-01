namespace MarioGame
{
    public abstract class Superpower
    {
        /// <summary>
        /// Name property
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Description proprety
        /// </summary>
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        /// abstract use method
        /// </summary>
        /// <param name="p"></param>
        public abstract void Use(Player p);
    }
}
