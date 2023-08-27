using ArtGallery.Core;
using UnityEngine;

namespace ArtGallery.BehaviourTree.Conditions
{
    public class IsTime : ActionNode
    {
        [SerializeField] [Range(0,24)] float initialTime = 0;
        [SerializeField] [Range(0,24)] float endTime = 24;
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
            if(clock.GetCurrentTime() >= initialTime && clock.GetCurrentTime() <= endTime)
            {
                return Status.Success;
            }

            return Status.Failure;
        }

        protected override void OnExit() { }
    }
}