using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ArtGallery.BehaviourTree
{
    [CreateAssetMenu(menuName = "Behaviour Tree/ New Behaviour Tree")]
    public class BehaviourTree : ScriptableObject
    {
        [SerializeField] Node rootNode = null;
        List<Node> nodes = new List<Node>();

        public Status Tick(TreeController controller)
        {
            return rootNode.Tick(controller);
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
    }
}
