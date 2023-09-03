using ArtGallery.Core;

namespace ArtGallery.BehaviourTree.Conditions
{
    public class HasTicket : ActionNode
    {
        Patron patron;

        protected override void OnEnter()
        {
            patron = controller.GetComponent<Patron>();
        }

        protected override Status OnTick()
        {
            if(patron.HasTicket())
            {
                return Status.Success;
            }

            return Status.Failure;
        }

        protected override void OnExit() { }
    }
}