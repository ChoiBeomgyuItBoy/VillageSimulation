using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ArtGallery.BehaviourTree
{
    [CreateAssetMenu(menuName = "New Behaviour Tree")]
    public class BehaviourTree : ScriptableObject
    {
        [SerializeField] RootNode rootNode = null;
        [SerializeField] List<Node> nodes = new List<Node>();

        public Status Tick(TreeController controller)
        {
            return rootNode.Tick(controller);
        }

        public RootNode GetRoot()
        {
            return rootNode;
        }

        public void SetRoot(RootNode rootNode)
        {
            this.rootNode = rootNode;
        }

        public IEnumerable<Node> GetNodes()
        {
            return nodes;
        }

        public Node CreateNode(Type type)
        {
            Node node = ScriptableObject.CreateInstance(type) as Node;
            node.name = type.Name;
            node.SetUniqueID(GUID.Generate().ToString());
            nodes.Add(node);

            AssetDatabase.AddObjectToAsset(node, this);
            AssetDatabase.SaveAssets();

            return node;
        }

        public void DeleteNode(Node node)
        {
            nodes.Remove(node);
            AssetDatabase.RemoveObjectFromAsset(node);
            AssetDatabase.SaveAssets();
        }

        public void AddChild(Node parent, Node child)
        {
            RootNode root = parent as RootNode;

            if(root != null)
            {
                root.SetChild(child);
            }

            DecoratorNode decorator = parent as DecoratorNode;

            if(decorator != null)
            {
                decorator.SetChild(child);
            }

            CompositeNode composite = parent as CompositeNode;

            if(composite != null)
            {
                composite.AddChild(child);
            }
        }

        public void RemoveChild(Node parent, Node child)
        {
            RootNode root = parent as RootNode;

            if(root != null)
            {
                root.SetChild(null);
            }

            DecoratorNode decorator = parent as DecoratorNode;

            if(decorator != null)
            {
                decorator.SetChild(null);
            }

            CompositeNode composite = parent as CompositeNode;

            if(composite != null)
            {
                composite.RemoveChild(child);
            }
        }

        public IEnumerable<Node> GetChildren(Node parent)
        {
            RootNode root = parent as RootNode;

            if(root != null && root.GetChild() != null)
            {
                yield return root.GetChild();
            }

            DecoratorNode decorator = parent as DecoratorNode;

            if(decorator != null && decorator.GetChild() != null)
            {
                yield return decorator.GetChild();
            }

            CompositeNode composite = parent as CompositeNode;

            if(composite != null && composite.GetChildren() != null)
            {
                foreach(var node in composite.GetChildren())
                {
                    yield return node;
                }
            }
        }

        public BehaviourTree Clone()
        {
            BehaviourTree tree = Instantiate(this);
            tree.rootNode = tree.rootNode.Clone() as RootNode;
            return tree;
        }
    }
}
