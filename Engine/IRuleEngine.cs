using System.Collections.Generic;

namespace Engine
{
    public interface IRuleEngine
    {
        string Run(List<string> moves);
    }
}
