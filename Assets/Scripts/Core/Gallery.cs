using System;
using System.Collections.Generic;
using UnityEngine;

namespace ArtGallery.Core
{
    public class Gallery : MonoBehaviour
    {
        [SerializeField] [Range(0,24)] float openingTime = 9;
        [SerializeField] [Range(0,24)] float closingTime = 19;
        Stack<Patron> patrons = new Stack<Patron>();
        List<Patron> patronsRecord = new List<Patron>();
        Clock clock = null;

        public Patron GetCurrentPatron()
        {
            if(patrons.Count == 0)
            {
                return null;
            }

            return patrons.Pop();
        }

        public bool RegisterPatron(Patron patron)
        {
            patronsRecord.Add(patron);
            patrons.Push(patron);
            return true;
        }

        public bool IsOpen()
        {
            return clock.GetCurrentTime() >= openingTime && clock.GetCurrentTime() <= closingTime;
        }

        private void Awake()
        {
            clock = FindObjectOfType<Clock>();
        }
    
        private void Update()
        {
            float roundedTime = (float) Math.Round(clock.GetCurrentTime(), 1);

            if(Mathf.Approximately(closingTime, roundedTime))
            {
                DeregisterAllPatrons();
            }
        }

        private void DeregisterAllPatrons()
        {
            foreach(var patron in patronsRecord)
            {
                patron.SetTicket(false);
            }

            patrons.Clear();
        }
    }
}