using UnityEngine;

namespace ArtGallery.BehaviourTree
{
    public class Loop : DecoratorNode
    {
        [SerializeField] BehaviourTree breakCondition;
        bool cloned = false;

        protected override void OnEnter() { } 

        protected override Status OnTick()
        {
            if(breakCondition != null)
            {
                CloneTree();

                Status treeStatus = breakCondition.Tick(controller);

                if(treeStatus == Status.Failure)
                {
                    return Status.Success;
                }
            }

            GetChild().Tick(controller);

            return Status.Running;
        }

        protected override void OnExit() { }

        private void CloneTree()
        {
            if(!cloned)
            {
                breakCondition = breakCondition.Clone();
                cloned = true;
            }  
        }
    }
}