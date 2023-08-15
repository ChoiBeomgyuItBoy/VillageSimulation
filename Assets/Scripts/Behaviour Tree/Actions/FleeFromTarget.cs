using ArtGallery.Core;
using UnityEngine;
using UnityEngine.AI;

namespace ArtGallery.BehaviourTree.Actions
{
    public class FleeFromTarget : GoToDestination
    {
        [SerializeField] float distance = 10;
        NavMeshAgent agent = null;
        Targeter targeter = null;
        Vector3 rememberedLocation = Vector3.zero;

        protected override void OnEnter()
        {
            agent = controller.GetComponent<NavMeshAgent>();
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

            return GoTo(agent, rememberedLocation);
        }

        protected override void OnExit() { }
    }
}