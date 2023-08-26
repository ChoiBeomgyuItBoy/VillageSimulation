using ArtGallery.Core;
using UnityEngine;

namespace ArtGallery.UI
{
    public class BoredomUI : MonoBehaviour
    {
        [SerializeField] Boredom boredom;
        [SerializeField] RectTransform foreground;

        void Update()
        {
            FillBoredomBar();
        }

        void FillBoredomBar()
        {
            foreground.localScale = new Vector3(boredom.GetBoredomFraction(), 1, 1);
        }
    }
}