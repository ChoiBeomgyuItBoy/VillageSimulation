using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace ArtGallery.BehaviourTree.Editor
{
    public class NodeView : UnityEditor.Experimental.GraphView.Node
    {
        BehaviourTree tree = null;
        Node node = null;
        Port inputPort = null;
        Port outputPort = null;

        public Action<NodeView> onNodeSelected;

        public NodeView(Node node, BehaviourTree tree)
        {
            this.tree = tree;
            this.node = node;
            this.title = node.name;
            this.viewDataKey = node.GetUniqueID();
            style.left = node.GetPosition().x;
            style.top = node.GetPosition().y;
            CreateInputPort();
            CreateOutputPorts();
        }

        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            evt.menu.AppendAction("Delete", (a) => DeleteNode());
        }

        public override void OnSelected()
        {
            onNodeSelected?.Invoke(this);
        }

        public override void SetPosition(Rect newPos)
        {
            base.SetPosition(newPos);
            node.SetPosition(new Vector2(newPos.x, newPos.y));
        }

        public Node GetNode()
        {
            return node;
        }

        public Edge ConnectTo(NodeView node)
        {
            return outputPort.ConnectTo(node.inputPort);
        }

        private void DeleteNode()
        {
            tree.DeleteNode(node);
            this.RemoveFromHierarchy();
        }

        private void CreateOutputPorts()
        {
            if(node is DecoratorNode)
            {
                outputPort = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(Node));
            }
            else if(node is CompositeNode)
            {
                outputPort = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(Node));
            }

            if(outputPort != null)
            {
                outputPort.portName = "";
                outputContainer.Add(outputPort);
            }
        }

        private void CreateInputPort()
        {
            inputPort = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(Node));

            if(inputPort != null)
            {
                inputPort.portName = "";
                inputContainer.Add(inputPort);
            }
        }
    }
}
