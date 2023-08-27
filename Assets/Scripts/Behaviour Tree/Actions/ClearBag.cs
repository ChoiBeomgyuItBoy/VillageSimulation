using ArtGallery.Core;
using UnityEngine;

namespace ArtGallery.BehaviourTree.Actions
{
    public class ClearBag : GoToDestination
    {
        [SerializeField] bool depositItems = true;
        Bag bag = null;

        protected override void OnEnter()
        {
            bag = controller.GetComponent<Bag>();
        }

        protected override Status OnTick()
        {
            Status status = GoTo(bag.GetDepositLocation());

            if(status == Status.Success)
            {
                if(depositItems && bag.HasItems())
                {
                    bag.SellItems();
                }
            }

            return status;
        }

        protected override void OnExit() { }
    }
}
