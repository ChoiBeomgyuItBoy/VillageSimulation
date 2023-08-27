using System.Linq;
using UnityEngine;

namespace ArtGallery.BehaviourTree
{
    public class Loop : CompositeNode
    {
        [SerializeField] BehaviourTree dependencyTree = null;
        int currentChild = 0;
        bool cloned = false;

        protected override void OnEnter()
        {
            currentChild = 0;
        }

        protected override Status OnTick()
        {
            if(dependencyTree != null)
            {
                if(!cloned)
                {
                    dependencyTree = dependencyTree.Clone();
                    cloned = true;
                }   

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

            if(currentChild == GetChildren().Count())
            {
                currentChild = 0;
            }

            return Status.Running;
        }

        protected override void OnExit() { }
    }   
}
