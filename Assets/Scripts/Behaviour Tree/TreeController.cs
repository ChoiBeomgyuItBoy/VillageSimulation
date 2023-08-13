using UnityEngine;

namespace ArtGallery.BehaviourTree
{
    public class TreeController : MonoBehaviour
    {
        [SerializeField] BehaviourTree tree = null;

        public BehaviourTree GetBehaviourTree()
        {
            return tree;
        }

        void Update()
        {
            tree.Tick(this);
        }

    }
}