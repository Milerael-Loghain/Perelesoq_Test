using Data;

namespace Circuit
{
    public class AlwaysActiveCircuitNodeLogic : CircuitNodeLogicBase
    {
        public override void Initialize(CircuitNode circuitNode, DeviceConfigBase deviceConfigBase)
        {
            base.Initialize(circuitNode, deviceConfigBase);

            IsActive = true;
        }

        public override float SetActiveState(bool isActive)
        {
            return base.SetActiveState(true);
        }
    }
}