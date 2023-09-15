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

                Status treeStatus = breakCondition.Tick();

                if(treeStatus == Status.Failure)
                {
                    return Status.Success;
                }
            }

            GetChild().Tick();

            return Status.Running;
        }

        protected override void OnExit() { }

        private void CloneTree()
        {
            if(!cloned)
            {
                breakCondition = breakCondition.Clone();
                breakCondition.Bind(controller);
                cloned = true;
            }  
        }
    }
}