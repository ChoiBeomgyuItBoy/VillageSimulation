using UnityEngine;

namespace ArtGallery.BehaviourTree
{
    public abstract class Node : ScriptableObject
    {
        [SerializeField] int priority = 0;
        protected TreeController controller;
        Status status = Status.Running;
        bool started = false;

        public int GetPriority()
        {
            return priority;
        }

        public Status Tick(TreeController controller)
        {
            if(this.controller == null)
            {
                this.controller = controller;
            }

            if(!started)
            {
                OnEnter();
                started = true;
            }

            status = OnTick();

            if(status == Status.Success || status == Status.Failure)
            {
                OnExit();
                started = false;
            }

            return status;
        }

        protected abstract void OnEnter();
        protected abstract Status OnTick();
        protected abstract void OnExit();
    }
}