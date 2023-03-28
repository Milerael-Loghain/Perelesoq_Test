using System;
using Data;

namespace Circuit
{
    public class CircuitNodeLogicBase : ICircuitNodeLogic
    {
        public event Action<bool> OnActiveStateChanged;

        public bool IsActive
        {
            get => _isActive;
            protected set
            {
                if (_isActive == value) return;

                _isActive = value;
                OnActiveStateChanged.Invoke(value);
                RefreshOutputNodesCurrentState();
            }
        }

        public bool HasCurrent
        {
            get => _hasCurrent;
            protected set
            {
                if (_hasCurrent == value) return;

                _hasCurrent = value;
                RefreshOutputNodesCurrentState();
            }
        }

        public float ActiveEnergyConsumedBySecond { get; private set; }

        protected CircuitNode CircuitNode;

        private bool _isActive;
        private bool _hasCurrent;
        private float _energyConsumedByActivation;

        public virtual void Initialize(CircuitNode circuitNode, DeviceConfigBase deviceConfigBase)
        {
            CircuitNode = circuitNode;
            ActiveEnergyConsumedBySecond = deviceConfigBase.ActiveEnergyConsumedByHour;
            _energyConsumedByActivation = deviceConfigBase.EnergyConsumedByActivation;
            IsActive = deviceConfigBase.InitialActiveState;
        }

        public virtual float SetActiveState(bool isActive)
        {
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
                outputNode.CircuitNodeLogic.RefreshCurrentState();
            }
        }
    }
}