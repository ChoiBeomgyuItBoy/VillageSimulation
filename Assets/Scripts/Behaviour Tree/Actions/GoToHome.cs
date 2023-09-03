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
            return GoTo(homeLocation);
        }

        protected override void OnExit() { }
    }
}