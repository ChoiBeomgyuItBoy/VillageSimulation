using UnityEngine;

namespace ArtGallery.Core
{
    public class Homeowner : MonoBehaviour
    {
        [SerializeField] GameObject home = null;

        public Vector3 GetHomeLocation()
        {
            return home.transform.position;
        }
    }
}