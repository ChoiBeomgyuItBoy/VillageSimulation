using UnityEngine;

namespace ArtGallery.BehaviourTree
{
    public class TreeController : MonoBehaviour
    {
        [SerializeField] BehaviourTree tree = null;

        void Update()
        {
            tree.Tick(this);
        }

    }
}