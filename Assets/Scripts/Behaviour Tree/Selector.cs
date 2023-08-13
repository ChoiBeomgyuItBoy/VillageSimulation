using System.Linq;
using UnityEngine;

namespace ArtGallery.BehaviourTree
{
    public class Selector : CompositeNode
    {
        [SerializeField] SelectionType selectionType = SelectionType.FirstToBeSuccessful;
        int currentChild = 0;

        enum SelectionType
        {
            FirstToBeSuccessful,
            ByPriority,
            Random
        }

        protected override void OnEnter()
        {
            currentChild = 0;

            switch(selectionType)
            {
                case SelectionType.ByPriority:
                    SortChildrenByPriority();
                    break;
                case SelectionType.Random:
                    ShuffleChildren();
                    break;
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