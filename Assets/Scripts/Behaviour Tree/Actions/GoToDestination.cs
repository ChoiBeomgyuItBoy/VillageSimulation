using UnityEngine;
using UnityEngine.AI;

namespace ArtGallery.BehaviourTree.Actions
{
    public abstract class GoToDestination : ActionNode
    {
        const float destinationTollerance = 5;
        ActionState state = ActionState.Idle;

        enum ActionState
        {
            Idle,
            Working
        }

        protected Status GoTo(NavMeshAgent agent, Vector3 destination)
        {
            float distanceToDestination = Vector3.Distance(destination, agent.transform.position);

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
    }   
}