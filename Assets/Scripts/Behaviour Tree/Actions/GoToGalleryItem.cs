using ArtGallery.Inventories;
using UnityEngine;

namespace ArtGallery.BehaviourTree.Actions
{
    public class GoToGalleryItem : GoToDestination
    {
        [SerializeField] string itemName = "";
        [SerializeField] bool randomItem = false;
        [SerializeField] bool addToBag = false;
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

            return status;
        }

        protected override void OnExit() { }
    }
}
