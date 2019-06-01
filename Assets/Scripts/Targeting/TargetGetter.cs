﻿using System.Collections;
using System.Collections.Generic;

namespace Hel.Targeting
{
    public abstract class TargetGetter
    {
        public List<ITargetable> CurrentTargets { get; protected set; }

        public abstract IEnumerator GetTargets();
    }
}

