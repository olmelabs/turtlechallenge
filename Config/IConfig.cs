using System.Collections.Generic;

namespace Config
{
    public interface IConfig
    {
        Config GetConfig(string fileName);

        List<List<string>> GetMoves(string fileName);
    }
}
