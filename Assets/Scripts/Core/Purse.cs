using System;
using UnityEngine;

namespace ArtGallery.Core
{
    public class Purse : MonoBehaviour
    {
        [SerializeField] float startingBalance = 0;
        float balance = 0;
        public event Action onPurseUpdated;

        public float GetBalance()
        {
            return balance;
        }

        public void UpdateBalance(float amount)
        {
            balance += amount;
            onPurseUpdated?.Invoke();
        }

        void Start()
        {
            UpdateBalance(startingBalance);
        }
    }   
}
