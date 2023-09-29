using ArtGallery.Villagers;
using UnityEngine;
using static ArtGallery.Villagers.Villager;

namespace ArtGallery.BehaviourTree.Actions
{
    public class EquipPrefab : ActionNode
    {
        [SerializeField] GameObject equippedPrefab;
        [SerializeField] EquipType equipType;
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
                villager.Equip(equippedPrefab, equipType);
            }
        }

        protected override Status OnTick()
        {
            return Status.Success;
        }

        protected override void OnExit() { }
    }
}
