using System.Collections;
using System.Collections.Generic;

namespace Trisolaris.Stats
{
    interface IModifierProvider
    {
        IEnumerable<float> GetAdditiveModifiers(Stat stat);
        IEnumerable<float> GetPercentageModifiers(Stat stat);
    }
}

