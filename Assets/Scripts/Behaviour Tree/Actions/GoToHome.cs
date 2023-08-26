using ArtGallery.Core;
using UnityEngine;

namespace ArtGallery.BehaviourTree.Actions
{
    public class GoToHome : GoToDestination
    {
        Vector3 homeLocation;

        protected override void OnEnter()
        {
            homeLocation = controller.GetComponent<Homeowner>().GetHomeLocation();
        }

        protected override Status OnTick()
        {
            Status status = GoTo(homeLocation);

            return status;
        }

        protected override void OnExit() { }
    }
}