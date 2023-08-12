using ArtGallery.Core;
using UnityEngine;
using UnityEngine.AI;

namespace ArtGallery.BehaviourTree.Actions
{
    public class GoToDoor : GoToDestination
    {
        [SerializeField] string doorName = "";
        NavMeshAgent agent = null;

        protected override void OnEnter()
        {
            agent = controller.GetComponent<NavMeshAgent>();
        }

        protected override Status OnTick()
        {
            Door door = Door.GetWithName(doorName);
            Status status = GoTo(agent, door.transform.position);

            if(status == Status.Success)
            {
                if(!door.IsLocked())
                {
                    door.Open();
                    return Status.Success;
                }

                return Status.Failure;
            }

            return status;
        }

        protected override void OnExit() { }
    }
}
