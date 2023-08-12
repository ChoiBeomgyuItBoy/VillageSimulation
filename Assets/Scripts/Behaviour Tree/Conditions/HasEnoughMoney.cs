using ArtGallery.Core;
using UnityEngine;

namespace ArtGallery.BehaviourTree.Conditions
{
    public class HasEnoughMoney : ActionNode
    {
        [SerializeField] float moneyThreshold = 500;
        Purse purse = null;

        protected override void OnEnter()
        {
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
                return Status.Success;
            }

            return Status.Failure;
        }

        protected override void OnExit() { }
    }
}
