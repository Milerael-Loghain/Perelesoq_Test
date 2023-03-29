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

        public virtual void Initialize(string deviceName, bool isActive)
        {
            _deviceNameLabel.text = deviceName;

            UpdateStateInfo(isActive);
        }

        public virtual void UpdateStateInfo(bool isActive)
        {
            IsActive = isActive;

            _stateLabel.text = isActive ? _activeStateStatusLabel : _inactiveStateStatusLabel;
        }

        protected void InvokeOnSetState(bool isActive)
        {
            OnSetState?.Invoke(isActive);
        }
    }
}