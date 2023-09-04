using ArtGallery.Core;
using UnityEngine;

namespace ArtGallery.BehaviourTree.Actions
{
    public class ChaseTarget : GoToDestination
    {
        [SerializeField] float chaseDistance = 10;
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
                Vector3 targetLocation = targeter.GetTargetLocation();
                Vector3 controllerLocation = controller.transform.position;
                Vector3 chaseDirection = controllerLocation - (controllerLocation - targetLocation).normalized * chaseDistance;
                rememberedLocation = chaseDirection;
            }

            return GoTo(rememberedLocation);
        }

        protected override void OnExit() { }
    }
}