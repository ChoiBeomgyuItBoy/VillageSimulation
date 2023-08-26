using UnityEngine;

namespace ArtGallery.BehaviourTree
{
    public abstract class Node : ScriptableObject
    {
        [SerializeField] int priority = 0;
        [SerializeField] string uniqueID = "";
        [SerializeField] Vector2 position = Vector2.zero;
        Status status = Status.Running;
        bool started = false;

        [TextArea] public string description  = "";

        protected TreeController controller;

        public virtual Node Clone()
        {
            return Instantiate(this);
        }

        public Vector2 GetPosition()
        {
            return position;
        }

        public string GetUniqueID()
        {
            return uniqueID;
        }

        public int GetPriority()
        {
            return priority;
        }

        public Status GetStatus()
        {
            return status;
        }

        public bool Started()
        {
            return started;
        }

        public void SetPosition(Vector2 position)
        {
            this.position = position;
        }
        
        public void SetUniqueID(string uniqueID)
        {
            this.uniqueID = uniqueID;
        }

        public void SetPriority(int priority)
        {
            this.priority = priority;
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