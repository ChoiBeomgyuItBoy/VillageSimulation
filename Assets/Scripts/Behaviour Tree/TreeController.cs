using UnityEngine;

namespace ArtGallery.BehaviourTree
{
    public class TreeController : MonoBehaviour
    {
        [SerializeField] BehaviourTree[] trees = null;

        public BehaviourTree GetBehaviourTree()
        {
            return trees[0];
        }

        void Update()
        {
            foreach(var tree in trees)
            {
                tree.Tick(this);
            }
        }

    }
}