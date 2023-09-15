using ArtGallery.Villagers;
using UnityEngine;

namespace ArtGallery.BehaviourTree.Actions
{
    public class EquipPrefab : ActionNode
    {
        [SerializeField] GameObject equippedPrefab;
        [SerializeField] bool isRightHanded = true;
        [SerializeField] bool unequip = false;

        protected override void OnEnter()
        {
            Villager villager = controller.GetComponent<Villager>();

            if(unequip)
            {
                villager.Unequip(equippedPrefab);
            }
            else
            {
                villager.Equip(equippedPrefab, isRightHanded);
            }
        }

        protected override Status OnTick()
        {
            return Status.Success;
        }

        protected override void OnExit() { }
    }
}
