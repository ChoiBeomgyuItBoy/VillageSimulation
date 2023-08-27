using ArtGallery.Inventories;
using ArtGallery.Core;
using UnityEngine;

namespace ArtGallery.BehaviourTree.Actions
{
    public class GoToGalleryItem : GoToDestination
    {
        [SerializeField] string itemName = "";
        [SerializeField] bool randomItem = false;
        [SerializeField] bool addToBag = false;
        [SerializeField] bool reduceBoredom = false;
        GalleryItem item = null;

        protected override void OnEnter()
        {
            if(randomItem)
            {
                item = GalleryItem.GetRandom();
            }
            else
            {
                item = GalleryItem.GetWithName(itemName);
            }
        }

        protected override Status OnTick()
        {
            if(item == null)
            {
                return Status.Failure;
            }

            if(!item.gameObject.activeSelf)
            {
                return Status.Failure;
            }

            Status status = GoTo(item.transform.position);

            if(status == Status.Success && addToBag)
            {
                controller.GetComponent<Bag>().AddItem(item);
            }

            if(status == Status.Success && reduceBoredom)
            {
                controller.GetComponent<Boredom>().ChangeBoredom(-item.GetInterestLevel());
            }

            return status;
        }

        protected override void OnExit() { }
    }
}
