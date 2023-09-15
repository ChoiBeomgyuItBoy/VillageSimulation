using UnityEngine;

namespace ArtGallery.BehaviourTree
{
    public class TreeController : MonoBehaviour
    {
        [SerializeField] BehaviourTree tree = null;

        public BehaviourTree GetTree()
        {
            return tree;
        }

        void Start()
        {
            tree = tree.Clone();
            tree.Bind(this);
        }

        void Update()
        {
            tree.Tick();
        }
    }
}