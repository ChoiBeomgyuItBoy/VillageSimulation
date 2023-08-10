using System.Collections.Generic;
using UnityEngine;

namespace ArtGallery.Core
{
    public class Bag : MonoBehaviour
    {
        List<GalleryItem> items = new List<GalleryItem>();

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
        }
    }
}