using UnityEngine;
using ArtGallery.Villagers;

namespace ArtGallery.BehaviourTree.Actions
{
    public class GoToLocation : GoToDestination
    {
        [SerializeField] Location location;
        [SerializeField] int destinationIndex = 0;
        [SerializeField] bool randomDestination = true;
        Vector3 locationPosition;

        protected override void OnEnter()
        {
            Villager villager = controller.GetComponent<Villager>();

            if(randomDestination)
            {
                locationPosition = villager.GetRandomLocation(location);
            }
            else
            {
                locationPosition = villager.GetLocation(location, destinationIndex);
            }
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
