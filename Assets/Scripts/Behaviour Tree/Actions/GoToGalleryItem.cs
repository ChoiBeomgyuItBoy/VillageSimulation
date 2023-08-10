using ArtGallery.Core;
using UnityEngine;
using UnityEngine.AI;

namespace ArtGallery.BehaviourTree.Actions
{
    [CreateAssetMenu(menuName = "Behaviour Tree/Actions/Go To Gallery Item")]
    public class GoToGalleryItem : GoToDestination
    {
        [SerializeField] string itemName = "";
        [SerializeField] bool addToBag = true;
        [SerializeField] bool clearBagIfSucceded = false;
        NavMeshAgent agent = null;

        protected override void OnEnter()
        {
            agent = controller.GetComponent<NavMeshAgent>();
        }

        protected override Status OnTick()
        {
            GalleryItem item = GalleryItem.GetWithName(itemName);

            if(!item.gameObject.activeSelf)
            {
                return Status.Failure;
            }

            Status status = GoTo(agent, item.transform.position);

            if(status == Status.Success && addToBag)
            {
                controller.GetComponent<Bag>().AddItem(item);
            }

            if(status == Status.Success && clearBagIfSucceded)
            {
                controller.GetComponent<Bag>().SellItems();
            }

            return status;
        }

        protected override void OnExit() { }
    }
}
