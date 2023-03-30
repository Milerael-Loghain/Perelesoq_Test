using System;
using Framework;
using TMPro;
using UnityEngine;

namespace Devices.UI
{
    public class TimeTrackerPhoneUi : MonoBehaviour
    {
        private const string TIME_FORMAT = "{0}d {1}h {2}m {3}s";

        [SerializeField] private TextMeshProUGUI _timeTextField;

        private void Update()
        {
            var day = DateTime.Now.ToString("dd");
            var hour = DateTime.Now.ToString("hh");
            var minute = DateTime.Now.ToString("mm");
            var second = DateTime.Now.ToString("ss");

            _timeTextField.text = string.Format(TIME_FORMAT, day, hour, minute, second);
        }
    }
}