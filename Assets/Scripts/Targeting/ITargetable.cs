using UnityEngine;

namespace Hel.Targeting
{
    public interface ITargetable
    {
        string name { get; }
        Transform transform { get; }
        Transform TargetTransform { get; }
    }
}

