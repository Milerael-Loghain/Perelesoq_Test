using System.Collections.Generic;
using Devices.Data;
using Devices.UI;
using Framework;
using UnityEngine;

namespace Startup
{
    public class CircuitUiManager : MonoBehaviour
    {
        [SerializeField] private RectTransform _devicesUIViewContainer;

        private void Awake()
        {
            ServiceLocator.Instance.Register(this);
        }

        public void Initialize(List<CircuitNode> circuitNodes, DevicesConfig devicesConfig)
        {
            foreach (var node in circuitNodes)
            {
                var deviceConfig = devicesConfig.GetDeviceConfigByType(node.DeviceType);
                if (deviceConfig.DeviceUiView == null) continue;

                var deviceUiViewGO = Instantiate(deviceConfig.DeviceUiView, _devicesUIViewContainer);
                var deviceUiView = deviceUiViewGO.GetComponent<IDeviceUIView>();

                node.BindUIView(deviceUiView);
            }
        }

        private void OnDestroy()
        {
            ServiceLocator.Instance.Unregister(this);
        }
    }
}