using ArtGallery.Villagers;
using UnityEngine;

namespace ArtGallery.BehaviourTree.Actions
{
    public class ReduceBoredom : ActionNode
    {
        [SerializeField] float boredomReduce = 10;
        [SerializeField] bool reduceEveryFrame = false;
        Boredom boredom;

        protected override void OnEnter()
        {
            boredom = controller.GetComponent<Boredom>();
            boredom.ChangeBoredom(-boredomReduce);
        }

        protected override Status OnTick()
        {
            if(!reduceEveryFrame)
            {
                return Status.Success;
            }
            
            boredom.ChangeBoredom(-Time.deltaTime * boredomReduce);

            if(boredom.GetBoredom() <= 0)
            {
                return Status.Success;
            }
            
            return Status.Running;
        }

        protected override void OnExit() { }
    }
}