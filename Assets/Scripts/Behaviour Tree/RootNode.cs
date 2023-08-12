using Microsoft.Unity.VisualStudio.Editor;
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
            node.SetChild(child.Clone());
            return node;
        }

        protected override void OnEnter() { }

        protected override Status OnTick()
        {
            return child.Tick(controller);
        }

        protected override void OnExit() { }
    }
}