namespace Engine
{
    public class GamePoint
    {
        public GamePoint(GamePoint pt)
        {
            X = pt.X;
            Y = pt.Y;
            Direction = pt.Direction;
        }

        public GamePoint(int x, int y, string direction)
        {
            X = x;
            Y = y;
            Direction = direction;
        }
        public int X { get; }
        public int Y { get;  }
        public string Direction { get; }
    }
}
