using ArtGallery.Core;
using UnityEngine;

namespace ArtGallery.BehaviourTree.Actions
{
    public class FleeFromTarget : GoToDestination
    {
        [SerializeField] float distance = 10;
        Targeter targeter;
        Vector3 rememberedLocation;

        protected override void OnEnter()
        {
            targeter = controller.GetComponent<Targeter>();
        }

        protected override Status OnTick()
        {
            if(GetState() == ActionState.Idle)
            {
                Vector3 controllerLocation = controller.transform.position;
                Vector3 targetLocation = targeter.GetTargetLocation();
                Vector3 fleeDirection = controllerLocation + (controllerLocation - targetLocation).normalized * distance;
                rememberedLocation = fleeDirection;
            }

            return GoTo(rememberedLocation);
        }

        protected override void OnExit() { }
    }
}