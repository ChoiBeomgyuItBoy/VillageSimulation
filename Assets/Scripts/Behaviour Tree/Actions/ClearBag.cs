using ArtGallery.Core;

namespace ArtGallery.BehaviourTree.Actions
{
    public class ClearBag : GoToDestination
    {
        Bag bag = null;

        protected override void OnEnter()
        {
            bag = controller.GetComponent<Bag>();
        }

        protected override Status OnTick()
        {
            if(!bag.HasItems())
            {
                return Status.Failure;
            }

            Status status = GoTo(bag.GetDepositLocation());

            if(status == Status.Success)
            {
                if(bag.HasItems())
                {
                    bag.SellItems();
                    return Status.Success;
                }
                else
                {
                    return Status.Failure;
                }
            }

            return status;
        }

        protected override void OnExit() { }
    }
}
