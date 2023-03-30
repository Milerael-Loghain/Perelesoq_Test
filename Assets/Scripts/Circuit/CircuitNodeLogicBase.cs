using System;
using Data;

namespace Circuit
{
    public class CircuitNodeLogicBase : ICircuitNodeLogic
    {
        public event Action<bool, bool> OnStateChanged;

        public bool IsActive
        {
            get => _isActive;
            protected set
            {
                if (_isActive == value) return;

                _isActive = value;

                OnStateChanged.Invoke(value, _hasCurrent);
                RefreshOutputNodesCurrentState();
            }
        }

        public bool HasCurrent
        {
            get => _hasCurrent;
            protected set
            {
                _hasCurrent = value;

                OnStateChanged.Invoke(_isActive, value);
                RefreshOutputNodesCurrentState();
            }
        }

        public float ActiveEnergyConsumedBySecond { get; private set; }

        protected CircuitNode CircuitNode;

        private bool _isActive;
        private bool _hasCurrent;
        private float _energyConsumedByActivation;
        private bool _canBeActivatedWithoutCurrent;

        public virtual void Initialize(CircuitNode circuitNode, DeviceConfigBase deviceConfigBase)
        {
            CircuitNode = circuitNode;
            IsActive = circuitNode.DefaultActiveState;

            ActiveEnergyConsumedBySecond = deviceConfigBase.ActiveEnergyConsumedByHour;
            _energyConsumedByActivation = deviceConfigBase.EnergyConsumedByActivation;
            _canBeActivatedWithoutCurrent = deviceConfigBase.CanBeActivatedWithoutCurrent;
        }

        public virtual float SetActiveState(bool isActive)
        {
            if (!_canBeActivatedWithoutCurrent && !_hasCurrent) return 0;

            IsActive = isActive;

            return _energyConsumedByActivation;
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