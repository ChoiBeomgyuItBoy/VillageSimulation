using ArtGallery.Core;

namespace ArtGallery.BehaviourTree.Conditions
{
    public class WaitingForTicket : ActionNode
    {
        Gallery gallery;
        Patron patron;

        protected override void OnEnter()
        {
            gallery = FindObjectOfType<Gallery>();
            patron = controller.GetComponent<Patron>();
        }

        protected override Status OnTick()
        {
            if(gallery.RegisterPatron(patron))
            {
                return Status.Success;
            }

            return Status.Failure;
        }

        protected override void OnExit() { }
    }
}