using System.Collections.Generic;
using UnityEngine;

namespace ArtGallery.BehaviourTree
{
    public abstract class CompositeNode : Node
    {
        [SerializeField] List<Node> children = new List<Node>();

        public IEnumerable<Node> GetChildren()
        {
            return children;
        }

        public void AddChild(Node child)
        {
            children.Add(child);
        }

        public void RemoveChild(Node child)
        {
            children.Remove(child);
        }

        public void SortChildrenByHorizontalPosition()
        {
            children.Sort(ComparePositions);
        }

        protected Node GetChild(int index)
        {
            return children[index];
        }

        protected void SortChildrenByPriority()
        {
            children.Sort(ComparePriorities);
        }

        protected void ShuffleChildren()
        {
            Shuffle();
        }

        private int ComparePositions(Node left, Node right)
        {
            return left.GetPosition().x < right.GetPosition().x ? -1 : 1;
        }

        private int ComparePriorities(Node x, Node y)
        {
            return x.GetPriority() < y.GetPriority() ? 1 : -1;
        }

        private void Shuffle()
        {
            int current = children.Count;

            while(current > 1)
            {
                current--;
                int randomIndex = new System.Random().Next(current + 1);
                Node randomNode = children[randomIndex];
                children[randomIndex] = children[current];
                children[current] = randomNode;
            }
        }
    }
}