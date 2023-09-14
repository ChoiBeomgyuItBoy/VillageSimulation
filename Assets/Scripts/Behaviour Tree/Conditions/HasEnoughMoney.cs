using ArtGallery.Inventories;

namespace ArtGallery.BehaviourTree.Conditions
{
    public class HasEnoughMoney : ActionNode
    {
        Purse purse;

        protected override void OnEnter()
        {
            purse = controller.GetComponent<Purse>();
        }

        protected override Status OnTick()
        {
            if(purse == null)
            {
                return Status.Failure;
            }

            if(purse.GetBalance() >= purse.GetMaxBalance())
            {
                return Status.Success;
            }

            return Status.Failure;
        }

        protected override void OnExit() { }
    }
}
