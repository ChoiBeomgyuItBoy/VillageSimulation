using ArtGallery.Villagers;

namespace ArtGallery.BehaviourTree.Actions
{
    public class StopMusic : ActionNode
    {
        protected override void OnEnter()
        {
            MusicPlayer musicPlayer = controller.GetComponent<MusicPlayer>();
            musicPlayer.Stop();
        }

        protected override Status OnTick()
        {
            return Status.Success;
        }

        protected override void OnExit() { }
    }
}
