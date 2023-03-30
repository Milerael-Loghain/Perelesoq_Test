using System;
using Framework;
using TMPro;
using UnityEngine;

namespace Devices.UI
{
    public class ConsumptionTrackerPhoneUi : MonoBehaviour
    {
        private const string TOTAL_CONSUMPTION_FORMAT = "TOTAL: {0}W";
        private const string CURRENT_CONSUMPTION_FORMAT = "CURRENT: {0}W";

        [SerializeField] private TextMeshProUGUI _totalConsumptionTextField;
        [SerializeField] private TextMeshProUGUI _currentConsumptionTextField;

        private CircuitConsumptionTracker _circuitConsumptionTracker;

        private void Start()
        {
            _circuitConsumptionTracker = ServiceLocator.Instance.GetService<CircuitConsumptionTracker>();
        }

        private void Update()
        {
            _totalConsumptionTextField.text =
                string.Format(TOTAL_CONSUMPTION_FORMAT, _circuitConsumptionTracker.TotalConsumption);

            _currentConsumptionTextField.text =
                string.Format(CURRENT_CONSUMPTION_FORMAT, _circuitConsumptionTracker.CurrentConsumption);
        }
    }
}