using Cinemachine;
using UnityEngine;

namespace Hel.Utilities
{
    /// <summary>
    /// Used to overwrite Cinemachine input.
    /// </summary>
    public class CinemachineInputHandler : MonoBehaviour
    {
        public void UnblockInput() => CinemachineCore.GetInputAxis = new CinemachineCore.AxisInputDelegate(NormalInput);
        public void BlockInput() => CinemachineCore.GetInputAxis = new CinemachineCore.AxisInputDelegate(BlockedInput);

        private float NormalInput(string axisName) { return Input.GetAxis(axisName); }
        private float BlockedInput(string axisName) { return 0f; }
    }
}
