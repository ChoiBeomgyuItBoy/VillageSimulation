using UnityEngine;

namespace ArtGallery.Core
{
    public class Targeter : MonoBehaviour
    {
        [SerializeField] GameObject target = null;

        public Vector3 GetTargetLocation()
        {
            return target.transform.position;
        }

        public bool CanSeeTarget(float distance, float maxAngle)
        {
            Vector3 directionToTarget = target.transform.position - transform.position;
            float angle = Vector3.Angle(directionToTarget, transform.forward);

            if(angle <= maxAngle || directionToTarget.magnitude <= distance)
            {
                RaycastHit hit;

                if(Physics.Raycast(transform.position, directionToTarget, out hit))
                {
                    if(hit.collider == target.GetComponent<Collider>()) return true;
                }
            }

            return false;
        }
    }
}
