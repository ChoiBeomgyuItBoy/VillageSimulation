using ArtGallery.Villagers;
using UnityEngine;

namespace ArtGallery.BehaviourTree.Actions
{
    public class PlayMusic : ActionNode
    {
        [SerializeField] int musicIndex = 0;
        [SerializeField] bool playRandom = false;
        MusicPlayer musicPlayer;

        protected override void OnEnter()
        {
            musicPlayer = controller.GetComponent<MusicPlayer>();

            if(playRandom)
            {
                musicPlayer.PlayRandom();
            }
            else
            {
                musicPlayer.PlayAtIndex(musicIndex);
            }
        }

        protected override Status OnTick()
        {
            if(!musicPlayer.IsActive())
            {
                return Status.Success;
            }

            return Status.Running;
        }

        protected override void OnExit() { }
    }
}
