using ArtGallery.Core;
using UnityEngine;

namespace ArtGallery.BehaviourTree.Conditions
{
    public class IsTime : ActionNode
    {
        [SerializeField] [Range(0,24)] float time = 5;
        Clock clock;

        protected override void OnEnter()
        {
            if(clock == null)
            {
                clock = FindObjectOfType<Clock>();
            }
        }

        protected override Status OnTick()
        {
            if(clock.GetCurrentTime() == time)
            {
                return Status.Success;
            }

            return Status.Failure;
        }

        protected override void OnExit() { }
    }
}