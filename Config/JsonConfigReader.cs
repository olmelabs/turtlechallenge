using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Config
{
    public class JsonConfigReader : IConfig
    {
        public Config GetConfig(string fileName)
        {
            return JsonSerializer.Deserialize<Config>(File.ReadAllText(fileName));
        }

        public List<string> GetMoves(string fileName)
        {
            return JsonSerializer.Deserialize<List<string>>(File.ReadAllText(fileName));
        }
    }
}
