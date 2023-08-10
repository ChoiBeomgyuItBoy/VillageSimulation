using UnityEngine;
using UnityEngine.AI;

namespace ArtGallery.BehaviourTree
{
    public class AIController : TreeController
    {
        [SerializeField] float destinationTollerance = 5;
        NavMeshAgent agent = null;
        ActionState state = ActionState.Idle;

        enum ActionState
        {
            Idle,
            Working
        }

        public Status GoTo(Vector3 destination)
        {
            float distanceToDestination = Vector3.Distance(destination, transform.position);

            if(state == ActionState.Idle)
            {
                agent.SetDestination(destination);
                state = ActionState.Working;
            }
            else if(Vector3.Distance(agent.pathEndPosition, destination) >= destinationTollerance)
            {
                state = ActionState.Idle;
                return Status.Failure;
            }
            else if(distanceToDestination < destinationTollerance)
            {
                state = ActionState.Idle;
                return Status.Success;
            }

            return Status.Running;
        }

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
        }
    }
}
