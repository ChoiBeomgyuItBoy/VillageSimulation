using System.Linq;
using UnityEngine;

namespace ArtGallery.BehaviourTree
{
    public class Selector : CompositeNode
    {
        [SerializeField] SelectionType selectionType = SelectionType.FirstToBeSuccessful;
        const int highestPriority = 10;
        const int lowestPriority = 1;
        int currentChild = 0;

        enum SelectionType
        {
            FirstToBeSuccessful,
            Priority,
            DynamicPriority,
            Random
        }

        protected override void OnEnter()
        {
            currentChild = 0;

            switch(selectionType)
            {
                case SelectionType.Priority:
                    SortChildrenByPriority();
                    break;
                case SelectionType.DynamicPriority:
                    SortChildrenByPriority();
                    break;
                case SelectionType.Random:
                    ShuffleChildren();
                    break;
            }
        }

        protected override Status OnTick()
        {
            Status childStatus = GetChild(currentChild).Tick();

            switch(childStatus)
            {
                case Status.Running:
                    return Status.Running;

                case Status.Success:
                    if(selectionType == SelectionType.DynamicPriority)
                    {
                        GetChild(currentChild).SetPriority(highestPriority);
                    }
                    return Status.Success;

                case Status.Failure:
                    if(selectionType == SelectionType.DynamicPriority)
                    {
                        GetChild(currentChild).SetPriority(lowestPriority);
                    }
                    currentChild++;
                    break;
            }

            return currentChild == GetChildren().Count() ? Status.Failure : Status.Running;
        }

        protected override void OnExit() { }
    }
}