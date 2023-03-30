using System;
using Data;

namespace Circuit
{
    public class CircuitNodeLogicBase : ICircuitNodeLogic
    {
        public event Action<bool, bool, float> OnStateChanged;

        public bool IsActive
        {
            get => _isActive;
            protected set
            {
                if (_isActive == value) return;

                _isActive = value;

                OnStateChanged.Invoke(value, _hasCurrent, _energyConsumedByActivation);
                RefreshOutputNodesCurrentState();
            }
        }

        public bool HasCurrent
        {
            get => _hasCurrent;
            protected set
            {
                _hasCurrent = value;

                OnStateChanged.Invoke(_isActive, value, _energyConsumedByActivation);
                RefreshOutputNodesCurrentState();
            }
        }

        public float ActiveEnergyConsumedByHour
        {
            get
            {
                if (!_isActive || !_hasCurrent) return 0;
                return _activeEnergyConsumedBySecond;
            }
            private set => _activeEnergyConsumedBySecond = value;
        }

        protected CircuitNode CircuitNode;

        private bool _isActive;
        private bool _hasCurrent;
        private float _energyConsumedByActivation;
        private bool _canBeActivatedWithoutCurrent;
        private float _activeEnergyConsumedBySecond;

        public virtual void Initialize(CircuitNode circuitNode, DeviceConfigBase deviceConfigBase)
        {
            CircuitNode = circuitNode;
            IsActive = circuitNode.DefaultActiveState;

            ActiveEnergyConsumedByHour = deviceConfigBase.ActiveEnergyConsumedByHour;
            _energyConsumedByActivation = deviceConfigBase.EnergyConsumedByActivation;
            _canBeActivatedWithoutCurrent = deviceConfigBase.CanBeActivatedWithoutCurrent;
        }

        public virtual void SetActiveState(bool isActive)
        {
            if (!_canBeActivatedWithoutCurrent && !_hasCurrent) return;

            IsActive = isActive;
        }

        public virtual void RefreshCurrentState()
        {
            bool hasCurrent = true;
            foreach (var node in CircuitNode.InputNodes)
            {
                if (node.CircuitNodeLogic == null) continue;
                if (node.CircuitNodeLogic.HasCurrent && node.CircuitNodeLogic.IsActive) continue;

                hasCurrent = false;
                break;
            }

            HasCurrent = hasCurrent;
        }

        private void RefreshOutputNodesCurrentState()
        {
            foreach (var outputNode in CircuitNode.OutputNodes)
            {
                outputNode.CircuitNodeLogic?.RefreshCurrentState();
            }
        }
    }
}