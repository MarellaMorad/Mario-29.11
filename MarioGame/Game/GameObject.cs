namespace MarioGame
{
    public abstract class GameObject
    {
        //private fields to hold the object's coordinates in the game + their width and height
        private double _x, _y;
        private string _name;

        /// <summary>
        /// Default GameObject constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public GameObject(string name, double x, double y)
        {
            _x = x;
            _y = y;
            _name = name;
        }

        /// <summary>
        /// The x coordinate of the game object
        /// </summary>
        public double X
        {
            get
            {
                return _x;
            }
            set
            {
                _x = value;
            }
        }

        /// <summary>
        /// The y coordinate of the game object
        /// </summary>
        public double Y
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
            }
        }

        /// <summary>
        /// The length property of the game object
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }
    }
}
