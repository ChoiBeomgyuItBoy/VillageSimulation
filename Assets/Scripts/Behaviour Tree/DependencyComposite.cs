using UnityEngine;

namespace ArtGallery.BehaviourTree
{
    public abstract class DependencyComposite : CompositeNode
    {
        [SerializeField] BehaviourTree dependencyTree;

        public override Node Clone()
        {
            DependencyComposite node = Instantiate(this);
            
            if(dependencyTree != null)
            {
                node.dependencyTree = dependencyTree.Clone();
            }
            
            node.SetChidren(GetChildren().ConvertAll((children) => children.Clone()));

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
