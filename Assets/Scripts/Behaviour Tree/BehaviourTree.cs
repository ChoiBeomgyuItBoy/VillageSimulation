using UnityEngine;

namespace ArtGallery.BehaviourTree
{
    [CreateAssetMenu(menuName = "Behaviour Tree/ New Behaviour Tree")]
    public class BehaviourTree : ScriptableObject
    {
        [SerializeField] Node rootNode = null;

        public Status Tick(TreeController controller)
        {
            return rootNode.Tick(controller);
        }
    }
}
