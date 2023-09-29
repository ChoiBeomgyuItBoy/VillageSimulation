using System.Collections.Generic;
using UnityEngine;

namespace ArtGallery.BehaviourTree
{
    public abstract class CompositeNode : Node
    {
        [SerializeField] List<Node> children = new List<Node>();

        public List<Node> GetChildren()
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

        public override Node Clone()
        {
            CompositeNode node = Instantiate(this);
            node.children = children.ConvertAll((children) => children.Clone());
            return node;
        }

        protected Node GetChild(int index)
        {
            return children[index];
        }

        protected void SetChidren(IEnumerable<Node> children)
        {
            this.children = new List<Node>(children);
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

        private int ComparePriorities(Node left, Node right)
        {
            return left.GetPriority() > right.GetPriority() ? -1 : 1;
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