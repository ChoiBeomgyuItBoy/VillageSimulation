using ArtGallery.Core;
using UnityEngine;

namespace ArtGallery.BehaviourTree.Actions
{
    public class Patrol : GoToDestination
    {
        Vector3 currentWaypoint;

        protected override void OnEnter()
        {
            currentWaypoint = controller.GetComponent<Patroller>().GetCurrentWaypoint();
        }

        protected override Status OnTick()
        {
            Status status = GoTo(currentWaypoint);
            
            if(status == Status.Success)
            {
                controller.GetComponent<Patroller>().CycleWaypoint();
                return Status.Success;
            }

            return Status.Running;
        }

        protected override void OnExit() { }
    }
}