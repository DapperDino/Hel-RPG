using UnityEngine;

namespace Hel.Targeting
{
    /// <summary>
    /// Defines any entity that can be targeted.
    /// </summary>
    public interface ITargetable
    {
        string name { get; }
        Transform transform { get; }
        Transform TargetTransform { get; }
    }
}

