using System.Linq;
using UnityEngine;

namespace ArtGallery.BehaviourTree
{
    public class Sequence : DependencyComposite
    {
        int currentChild = 0;

        protected override void OnEnter()
        {
            currentChild = 0;
        }

        protected override Status OnTick()
        {
            BehaviourTree dependency = GetDependencyTree();

            if(dependency != null)
            {
                if(dependency.Tick() == Status.Failure)
                {
                    return Status.Failure;
                }
            }

            Status childStatus = GetChild(currentChild).Tick();

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
