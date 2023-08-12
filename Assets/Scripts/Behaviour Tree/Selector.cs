using System.Linq;
using UnityEngine;

namespace ArtGallery.BehaviourTree
{
    public class Selector : CompositeNode
    {
        [SerializeField] bool selectByPriority = false;
        int currentChild = 0;

        protected override void OnEnter()
        {
            currentChild = 0;

            if(selectByPriority)
            {
                SortChildren();
            }
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

            return currentChild == GetChildren().Count() ? Status.Failure : Status.Running;
        }

        protected override void OnExit() { }
    }
}