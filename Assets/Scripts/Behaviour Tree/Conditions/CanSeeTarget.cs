using ArtGallery.Core;
using UnityEngine;

namespace ArtGallery.BehaviourTree.Conditions
{
    public class CanSeeTarget : ActionNode
    {
        [SerializeField] float viewDistance = 10;
        [SerializeField] float maxViewAngle = 90;
        Targeter targeter = null;

        protected override void OnEnter()
        {
            targeter = controller.GetComponent<Targeter>();
        }

        protected override Status OnTick()
        {
            if(targeter.CanSeeTarget(viewDistance, maxViewAngle))
            {
                return Status.Success;
            }

            return Status.Failure;
        }

        protected override void OnExit() { }   
    }
}
