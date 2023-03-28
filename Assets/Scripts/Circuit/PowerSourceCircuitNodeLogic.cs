using Data;

namespace Circuit
{
    public class PowerSourceCircuitNodeLogic : AlwaysActiveCircuitNodeLogic
    {
        public override void Initialize(CircuitNode circuitNode, DeviceConfigBase deviceConfigBase)
        {
            base.Initialize(circuitNode, deviceConfigBase);
            HasCurrent = true;
        }

        public override void RefreshCurrentState()
        {
            HasCurrent = true;
        }
    }
}