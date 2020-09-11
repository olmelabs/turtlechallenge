using System;
using Config;
using Engine;
using Microsoft.Extensions.DependencyInjection;

namespace Turtle
{
    class Program
    {
        static void Main(string[] args)
        {
            var collection = new ServiceCollection();
            collection.AddScoped<IConfig, JsonConfigReader>();
            collection.AddTransient<IRuleEngine, RuleEngine>();
            collection.AddScoped(c => c.GetService<IConfig>().GetConfig(args[0]));

            var sp = collection.BuildServiceProvider();

            using var scope = sp.CreateScope();

            var config = sp.GetService<IConfig>();

            var probes = config.GetMoves(args[1]);
            var i = 0;
            foreach (var seq in probes)
            {
                var game = sp.GetService<IRuleEngine>();
                Console.WriteLine($"Seq {i++}: {game.Run(seq)}");
            }
        }
    }
}
