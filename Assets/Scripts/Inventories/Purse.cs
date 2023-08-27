using System;
using UnityEngine;

namespace ArtGallery.Inventories
{
    public class Purse : MonoBehaviour
    {
        [SerializeField] [Range(0, 1200)] float startingBalance = 0;
        [SerializeField] float maxBalance = 1200;
        float balance = 0;
        public event Action onPurseUpdated;

        public float GetMaxBalance()
        {
            return maxBalance;
        }

        public float GetBalance()
        {
            return balance;
        }

        public void UpdateBalance(float amount)
        {
            if(balance >= maxBalance) return;
            balance += amount;
            onPurseUpdated?.Invoke();
        }

        void Start()
        {
            UpdateBalance(startingBalance);
        }

        void OnValidate()
        {
            onPurseUpdated?.Invoke();
        }
    }   
}
