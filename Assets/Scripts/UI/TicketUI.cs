using ArtGallery.Core;
using UnityEngine;
using UnityEngine.UI;

namespace ArtGallery.UI
{
    public class TicketUI : MonoBehaviour
    {
        [SerializeField] Image ticketIcon;
        [SerializeField] Patron patron;

        void Start()
        {
            patron.onTicketChanged += RefreshTicket;
            RefreshTicket();
        }

        void RefreshTicket()
        {
            ticketIcon.enabled = patron.HasTicket();
        }
    }
}