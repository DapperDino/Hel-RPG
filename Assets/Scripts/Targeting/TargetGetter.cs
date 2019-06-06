using System.Collections;
using System.Collections.Generic;

namespace Hel.Targeting
{
    /// <summary>
    /// The base class for all target getters.
    /// </summary>
    public abstract class TargetGetter
    {
        public List<ITargetable> CurrentTargets { get; protected set; }

        public abstract IEnumerator GetTargets();
    }
}

