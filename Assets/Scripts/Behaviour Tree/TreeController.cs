using UnityEngine;

namespace ArtGallery.BehaviourTree
{
    public abstract class TreeController : MonoBehaviour
    {
        [SerializeField] BehaviourTree tree = null;
        Status treeStatus = Status.Running;

        void Update()
        {
            treeStatus = tree.Tick(this);
        }
    }
}