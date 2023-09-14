using ArtGallery.Villagers;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;

namespace ArtGallery.UI
{
    public class OccupationUI : MonoBehaviour
    {
        [SerializeField] Villager villager;

        void Start()
        {
            string occupation = Regex.Replace(villager.GetOccupation().ToString(), "(\\B[A-Z])", " $1");
            GetComponent<TMP_Text>().text = $"- {occupation} -";
        }
    }
}