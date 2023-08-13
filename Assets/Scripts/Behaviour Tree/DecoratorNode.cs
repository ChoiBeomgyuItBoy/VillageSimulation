using UnityEngine;

namespace ArtGallery.BehaviourTree
{
    public abstract class DecoratorNode : Node
    {
        [SerializeField] Node child;

        public Node GetChild()
        {
            return child;
        }

        public void SetChild(Node child)
        {
            this.child = child;
        }
    }
}