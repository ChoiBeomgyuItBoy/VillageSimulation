using ArtGallery.Core;

namespace ArtGallery.BehaviourTree.Actions
{
    public class GoToPatron : GoToDestination
    {
        Gallery gallery;

        protected override void OnEnter()
        {
            gallery = FindObjectOfType<Gallery>();
        }

        protected override Status OnTick()
        {
            Patron patron = gallery.GetCurrentPatron();

            if(patron == null)
            {
                return Status.Failure;
            }

            Status status = GoTo(patron.transform.position);

            if(status == Status.Success)
            {
                patron.SetTicket(true);
            }

            return status;
        }

        protected override void OnExit() { }
    }
}