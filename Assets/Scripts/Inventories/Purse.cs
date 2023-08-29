using System;
using UnityEngine;

namespace ArtGallery.Inventories
{
    public class Purse : MonoBehaviour
    {
        [SerializeField] [Range(0, 1200)] float startingBalance = 0;
        [SerializeField] float maxBalance = 1200;
        [SerializeField] bool reduceMoneyWithTime = false;
        [SerializeField] float moneyReduceRate = 0.1f;
        float balance = 0;

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
        }

        void Start()
        {
            UpdateBalance(startingBalance);
        }

        void Update()
        {
            if(!reduceMoneyWithTime) return;

            balance = Mathf.Max(0, balance - Time.deltaTime * moneyReduceRate);
        }
    }   
}
