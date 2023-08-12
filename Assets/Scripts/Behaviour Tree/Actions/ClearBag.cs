using ArtGallery.Core;
using UnityEngine.AI;

namespace ArtGallery.BehaviourTree.Actions
{
    public class ClearBag : GoToDestination
    {
        NavMeshAgent agent = null;
        Bag bag = null;

        protected override void OnEnter()
        {
            agent = controller.GetComponent<NavMeshAgent>();
            bag = controller.GetComponent<Bag>();
        }

        protected override Status OnTick()
        {
            Status status = GoTo(agent, bag.GetDepositLocation());

            if(status == Status.Success)
            {
                controller.GetComponent<Bag>().SellItems();
            }

            return status;
        }

        protected override void OnExit() { }
    }
}
