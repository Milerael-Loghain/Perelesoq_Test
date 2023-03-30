using UnityEngine;

namespace Devices.UI
{
    public class CircuitConsumptionTracker
    {
        public float TotalConsumption => _totalConsumption;
        public float CurrentConsumption => _currentConsumption;

        private CircuitManager _circuitManager;
        private float _totalConsumption;
        private float _currentConsumption;
        private bool _isInitialized;

        public void Initialize(CircuitManager circuitManager)
        {
            _circuitManager = circuitManager;

            foreach (var circuitNode in _circuitManager.Nodes)
            {
                circuitNode.CircuitNodeLogic.OnStateChanged += OnCircuitNodeStateChanged;
            }

            _isInitialized = true;
        }

        public void Update()
        {
            if (!_isInitialized) return;

            _currentConsumption = 0;
            foreach (var circuitNode in _circuitManager.Nodes)
            {
                _currentConsumption += circuitNode.CircuitNodeLogic.ActiveEnergyConsumedByHour;
            }

            _totalConsumption += (_currentConsumption / 60f / 60f) * Time.deltaTime;
            // Debug.Log($"Current Consumption: {currentConsumption}, Total Consumption: {totalConsumption}");
        }

        private void OnCircuitNodeStateChanged(bool isActive, bool hasCurrent, float consumption)
        {
            _totalConsumption += consumption;
        }
    }
}