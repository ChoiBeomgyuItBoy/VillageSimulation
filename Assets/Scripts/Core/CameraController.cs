using UnityEngine;

namespace ArtGallery.Core
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] float speed = 10;
        [SerializeField] float rightRange = 50;
        [SerializeField] float forwardRange = 50;
        [SerializeField] float upRange = 50;
        CharacterController controller;
        Transform mainCamera;

        void Awake()
        {
            controller = GetComponent<CharacterController>();
            mainCamera = Camera.main.transform;
        }

        void Start()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        void Update()
        {
            Move();
            ClampPosition();
        }

        void ClampPosition()
        {
            Vector3 clampedPosition = new Vector3
            (
                Mathf.Clamp(transform.position.x, -rightRange, rightRange),
                Mathf.Clamp(transform.position.y, -upRange, upRange),
                Mathf.Clamp(transform.position.z, -forwardRange, forwardRange)
            );

            transform.position = clampedPosition;
        }

        void Move()
        {
            Vector3 right = (GetInputValue().x * mainCamera.right).normalized;
            right.y = 0;

            Vector3 forward = (GetInputValue().z * mainCamera.forward).normalized;
            forward.y = 0;

            Vector3 up = (GetInputValue().y * mainCamera.up).normalized;
            up.x = 0;
            up.z = 0;

            Vector3 movementDirection = right + up + forward;
            controller.Move(movementDirection * speed * Time.deltaTime);
        }
        
        Vector3 GetInputValue()
        {
            return new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Jump"), Input.GetAxisRaw("Vertical"));
        }
    }   
}
