using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
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

            Undo.undoRedoPerformed += OnUndoRedo;
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

            if(tree.GetRoot() == null)
            {
                tree.SetRoot(tree.CreateNode(typeof(RootNode)) as RootNode);
                EditorUtility.SetDirty(tree);
                AssetDatabase.SaveAssets();
            }

            foreach(var node in tree.GetNodes())
            {
                CreateNodeView(node);
            }

            foreach(var node in tree.GetNodes())
            {
                foreach(var child in tree.GetChildren(node))
                {
                    CreateEdges(node, child);
                }
            }
        }

        public void UpdateNodeStates()
        {
            foreach(var node in nodes)
            {
                NodeView nodeView = node as NodeView;

                if(nodeView != null)
                {
                    nodeView.UpdateState();
                }
            }
        }

        public void CreateNodeCopy(Node node)
        {
            Node copy = tree.CreateCopy(node);
            CreateNodeView(copy);
        }

        private void CreateNode(Type type, Vector2 position)
        {
            Node node = tree.CreateNode(type);
            CreateNodeView(node);
            GetNodeView(node).SetPosition(new Rect(position.x, position.y, 0, 0));
        }

        private void CreateNodeView(Node node)
        {
            NodeView nodeView = new NodeView(node, tree, this);
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

            var movedElements = graphViewChange.movedElements;

            if(movedElements != null)
            {
                foreach(var node in nodes)
                {
                    NodeView nodeView = node as NodeView;
                    nodeView.SortChildren();
                }
            }

            return graphViewChange;
        }

        private void AddToContextMenu<T>(ContextualMenuPopulateEvent evt)
        {
            var types = TypeCache.GetTypesDerivedFrom<T>();
            Vector2 mousePosition = viewTransform.matrix.inverse.MultiplyPoint(evt.localMousePosition);

            foreach(var type in types)
            {
                if(type.IsAbstract) continue;

                string typeName = Regex.Replace(typeof(T).Name, @"([a-z])([A-Z0-9])", "$1 $2");
                string concreteName = Regex.Replace(type.Name, @"([a-z])([A-Z0-9])", "$1 $2");

                evt.menu.AppendAction($"{typeName}/{concreteName}", (action) => CreateNode(type, mousePosition));
            }
        }

        private void OnUndoRedo()
        {
            PopulateView(tree);
            AssetDatabase.SaveAssets();
        }
    }
}
