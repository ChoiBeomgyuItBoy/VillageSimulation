using UnityEngine;

namespace ArtGallery.BehaviourTree.Actions
{
    public class WaitForSeconds : ActionNode
    {
        [SerializeField] float secondsToWait = 3;
        float timer = 0;

        protected override void OnEnter()
        {
            timer = 0;
        }

        protected override Status OnTick()
        {
            timer += Time.deltaTime;

            if(timer >= secondsToWait)
            {
                return Status.Success;
            }

            return Status.Running;
        }

        protected override void OnExit() { }
    }
}
