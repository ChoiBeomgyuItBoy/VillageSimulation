using System;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace ArtGallery.BehaviourTree.Editor
{
    public class NodeView : UnityEditor.Experimental.GraphView.Node
    {
        Node node = null;
        BehaviourTree tree = null;
        BehaviourTreeView treeView = null;
        Port inputPort = null;
        Port outputPort = null;

        public Action<NodeView> onNodeSelected;

        public NodeView(Node node, BehaviourTree tree, BehaviourTreeView treeView) 
        : base("Assets/Scripts/Behaviour Tree/Editor/NodeView.uxml")
        {
            this.node = node;
            this.tree = tree;
            this.treeView = treeView;
            this.title = $"[{node.name}]";
            this.viewDataKey = node.GetUniqueID();

            style.left = node.GetPosition().x;
            style.top = node.GetPosition().y;

            CreateInputPort();
            CreateOutputPorts();
            SetupClasses();

            Label descriptionLabel = this.Q<Label>("description");
            descriptionLabel.bindingPath = "description";
            descriptionLabel.Bind(new SerializedObject(node));
        }

        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            if(node is RootNode) return;
            evt.menu.AppendAction("Delete", (a) => DeleteNode());
            evt.menu.AppendAction("Duplicate", (a) => treeView.CreateNodeCopy(node));
        }

        public override void OnSelected()
        {
            onNodeSelected?.Invoke(this);
        }

        public override void SetPosition(Rect newPos)
        {
            base.SetPosition(newPos);
            Undo.RecordObject(node, "(Behaviour Tree) Moved node");
            node.SetPosition(new Vector2(newPos.x, newPos.y));
            EditorUtility.SetDirty(node);
        }

        public void SortChildren()
        {
            CompositeNode composite = node as CompositeNode;

            if(composite != null)
            {
                composite.SortChildrenByHorizontalPosition();
            }
        }

        public void UpdateState()
        {
            RemoveFromClassList("running");
            RemoveFromClassList("success");
            RemoveFromClassList("failure");

            if(!Application.isPlaying) return;

            switch (node.GetStatus())
            {
                case Status.Running:
                    if(node.Started())
                    {
                        AddToClassList("running");
                    }
                    break;
                case Status.Success:
                    AddToClassList("success");
                    break;
                case Status.Failure:
                    AddToClassList("failure");
                    break;
            }
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
            DisconnectPorts(inputContainer);
            DisconnectPorts(outputContainer);
            tree.DeleteNode(node);
            this.RemoveFromHierarchy();
        }

        private void DisconnectPorts(VisualElement container)
        {
            foreach(Port port in container.Children())
            {
                if(!port.connected) continue;

                treeView.DeleteElements(port.connections);
            }
        }

        private void CreateOutputPorts()
        {
            if(node is RootNode)
            {
                outputPort = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Single, typeof(Node));
            }
            else if(node is DecoratorNode)
            {
                outputPort = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Single, typeof(Node));
            }
            else if(node is CompositeNode)
            {
                outputPort = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Multi, typeof(Node));
            }

            if(outputPort != null)
            {
                outputPort.portName = "";
                outputPort.style.flexDirection = FlexDirection.ColumnReverse;
                outputContainer.Add(outputPort);
            }
        }

        private void CreateInputPort()
        {
            if(node is RootNode) return;

            inputPort = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(Node));

            if(inputPort != null)
            {
                inputPort.portName = "";
                inputPort.style.flexDirection = FlexDirection.Column;
                inputContainer.Add(inputPort);
            }
        }

        private void SetupClasses()
        {
            if(node is RootNode)
            {
                AddToClassList("root");
            }
            else if(node is ActionNode)
            {
                AddToClassList("action");
            }
            else if(node is DecoratorNode)
            {
                AddToClassList("decorator");
            }
            else if(node is CompositeNode)
            {
                AddToClassList("composite");
            }
        }
    }
}
