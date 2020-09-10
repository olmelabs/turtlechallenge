using System.Collections.Generic;

namespace Config
{
    public class Config
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public Point EntryPoint { get; set; }
        public string EntryDirection { get; set; }
        public Point ExitPoint { get; set; }
        public List<Point> Mines { get; set; }
    }
}
