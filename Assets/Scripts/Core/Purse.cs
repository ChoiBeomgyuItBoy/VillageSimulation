using System;
using UnityEngine;

namespace ArtGallery.Core
{
    public class Purse : MonoBehaviour
    {
        [SerializeField] float startingBalance = 0;
        [SerializeField] float maxbalance = 1200;
        float balance = 0;
        public event Action onPurseUpdated;

        public float GetMaxBalance()
        {
            return maxbalance;
        }

        public float GetBalance()
        {
            return balance;
        }

        public void UpdateBalance(float amount)
        {
            if(balance >= maxbalance) return;
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
