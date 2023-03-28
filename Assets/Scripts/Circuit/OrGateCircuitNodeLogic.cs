using System.Linq;

namespace Circuit
{
    public class OrGateCircuitNodeLogic : AlwaysActiveCircuitNodeLogic
    {
        public override void RefreshCurrentState()
        {
            foreach (var node in CircuitNode.InputNodes)
            {
                if (node.CircuitNodeLogic == null) continue;
                if (node.CircuitNodeLogic.HasCurrent && node.CircuitNodeLogic.IsActive)
                {
                    HasCurrent = true;
                    break;
                }
            }

            HasCurrent = false;
        }
    }
}