using System.Collections.Generic;
using UnityEngine;

namespace ArtGallery.Core
{
    public class Bag : MonoBehaviour
    {
        [SerializeField] GameObject deposit = null;
        List<GalleryItem> items = new List<GalleryItem>();

        public Vector3 GetDepositLocation()
        {
            return deposit.transform.position;
        }

        public void AddItem(GalleryItem item)
        {
            item.transform.parent = transform;
            items.Add(item);
        }

        public void SellItems()
        {
            foreach(var item in items)
            {
                GetComponent<Purse>().UpdateBalance(item.GetPrice());
                item.gameObject.SetActive(false);
            }

            items.Clear();
        }

        public bool HasItems()
        {
            if(items == null) return false;
            return items.Count > 0;
        }
    }
}