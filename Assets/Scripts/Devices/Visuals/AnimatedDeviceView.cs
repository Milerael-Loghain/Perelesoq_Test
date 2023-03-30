using Devices.Visuals.Abstraction;
using UnityEngine;

namespace Devices.Visuals
{
    public class AnimatedDeviceView : MonoBehaviour, IDeviceView
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private string _activeState;
        [SerializeField] private string _inactiveState;

        private bool _isActive;

        public void SetVisualState(bool isActive, bool hasCurrent)
        {
            if ((isActive && hasCurrent) == _isActive) return;

            _isActive = isActive && hasCurrent;
            _animator.Play(_isActive ? _activeState : _inactiveState, 0, 0);
        }
    }
}