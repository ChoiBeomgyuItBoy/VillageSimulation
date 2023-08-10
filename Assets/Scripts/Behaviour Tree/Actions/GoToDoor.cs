using ArtGallery.Core;
using UnityEngine;

namespace ArtGallery.BehaviourTree.Actions
{
    [CreateAssetMenu(menuName = "Behaviour Tree/Actions/Go To Door")]
    public class GoToDoor : ActionNode
    {
        [SerializeField] string doorName = "";
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
            
            Door door = Door.GetWithName(doorName);
            Status status = aIController.GoTo(door.transform.position);

            if(status == Status.Success)
            {
                if(!door.IsLocked())
                {
                    door.Open();
                    return Status.Success;
                }

                return Status.Failure;
            }

            return status;
        }

        protected override void OnExit() { }
    }
}
