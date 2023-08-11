using System;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace ArtGallery.BehaviourTree.Editor
{
    public class BehaviourTreeView : GraphView
    {
        BehaviourTree tree = null;

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

        public void PopulateView(BehaviourTree tree)
        {
            this.tree = tree;

            graphViewChanged -= OnGraphViewChanged;
            DeleteElements(graphElements);
            graphViewChanged += OnGraphViewChanged;

            foreach(var node in tree.GetNodes())
            {
                CreateNodeView(node);
            }
        }

        private GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange)
        {
            var elements = graphViewChange.elementsToRemove;

            if(elements != null)
            {
                foreach(var element in elements)
                {
                    NodeView nodeView = element as NodeView;
                    if(nodeView == null) continue;
                    tree.DeleteNode(nodeView.GetNode());
                }
            }

            return graphViewChange;
        }

        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            if(tree == null) return;

            AddToContextMenu<ActionNode>(evt);
            AddToContextMenu<DecoratorNode>(evt);
            AddToContextMenu<CompositeNode>(evt);
        }

        private void AddToContextMenu<T>(ContextualMenuPopulateEvent evt)
        {
            var types = TypeCache.GetTypesDerivedFrom<T>();

            foreach(var type in types)
            {
                if(type.IsAbstract) continue;
                evt.menu.AppendAction($"{type.Name} ({type.BaseType.Name})", (action) => CreateNode(type));
            }
        }

        private void CreateNode(Type type)
        {
            Node node = tree.CreateNode(type);
            CreateNodeView(node);
        }

        private void CreateNodeView(Node node)
        {
            NodeView nodeView = new NodeView(node);
            AddElement(nodeView);
        }
    }
}
