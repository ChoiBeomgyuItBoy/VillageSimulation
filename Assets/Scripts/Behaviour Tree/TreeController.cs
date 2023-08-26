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

        void Start()
        {
            tree = tree.Clone();
        }

        void Update()
        {
            tree.Tick(this);
        }

    }
}