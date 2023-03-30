using System;
using System.Collections;
using Devices.Data;
using Devices.UI;
using Framework;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Startup
{
    public class Startup : MonoBehaviour
    {
        [SerializeField] private string[] _gameScenes;

        [SerializeField] private DevicesConfig _devicesConfig;

        private CircuitConsumptionTracker _circuitConsumptionTracker;

        private IEnumerator Start()
        {
            _circuitConsumptionTracker = new CircuitConsumptionTracker();
            ServiceLocator.Instance.Register(_circuitConsumptionTracker);

            foreach (var gameScene in _gameScenes)
            {
                yield return SceneManager.LoadSceneAsync(gameScene, LoadSceneMode.Additive);
            }

            var circuitManager = ServiceLocator.Instance.GetService<CircuitManager>();
            var circuitUIManager = ServiceLocator.Instance.GetService<CircuitUiManager>();

            circuitUIManager.Initialize(circuitManager.Nodes, _devicesConfig);
            circuitManager.Initialize(_devicesConfig);

            _circuitConsumptionTracker.Initialize(circuitManager);
        }

        private void Update()
        {
            _circuitConsumptionTracker?.Update();
        }

        private void OnDestroy()
        {
            ServiceLocator.Instance.Unregister(_circuitConsumptionTracker);
        }
    }
}