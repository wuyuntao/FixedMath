namespace FixedMath
{
    public struct FVector2
    {
        public Fixed X;
        public Fixed Y;

        public FVector2(Fixed value)
        {
            X = value;
            Y = value;
        }

        public FVector2(Fixed x, Fixed y)
        {
            X = x;
            Y = y;
        }
    }
}
