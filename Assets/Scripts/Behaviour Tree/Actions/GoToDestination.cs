using ArtGallery.Movement;
using UnityEngine;

namespace ArtGallery.BehaviourTree.Actions
{
    public abstract class GoToDestination : ActionNode
    {
        [SerializeField] [Range(0,1)] float speedFraction = 1;
        ActionState state = ActionState.Idle;

        protected enum ActionState
        {
            Idle,
            Working
        }

        protected ActionState GetState()
        {
            return state;
        }

        protected Status GoTo(Vector3 destination, bool isPlayer = false)
        {
            Mover mover = controller.GetComponent<Mover>();

            if(state == ActionState.Idle)
            {
                mover.MoveTo(destination, speedFraction, isPlayer);
                state = ActionState.Working;
            }
            else if(!mover.CanGoTo(destination))
            {
                state = ActionState.Idle;
                return Status.Failure;
            }
            else if(mover.AtDestination(destination))
            {
                state = ActionState.Idle;
                return Status.Success;
            }

            return Status.Running;
        }
    }   
}