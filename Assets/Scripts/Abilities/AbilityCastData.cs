using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Hel.Abilities
{
    /// <summary>
    /// Used to be able to pass data to ability actions that they might need access to.
    /// </summary>
    [Serializable]
    public struct AbilityCastData
    {
        [Required] [SerializeField] private AbilityActionHandler abilityActionHandler;
        [Required] [SerializeField] private Animator animator;
        [Required] [SerializeField] private Transform rootTransform;
        [Required] [SerializeField] private Transform leftHandTransform;
        [Required] [SerializeField] private Transform rightHandTransform;

        public AbilityActionHandler AbilityActionHandler { get { return abilityActionHandler; } }
        public Animator Animator { get { return animator; } }
        public Transform RootTransform { get { return rootTransform; } }
        public Transform LeftHandTransform { get { return leftHandTransform; } }
        public Transform RightHandTransform { get { return rightHandTransform; } }
    }
}
