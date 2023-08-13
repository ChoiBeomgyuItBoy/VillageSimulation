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

#if UNITY_EDITOR
        public Node CreateNode(Type type)
        {
            Node node = ScriptableObject.CreateInstance(type) as Node;
            node.name = type.Name;
            node.SetUniqueID(GUID.Generate().ToString());

            Undo.RecordObject(this, "(Behaviour Tree) Created node");
            nodes.Add(node);

            if(!Application.isPlaying)
            {
                AssetDatabase.AddObjectToAsset(node, this);
            }
 
            Undo.RegisterCreatedObjectUndo(node, "(Behaviour Tree) Created node");
            AssetDatabase.SaveAssets();

            return node;
        }

        public void DeleteNode(Node node)
        {
            Undo.RecordObject(this, "(Behaviour Tree) Created node");
            nodes.Remove(node);
            Undo.DestroyObjectImmediate(node);
            AssetDatabase.SaveAssets();
        }

        public void AddChild(Node parent, Node child)
        {
            RootNode root = parent as RootNode;

            if(root != null)
            {
                Undo.RecordObject(root, "(Behaviour Tree) Added child");
                root.SetChild(child);
                EditorUtility.SetDirty(root);
            }

            DecoratorNode decorator = parent as DecoratorNode;

            if(decorator != null)
            {
                Undo.RecordObject(decorator, "(Behaviour Tree) Added child");
                decorator.SetChild(child);
                EditorUtility.SetDirty(decorator);
            }

            CompositeNode composite = parent as CompositeNode;

            if(composite != null)
            {
                Undo.RecordObject(composite, "(Behaviour Tree) Added child");
                composite.AddChild(child);
                EditorUtility.SetDirty(composite);
            }
        }

        public void RemoveChild(Node parent, Node child)
        {
            RootNode root = parent as RootNode;

            if(root != null)
            {
                Undo.RecordObject(root, "(Behaviour Tree) Removed child");
                root.SetChild(null);
                EditorUtility.SetDirty(root);
            }

            DecoratorNode decorator = parent as DecoratorNode;

            if(decorator != null)
            {
                Undo.RecordObject(decorator, "(Behaviour Tree) Removed child");
                decorator.SetChild(null);
                EditorUtility.SetDirty(decorator);
            }

            CompositeNode composite = parent as CompositeNode;

            if(composite != null)
            {
                Undo.RecordObject(composite, "(Behaviour Tree) Removed child");
                composite.RemoveChild(child);
                EditorUtility.SetDirty(composite);
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
#endif
    }
}
