using System;
using System.Collections.Generic;
using System.Linq;

namespace Engine
{
    public class RuleEngine : IRuleEngine
    {
        private GamePoint currentPoint;
        private readonly Config.Config cfg;
        private int seq;

        public RuleEngine(Config.Config config)
        {
            cfg = config;

            currentPoint = new GamePoint(cfg.EntryPoint.X, cfg.EntryPoint.Y, cfg.EntryDirection);
            if (!IsValidPoint(currentPoint))
            {
                throw new Exception("invalid point");
            }
        }

        public void Run(List<string> moves)
        {
            if (CheckOnExit())
                return;

            foreach (var m in moves)
            {
                var newPoint = CreateNewPoint(m);

                if (!IsValidPoint(newPoint))
                {
                    throw new Exception("invalid move");
                }

                currentPoint = newPoint;
                seq++;

                if (CheckOnExit())
                    return;

                ShowMoveResult();
            }

            Console.WriteLine("No moves left. Exit not reached.");
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
                _ => throw new Exception($"Seg {seq}: unknown move")
            };
        }

        private GamePoint GetNewPoint() => currentPoint.Direction switch
        {
            "N" => new GamePoint(currentPoint.X, currentPoint.Y - 1, currentPoint.Direction),
            "E" => new GamePoint(currentPoint.X + 1, currentPoint.Y, currentPoint.Direction),
            "S" => new GamePoint(currentPoint.X, currentPoint.Y + 1, currentPoint.Direction),
            "W" => new GamePoint(currentPoint.X - 1, currentPoint.Y, currentPoint.Direction),
            _ => throw new Exception($"Seg {seq}: Unknown direction")
        };

        private string GetNewDirection(string direction) => direction switch
        {
            "N" => "E",
            "E" => "S",
            "S" => "W",
            "W" => "N",
            _ => throw new Exception("Unknown direction")
        };

        private void ShowMoveResult()
        {
            var res = cfg.Mines.Any(m => m.X == currentPoint.X && m.Y == currentPoint.Y);
            Console.WriteLine(res ? $"Seg {seq}: Mine hit" : $"Seg {seq}: Success");
        }


        private bool CheckOnExit()
        {
            var res = IsOnExit();
            if (res)
            {
                Console.WriteLine($"Seg {seq}: Exit Reached");
            }

            return res;
        }

        bool IsOnExit()
        {
            return currentPoint.X == cfg.ExitPoint.X && currentPoint.Y == cfg.ExitPoint.Y;
        }
    }
}
