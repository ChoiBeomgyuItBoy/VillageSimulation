using System;
using UnityEngine;

namespace ArtGallery.Core
{
    public class Patron : MonoBehaviour
    {
        [SerializeField] bool hasTicket = false;
        public event Action onTicketChanged;

        public bool HasTicket()
        {
            return hasTicket;
        }

        public void SetTicket(bool hasTicket)
        {
            this.hasTicket = hasTicket;
            onTicketChanged?.Invoke();
        }
    }
}