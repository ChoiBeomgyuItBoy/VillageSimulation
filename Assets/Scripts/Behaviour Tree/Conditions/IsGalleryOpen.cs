using ArtGallery.Core;
using UnityEngine;

namespace ArtGallery.BehaviourTree.Conditions
{
    public class IsGalleryOpen : ActionNode
    {
        Gallery gallery;

        protected override void OnEnter()
        {
            gallery = FindObjectOfType<Gallery>();
        }

        protected override Status OnTick()
        {
            if(gallery.IsOpen())
            {
                return Status.Success;
            }

            return Status.Failure;
        }

        protected override void OnExit() { }
    }
}