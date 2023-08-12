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

        public override Node Clone()
        {
            CompositeNode node = Instantiate(this);
            node.children = children.ConvertAll(child => child.Clone());
            return node;
        }

        protected Node GetChild(int index)
        {
            return children[index];
        }

        protected void SortChildren()
        {
            Sort(0, children.Count - 1);
        }

        protected void ShuffleChildren()
        {
            Shuffle();
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

        private void Sort(int low, int high)
        {
            if (low < high)
            {
                int partitionIndex = Partition(low, high);
                Sort(low, partitionIndex - 1);
                Sort(partitionIndex + 1, high);
            }
        }

        private int Partition(int low, int high)
        {
            Node pivot = children[high];

            int lowIndex = (low - 1);

            for (int j = low; j < high; j++)
            {
                if (children[j].GetPriority() <= pivot.GetPriority())
                {
                    lowIndex++;
                    Switch(lowIndex, j);
                }
            }

            Switch(lowIndex, high);

            return lowIndex + 1;
        }

        private void Switch(int indexA, int indexB)
        {
            Node cache = children[indexA + 1];
            children[indexA + 1] = children[indexB];
            children[indexB] = cache;
        }
    }
}