
namespace Assets.Logic.CarLocation
{
    public class CartesianPoint 
    {
        public CartesianPoint() : this(0, 0)
        {
        }

        public CartesianPoint(float x, float y)
        {
            X = x;
            Y = y;
        }

        public float X { get; set; } = 0;
        public float Y { get; set; } = 0;
	}
}