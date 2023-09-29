using UnityEngine;

namespace ArtGallery.BehaviourTree
{
    public class Loop : DependencyDecorator
    {
        protected override void OnEnter() { } 

        protected override Status OnTick()
        {
            BehaviourTree dependency = GetDependencyTree();

            if(dependency != null)
            {
                if(GetDependencyTree().Tick() == Status.Failure)
                {
                    return Status.Success;
                }
            }

            GetChild().Tick();

            return Status.Running;
        }

        protected override void OnExit() { }
    }
}