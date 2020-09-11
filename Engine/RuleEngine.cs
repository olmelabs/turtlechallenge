using System;
using System.Collections.Generic;
using System.Linq;

namespace Engine
{
    public class RuleEngine : IRuleEngine
    {
        private GamePoint currentPoint;
        private readonly Config.Config cfg;

        public RuleEngine(Config.Config config)
        {
            cfg = config;

            currentPoint = new GamePoint(cfg.EntryPoint.X, cfg.EntryPoint.Y, cfg.EntryDirection);
            if (!IsValidPoint(currentPoint))
            {
                throw new Exception("invalid point");
            }
        }

        public string  Run(List<string> moves)
        {
            if (IsOnExit())
                return "Success";

            foreach (var m in moves)
            {
                var newPoint = CreateNewPoint(m);

                if (!IsValidPoint(newPoint))
                {
                    throw new Exception("invalid move");
                }

                currentPoint = newPoint;
                //TracePosition();

                if (IsOnExit())
                    return "Success";

                if (!CheckMoveResult())
                    return "Mine hit";
            }

            return "No moves left. Exit not reached.";
        }

        private bool IsValidPoint(GamePoint point)
        {
            return point.X >= 0 && point.X < cfg.Width && point.Y >= 0 && point.Y < cfg.Height;
        }

        private GamePoint CreateNewPoint(string move)
        {
            return move switch
            {
                "r" => new GamePoint(currentPoint.X, currentPoint.Y, GetNewDirection(currentPoint.Direction)),
                "m" => new GamePoint(GetNewPoint()),
                _ => throw new Exception("Unknown move")
            };
        }

        private GamePoint GetNewPoint() => currentPoint.Direction switch
        {
            "N" => new GamePoint(currentPoint.X, currentPoint.Y - 1, currentPoint.Direction),
            "E" => new GamePoint(currentPoint.X + 1, currentPoint.Y, currentPoint.Direction),
            "S" => new GamePoint(currentPoint.X, currentPoint.Y + 1, currentPoint.Direction),
            "W" => new GamePoint(currentPoint.X - 1, currentPoint.Y, currentPoint.Direction),
            _ => throw new Exception("Unknown direction")
        };

        private string GetNewDirection(string direction) => direction switch
        {
            "N" => "E",
            "E" => "S",
            "S" => "W",
            "W" => "N",
            _ => throw new Exception("Unknown direction")
        };

        private bool CheckMoveResult()
        {
            return !cfg.Mines.Any(m => m.X == currentPoint.X && m.Y == currentPoint.Y);
        }

        bool IsOnExit()
        {
            return currentPoint.X == cfg.ExitPoint.X && currentPoint.Y == cfg.ExitPoint.Y;
        }

        private void TracePosition()
        {
            Console.WriteLine($"X:{currentPoint.X}, Y:{currentPoint.Y}, D: {currentPoint.Direction}");
        }
    }
}
