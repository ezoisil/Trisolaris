using System.Collections;
using System.Collections.Generic;

namespace Trisolaris.Stats
{
    interface IModifierProvider
    {
        IEnumerable<float> GetAdditiveModifier(Stat stat);
    }
}

