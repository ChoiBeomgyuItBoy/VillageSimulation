using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace ArtGallery.BehaviourTree.Editor
{
    public class BehaviourTreeView : GraphView
    {
        BehaviourTree tree = null;

        public Action<NodeView> onNodeSelected;

        public new class UxmlFactory : UxmlFactory<BehaviourTreeView, GraphView.UxmlTraits> { }

        public BehaviourTreeView()
        {
            Insert(0, new GridBackground());

            this.AddManipulator(new ContentZoomer());
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());


            StyleSheet styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>
            (
                "Assets/Scripts/Behaviour Tree/Editor/BehaviourTreeEditor.uss"
            );

            styleSheets.Add(styleSheet);
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            return ports.ToList().Where 
            (
                endPort => endPort.direction != startPort.direction &&
                endPort.node != startPort.node 
                ).ToList();
        }

        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            if(tree == null) return;

            AddToContextMenu<ActionNode>(evt);
            AddToContextMenu<DecoratorNode>(evt);
            AddToContextMenu<CompositeNode>(evt);
        }

        public void PopulateView(BehaviourTree tree)
        {
            this.tree = tree;

            graphViewChanged -= OnGraphViewChanged;
            DeleteElements(graphElements);
            graphViewChanged += OnGraphViewChanged;

            foreach(var node in tree.GetNodes())
            {
                CreateNodeView(node);

                foreach(var child in tree.GetChildren(node))
                {
                    CreateEdges(node, child);
                }
            }
        }

        private void CreateNode(Type type)
        {
            Node node = tree.CreateNode(type);
            CreateNodeView(node);
        }

        private void CreateNodeView(Node node)
        {
            NodeView nodeView = new NodeView(node, tree);
            nodeView.onNodeSelected = onNodeSelected;
            AddElement(nodeView);
        }

        private NodeView GetNodeView(Node node)
        {
            return GetNodeByGuid(node.GetUniqueID()) as NodeView;
        }

        private void CreateEdges(Node parent, Node child)
        {
            NodeView parentView = GetNodeView(parent);
            NodeView childView = GetNodeView(child);

            if(parentView == null || childView == null) return;

            Edge edge = parentView.ConnectTo(childView);
            AddElement(edge);
        }

        private GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange)
        {
            var elementsToRemove = graphViewChange.elementsToRemove;

            if(elementsToRemove != null)
            {
                foreach(var element in elementsToRemove)
                {
                    Edge edge = element as Edge;

                    if(edge != null)
                    {
                        NodeView parentView = edge.output.node as NodeView;
                        NodeView childView = edge.input.node as NodeView;
                        tree.RemoveChild(parentView.GetNode(), childView.GetNode());
                    }
                }
            }

            var edgesToCreate = graphViewChange.edgesToCreate;

            if(edgesToCreate != null)
            {   
                foreach(var edge in edgesToCreate)
                {
                    NodeView parentView = edge.output.node as NodeView;
                    NodeView childView = edge.input.node as NodeView;
                    tree.AddChild(parentView.GetNode(), childView.GetNode());
                }
            }

            return graphViewChange;
        }

        private void AddToContextMenu<T>(ContextualMenuPopulateEvent evt)
        {
            var types = TypeCache.GetTypesDerivedFrom<T>();

            foreach(var type in types)
            {
                if(type.IsAbstract) continue;

                string typeName = Regex.Replace(typeof(T).Name, @"([a-z])([A-Z0-9])", "$1 $2");
                string concreteName = Regex.Replace(type.Name, @"([a-z])([A-Z0-9])", "$1 $2");

                evt.menu.AppendAction($"{typeName}/{concreteName}", (action) => CreateNode(type));
            }
        }
    }
}
