using UnityEngine;

namespace ArtGallery.Core
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] public float speed = 10.0f;
        [SerializeField] float rotationSpeed = 100.0f;

        void Update()
        {
            float translation = Input.GetAxis("Vertical") * speed;
            float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

            translation *= Time.deltaTime;
            rotation *= Time.deltaTime;
            transform.Translate(0, 0, translation);
            transform.Rotate(0, rotation, 0);
        }
    }
}