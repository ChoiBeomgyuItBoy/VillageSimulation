using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ArtGallery.Inventories
{
    public class GalleryItem : MonoBehaviour
    {
        [SerializeField] float price = 200;
        static Dictionary<string, GalleryItem> itemLookup = null;

        public static GalleryItem GetWithName(string name)
        {
            if(itemLookup == null)
            {
                BuildLookup();
            }

            return itemLookup[name];
        }

        public static GalleryItem GetRandom()
        {
            if(itemLookup == null)
            {
                BuildLookup();
            }

            return itemLookup[GetRandomItemName()];
        }

        private static string GetRandomItemName()
        {
            if(itemLookup == null)
            {
                BuildLookup();
            }

            List<string> names = new List<string>(itemLookup.Keys.Where((name) => itemLookup[name].gameObject.activeSelf));

            return names[new System.Random().Next(names.Count)];
        }

        private static void BuildLookup()
        {
            itemLookup = new Dictionary<string, GalleryItem>();

            foreach (var item in FindObjectsOfType<GalleryItem>())
            {
                itemLookup[item.name] = item;
            }
        }

        public float GetPrice()
        {
            return price;
        }
    }
}
