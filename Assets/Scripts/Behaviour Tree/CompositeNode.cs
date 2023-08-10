using System.Collections.Generic;
using UnityEngine;

namespace ArtGallery.BehaviourTree
{
    public abstract class CompositeNode : Node
    {
        [SerializeField] List<Node> children = new List<Node>();

        protected int GetChildCount()
        {
            return children.Count;
        }

        protected Node GetChild(int index)
        {
            return children[index];
        }
    }
}