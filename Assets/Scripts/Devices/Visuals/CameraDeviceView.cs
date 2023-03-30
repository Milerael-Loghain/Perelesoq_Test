using Devices.Visuals.Abstraction;
using UnityEngine;

namespace Devices.Visuals
{
    public class CameraDeviceView : MonoBehaviour, IDeviceView
    {
        [SerializeField] private Transform _cameraHolder;

        public void SetVisualState(bool isActive, bool hasCurrent)
        {
            if (isActive)
            {
                Camera.main.transform.SetParent(_cameraHolder, false);
            }
        }
    }
}