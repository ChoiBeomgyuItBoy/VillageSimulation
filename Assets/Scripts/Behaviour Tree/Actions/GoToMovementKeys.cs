using UnityEngine;

namespace ArtGallery.BehaviourTree.Actions
{
    public class GoToMovementKeys : GoToDestination
    {
        protected override void OnEnter() { }

        protected override Status OnTick()
        {
            GoTo(GetMovement(), true);
            return Status.Success;
        }

        protected override void OnExit() { }

        private Vector3 GetMovement()
        {
            return new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        }
    }
}