namespace FixedMath
{
    public struct FixedVector2
    {
        public Fixed X;
        public Fixed Y;

        public FixedVector2(Fixed value)
        {
            X = value;
            Y = value;
        }

        public FixedVector2(Fixed x, Fixed y)
        {
            X = x;
            Y = y;
        }
    }
}
