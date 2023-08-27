using System;
using ArtGallery.Core;
using TMPro;
using UnityEngine;

namespace ArtGallery.UI
{
    public class ClockUI : MonoBehaviour
    {
        [SerializeField] TMP_Text timeText;
        Clock clock;

        void Start()
        {
            clock = FindObjectOfType<Clock>();
        }

        void Update()
        {
            UpdateUI();
        }

        void UpdateUI()
        {
            string hour = DateTime.FromOADate(clock.GetCurrentTime() * 1.0 / 24).
            ToString(@"hh:mm tt", System.Globalization.CultureInfo.InvariantCulture).Replace(".", "");
            hour = hour.Replace("AM", "am").Replace("PM", "pm");
            timeText.text = hour;
        }
    }
}
