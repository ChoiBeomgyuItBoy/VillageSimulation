using UnityEngine;
using ArtGallery.Villagers;

namespace ArtGallery.BehaviourTree.Actions
{
    public class GoToLocation : GoToDestination
    {
        [SerializeField] Location location;
        Vector3 locationPosition;

        protected override void OnEnter()
        {
            locationPosition = controller.GetComponent<Villager>().GetLocation(location);
        }

        protected override Status OnTick()
        {
            Status status = GoTo(locationPosition);

            if(status == Status.Success)
            {
                return Status.Success;
            }

            return Status.Running;
        }

        protected override void OnExit() { }
    }
}
