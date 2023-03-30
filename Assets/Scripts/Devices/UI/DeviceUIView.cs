using System;
using TMPro;
using UnityEngine;

namespace Devices.UI
{
    public class DeviceUIView : MonoBehaviour, IDeviceUIView
    {
        [Header("Label")]
        [SerializeField] private TextMeshProUGUI _deviceNameLabel;

        [Header("State Label")]
        [SerializeField] private TextMeshProUGUI _stateLabel;
        [SerializeField] private string _activeStateStatusLabel;
        [SerializeField] private string _inactiveStateStatusLabel;

        public event Action<bool> OnSetState;

        protected bool IsActive;
        protected bool HasCurrent;

        public virtual void Initialize(string deviceName, bool isActive)
        {
            _deviceNameLabel.text = deviceName;

            UpdateStateInfo(isActive, false);
        }

        public virtual void UpdateStateInfo(bool isActive, bool hasCurrent)
        {
            IsActive = isActive;
            HasCurrent = hasCurrent;

            _stateLabel.text = isActive && hasCurrent ? _activeStateStatusLabel : _inactiveStateStatusLabel;
        }

        protected void InvokeOnSetState(bool isActive)
        {
            OnSetState?.Invoke(isActive);
        }
    }
}