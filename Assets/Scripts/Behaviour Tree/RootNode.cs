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

        public override Node Clone()
        {
            RootNode node = Instantiate(this);
            node.child = child.Clone();
            return node;
        }

        protected override void OnEnter() { }

        protected override Status OnTick()
        {
            if(child == null)
            {
                return Status.Failure;
            }
            
            return child.Tick();
        }

        protected override void OnExit() { }
    }
}
