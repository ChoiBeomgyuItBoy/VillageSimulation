using ArtGallery.Core;
using UnityEngine;

namespace ArtGallery.BehaviourTree.Actions
{
    [CreateAssetMenu(menuName = "Behaviour Tree/Actions/Go To Gallery Item")]
    public class GoToGalleryItem : ActionNode
    {
        [SerializeField] string itemName = "";
        [SerializeField] bool addToBag = true;
        [SerializeField] bool clearBagIfSucceded = false;
        AIController aIController;

        protected override void OnEnter()
        {
            aIController = controller as AIController;
        }

        protected override Status OnTick()
        {
            if(aIController == null)
            {
                return Status.Failure;
            }

            GalleryItem item = GalleryItem.GetWithName(itemName);

            if(!item.gameObject.activeSelf)
            {
                return Status.Failure;
            }

            Status status = aIController.GoTo(item.transform.position);

            if(status == Status.Success && addToBag)
            {
                aIController.GetComponent<Bag>().AddItem(item);
            }

            if(status == Status.Success && clearBagIfSucceded)
            {
                aIController.GetComponent<Bag>().SellItems();
            }

            return status;
        }

        protected override void OnExit() { }
    }
}
