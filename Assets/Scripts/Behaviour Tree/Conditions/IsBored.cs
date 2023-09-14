using ArtGallery.Villagers;
using UnityEngine;

namespace ArtGallery.BehaviourTree.Conditions
{
    public class IsBored : ActionNode
    {
        [SerializeField] float boredomThreshold = 100;
        Boredom boredom;

        protected override void OnEnter()
        {
            boredom = controller.GetComponent<Boredom>();
        }

        protected override Status OnTick()
        {
            if(boredom.GetBoredom() < boredomThreshold)
            {
                return Status.Failure;
            }
            else
            {
                return Status.Success;
            }
        }

        protected override void OnExit() { }
    }
}
