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
            collection.AddScoped<IRuleEngine, RuleEngine>();
            collection.AddScoped(c => c.GetService<IConfig>().GetConfig(args[0]));

            var sp = collection.BuildServiceProvider();

            using var scope = sp.CreateScope();
          
            var config = sp.GetService<IConfig>();
            var game = sp.GetService<IRuleEngine>();

            game.Run(config.GetMoves(args[1]));
        }
    }
}
