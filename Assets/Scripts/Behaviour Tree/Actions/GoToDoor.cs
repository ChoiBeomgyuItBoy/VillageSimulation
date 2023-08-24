using ArtGallery.Core;
using UnityEngine;

namespace ArtGallery.BehaviourTree.Actions
{
    public class GoToDoor : GoToDestination
    {
        [SerializeField] string doorName = "";

        protected override void OnEnter() { }

        protected override Status OnTick()
        {
            Door door = Door.GetWithName(doorName);

            if(door.Visited() && door.IsLocked())
            {
                return Status.Failure;
            }

            Status status = GoTo(door.transform.position);

            if(status == Status.Success)
            {
                if(door.Open())
                {
                    return Status.Success;
                }

                return Status.Failure;
            }

            return status;
        }

        protected override void OnExit() { }
    }
}
