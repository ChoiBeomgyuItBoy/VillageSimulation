using UnityEngine;

namespace ArtGallery.BehaviourTree
{
    public class RootNode : Node
    {
        [SerializeField] Node child = null;

        public void SetChild(Node child)
        {
            this.child = child;
        }

        public Node GetChild()
        {
            return child;
        }

        protected override void OnEnter() { }

        protected override Status OnTick()
        {
            return child.Tick(controller);
        }

        protected override void OnExit() { }
    }
}
