﻿using System.Collections;

namespace Hel.Utilities
{
    public class CoroutineWithData
    {
        public object Result { get; private set; }

        private readonly IEnumerator target;

        public CoroutineWithData(IEnumerator target) => this.target = target;

        public IEnumerator Run()
        {
            while (target.MoveNext())
            {
                Result = target.Current;
                yield return Result;
            }
        }
    }
}
