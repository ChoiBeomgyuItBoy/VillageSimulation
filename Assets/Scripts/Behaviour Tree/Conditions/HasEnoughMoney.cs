using ArtGallery.Core;
using UnityEngine;

namespace ArtGallery.BehaviourTree.Conditions
{
    [CreateAssetMenu(menuName = "Behaviour Tree/Conditions/Has Enough Money")]
    public class HasEnoughMoney : ActionNode
    {
        [SerializeField] float moneyThreshold = 500;
        Purse purse = null;

        protected override void OnEnter()
        {
            if(controller == null) return;
            purse = controller.GetComponent<Purse>();
        }

        protected override Status OnTick()
        {
            if(purse == null)
            {
                return Status.Failure;
            }

            if(purse.GetBalance() >= moneyThreshold)
            {
                return Status.Failure;
            }

            return Status.Success;
        }

        protected override void OnExit() { }
    }
}
