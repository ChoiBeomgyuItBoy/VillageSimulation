using UnityEngine;

namespace ArtGallery.BehaviourTree
{
    [CreateAssetMenu(menuName = "Behaviour Tree/Selector")]
    public class Selector : CompositeNode
    {
        int currentChild = 0;

        protected override void OnEnter()
        {
            currentChild = 0;
        }

        protected override Status OnTick()
        {
            Status childStatus = GetChild(currentChild).Tick(controller);

            switch(childStatus)
            {
                case Status.Running:
                    return Status.Running;
                case Status.Success:
                    return Status.Success;
                case Status.Failure:
                    currentChild++;
                    break;
            }

            return currentChild == GetChildCount() ? Status.Failure : Status.Running;
        }

        protected override void OnExit() { }
    }
}