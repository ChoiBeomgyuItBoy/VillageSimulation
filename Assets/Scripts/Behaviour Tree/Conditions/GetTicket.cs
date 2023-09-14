using ArtGallery.Villagers;

namespace ArtGallery.BehaviourTree.Conditions
{
    public class GetTicket : ActionNode
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