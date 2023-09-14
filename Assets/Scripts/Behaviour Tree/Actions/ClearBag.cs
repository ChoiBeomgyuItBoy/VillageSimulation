using ArtGallery.Inventories;

namespace ArtGallery.BehaviourTree.Actions
{
    public class ClearBag : GoToDestination
    {
        Bag bag;

        protected override void OnEnter()
        {
            bag = controller.GetComponent<Bag>();
        }

        protected override Status OnTick()
        {
            if(bag.HasItems())
            {
                bag.SellItems();
                return Status.Success;
            }

            return Status.Failure;
        }

        protected override void OnExit() { }
    }
}
