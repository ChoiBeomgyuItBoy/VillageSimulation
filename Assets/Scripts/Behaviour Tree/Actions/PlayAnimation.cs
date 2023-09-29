using UnityEngine;

namespace ArtGallery.BehaviourTree.Actions
{
    public class PlayAnimation : ActionNode
    {
        [SerializeField] string animationName = "";
        Animator animator;

        protected override void OnEnter() 
        { 
            animator = controller.GetComponent<Animator>();
            animator.CrossFade(animationName, 0.1f);
        }

        protected override Status OnTick()
        {
            return Status.Success;
        }

        protected override void OnExit() { }
    }
}
