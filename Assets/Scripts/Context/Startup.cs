using System.Collections;
using Devices.Data;
using Framework;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Startup
{
    public class Startup : MonoBehaviour
    {
        [SerializeField] private string[] _gameScenes;

        [SerializeField] private DevicesConfig _devicesConfig;

        private IEnumerator Start()
        {
            foreach (var gameScene in _gameScenes)
            {
                yield return SceneManager.LoadSceneAsync(gameScene, LoadSceneMode.Additive);
            }

            var circuitManager = ServiceLocator.Instance.GetService<CircuitManager>();
            var circuitUIManager = ServiceLocator.Instance.GetService<CircuitUiManager>();

            circuitUIManager.Initialize(circuitManager.Nodes, _devicesConfig);
            circuitManager.Initialize(_devicesConfig);
        }
    }
}