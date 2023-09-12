using UnityEngine;

namespace ArtGallery.Core
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] float speed = 10;
        [SerializeField] float rotationDamping = 10;
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

        void Update()
        {
            Move();
            FaceMovementDirection();
            ClampPosition();
        }

        void Move()
        {
            controller.Move(GetMovementDirection() * speed * Time.deltaTime);
        }

        void FaceMovementDirection()
        {
            Vector3 rotationDirection = new Vector3(GetMovementDirection().x, 0, GetMovementDirection().z);

            if(rotationDirection == Vector3.zero) return;

            transform.rotation = Quaternion.Lerp
            (
                transform.rotation, 
                Quaternion.LookRotation(rotationDirection),
                Time.deltaTime * rotationDamping
            );
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

        Vector3 GetMovementDirection()
        {
            Vector3 right = (GetInputValue().x * mainCamera.right).normalized;
            right.y = 0;

            Vector3 forward = (GetInputValue().z * mainCamera.forward).normalized;
            forward.y = 0;

            Vector3 up = (GetInputValue().y * mainCamera.up).normalized;
            up.x = 0;
            up.z = 0;

            return right + up + forward;
        }
        
        Vector3 GetInputValue()
        {
            return new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Jump"), Input.GetAxisRaw("Vertical"));
        }
    }   
}
