using UnityEngine;

namespace ArtGallery.BehaviourTree.Editor
{
    public class NodeView : UnityEditor.Experimental.GraphView.Node
    {
        Node node = null;

        public NodeView(Node node)
        {
            this.node = node;
            this.title = node.name;
            this.viewDataKey = node.GetUniqueID();
            style.left = node.GetPosition().x;
            style.top = node.GetPosition().y;
        }

        public Node GetNode()
        {
            return node;
        }

        public override void SetPosition(Rect newPos)
        {
            base.SetPosition(newPos);
            node.SetPosition(new Vector2(newPos.x, newPos.y));
        }
    }
}
