using System;
using ArtGallery.Inventories;
using TMPro;
using UnityEngine;

namespace ArtGallery.UI
{
    public class PurseUI : MonoBehaviour
    {
        [SerializeField] Purse purse = null;
        TMP_Text balanceText = null;

        void Awake()
        {
            balanceText = GetComponent<TMP_Text>();
        }

        void Update()
        {
            RefreshUI();
        }

        void RefreshUI()
        {
            balanceText.text = $"${purse.GetBalance():N0}/${purse.GetMaxBalance():N0}";
        }
    }
}