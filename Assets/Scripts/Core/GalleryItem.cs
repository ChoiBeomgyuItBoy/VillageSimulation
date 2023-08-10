using System.Collections.Generic;
using UnityEngine;

namespace ArtGallery.Core
{
    public class GalleryItem : MonoBehaviour
    {
        [SerializeField] float price = 200;
        static Dictionary<string, GalleryItem> itemLookup = null;

        public static GalleryItem GetWithName(string name)
        {
            if(itemLookup == null)
            {
                itemLookup = new Dictionary<string, GalleryItem>();

                foreach(var item in FindObjectsOfType<GalleryItem>())
                {
                    itemLookup[item.name] = item;
                }
            }

            return itemLookup[name];
        }

        public float GetPrice()
        {
            return price;
        }
    }
}
