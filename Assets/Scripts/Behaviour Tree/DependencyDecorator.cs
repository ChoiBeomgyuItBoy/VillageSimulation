using UnityEngine;

namespace ArtGallery.BehaviourTree
{
    public abstract class DependencyDecorator : DecoratorNode
    {   
        [SerializeField] BehaviourTree dependencyTree;

        public override Node Clone()
        {
            DependencyDecorator node = Instantiate(this);
            node.SetChild(GetChild().Clone());

            if(dependencyTree != null)
            {
                node.dependencyTree = dependencyTree.Clone();
            }

            return node;
        }

        public override void Bind(TreeController controller)
        {
            if(dependencyTree != null)
            {
                dependencyTree.Bind(controller);
            }

            base.Bind(controller);
        }

        protected BehaviourTree GetDependencyTree()
        {
            return dependencyTree;
        }
    }
}
