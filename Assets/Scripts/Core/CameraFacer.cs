using UnityEngine;

namespace ArtGallery.Core
{
    public class CameraFacer : MonoBehaviour
    {
        Transform mainCameraTransform;

        void Start()
        {
            mainCameraTransform = Camera.main.transform;
        }

        void LateUpdate()
        {
            transform.forward = mainCameraTransform.transform.forward;
        }
    }
}
