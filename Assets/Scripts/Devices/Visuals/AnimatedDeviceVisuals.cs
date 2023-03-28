using Devices.Visuals.Abstraction;
using UnityEngine;

namespace Devices.Visuals
{
    public class AnimatedDeviceVisuals : MonoBehaviour, IDeviceVisuals
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private string _activeState;
        [SerializeField] private string _inactiveState;

        public void SetVisualState(bool isActive)
        {
            _animator.Play(isActive ? _activeState : _inactiveState);
        }
    }
}