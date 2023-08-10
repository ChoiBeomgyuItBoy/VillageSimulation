using UnityEngine;

namespace ArtGallery.BehaviourTree
{
    public abstract class DecoratorNode : Node
    {
        [SerializeField] Node child;

        protected Node GetChild()
        {
            return child;
        }
    }
}