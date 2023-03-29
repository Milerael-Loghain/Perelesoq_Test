using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Devices.UI
{
    public class ToggleDeviceUIView : DeviceUIView
    {
        [Header("Toggle")]
        [SerializeField] private Toggle _toggle;
        [SerializeField] private TextMeshProUGUI _toggleLabel;
        [SerializeField] private string _activeStateToggleLabel;
        [SerializeField] private string _inactiveStateToggleLabel;

        public override void Initialize(string deviceName, bool isActive)
        {
            base.Initialize(deviceName, isActive);

            _toggle.isOn = isActive;
            _toggle.onValueChanged.AddListener(OnSetToggleState);
        }

        public override void UpdateStateInfo(bool isActive)
        {
            base.UpdateStateInfo(isActive);

            _toggle.isOn = isActive;
            _toggleLabel.text = isActive ? _activeStateToggleLabel : _inactiveStateToggleLabel;
        }

        private void OnSetToggleState(bool state)
        {
            InvokeOnSetState(!state);
        }

        private void OnDestroy()
        {
            _toggle.onValueChanged.RemoveListener(OnSetToggleState);
        }
    }
}