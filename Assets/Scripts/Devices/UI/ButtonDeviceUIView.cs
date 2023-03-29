using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Devices.UI
{
    public class ButtonDeviceUIView : DeviceUIView
    {
        [Header("Button")]
        [SerializeField] private Button _button;
        [SerializeField] private TextMeshProUGUI _buttonLabel;
        [SerializeField] private string _activeStateButtonLabel;
        [SerializeField] private string _inactiveStateButtonLabel;

        public override void Initialize(string deviceName, bool isActive)
        {
            base.Initialize(deviceName, isActive);

            _button.onClick.AddListener(OnButtonClick);
        }

        public override void UpdateStateInfo(bool isActive)
        {
            base.UpdateStateInfo(isActive);

            _buttonLabel.text = isActive ? _activeStateButtonLabel : _inactiveStateButtonLabel;
        }

        private void OnButtonClick()
        {
            InvokeOnSetState(!IsActive);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnButtonClick);
        }
    }
}