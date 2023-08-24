using ArtGallery.Core;
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

        void Start()
        {
            purse.onPurseUpdated += RefreshUI;
            RefreshUI();
        }

        void RefreshUI()
        {
            balanceText.text = $"${purse.GetBalance()}/{purse.GetMaxBalance()}";
        }
    }
}