using UnityEngine;

namespace ArtGallery.BehaviourTree
{
    public class Inverter : DecoratorNode
    {
        protected override void OnEnter() { }

        protected override Status OnTick()
        {
            Status childStatus = GetChild().Tick();

            switch(childStatus)
            {
                case Status.Running:
                    return Status.Running;
                case Status.Failure:
                    return Status.Success;
                case Status.Success:
                    return Status.Failure;
            }

            return Status.Running;
        }

        protected override void OnExit() { }
    }
}