using UnityEngine;

namespace ArtGallery.Core
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] float timeScale = 1;

        void Start()
        {
            Time.timeScale = timeScale;
        }
    }
}
