using System.Linq;
using UnityEngine;

namespace ArtGallery.BehaviourTree
{
    public class Sequence : CompositeNode
    {
        [SerializeField] BehaviourTree dependencyTree = null;
        int currentChild = 0;

        protected override void OnEnter()
        {
            currentChild = 0;
        }

        protected override Status OnTick()
        {
            if(dependencyTree != null)
            {
                if(dependencyTree.Tick(controller) == Status.Failure)
                {
                    return Status.Failure;
                }
            }

            Status childStatus = GetChild(currentChild).Tick(controller);

            switch(childStatus)
            {
                case Status.Running:
                    return Status.Running;
                case Status.Failure:
                    return Status.Failure;
                case Status.Success:
                    currentChild++;
                    break;
            }

            return currentChild == GetChildren().Count() ? Status.Success : Status.Running;
        }

        protected override void OnExit() { }
    }
}
