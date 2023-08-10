using ArtGallery.Core;
using TMPro;
using UnityEngine;

namespace ArtGallery.UI
{
    public class PurseUI : MonoBehaviour
    {
        [SerializeField] TMP_Text balanceText = null;
        Purse robberPurse = null;

        void Awake()
        {
            robberPurse = GameObject.FindWithTag("Robber").GetComponent<Purse>();
        }

        void Start()
        {
            robberPurse.onPurseUpdated += RefreshUI;
        }

        void RefreshUI()
        {
            balanceText.text = $"Robber balance: ${robberPurse.GetBalance()}";
        }
    }
}