using UnityEngine;

namespace ArtGallery.Core
{
    public class Patroller : MonoBehaviour
    {
        [SerializeField] PatrolPath patrolPath;

        int currentWaypointIndex = 0;

        public void CycleWaypoint()
        {
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
        }

        public Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWaypoint(currentWaypointIndex);
        }
    }
}