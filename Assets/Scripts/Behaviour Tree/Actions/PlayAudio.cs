using UnityEngine;

namespace ArtGallery.BehaviourTree.Actions
{
    public class PlayAudio : ActionNode
    {
        [SerializeField] AudioConfig audioConfig;
        [SerializeField] bool stop;
        AudioSource audioSource;

        [System.Serializable]
        class AudioConfig
        {
            public AudioClip clip;
            [Range(0,1)] public float volume;
            public bool loop;
        }

        protected override void OnEnter()
        {
            audioSource = controller.GetComponent<AudioSource>();

            if(stop)
            {
                Stop();
            }
            else
            {
                Play();
            }
        }

        protected override Status OnTick()
        {
            if(stop)
            {
                return Status.Success;
            }
            else
            {
                if(!audioSource.isPlaying)
                {
                    return Status.Success;
                }
            }

            return Status.Running;
        }

        protected override void OnExit() { }

        void Stop()
        {
            audioSource.clip = null;
            audioSource.Stop();
        }

        void Play()
        {
            audioSource.clip = audioConfig.clip;
            audioSource.volume = audioConfig.volume;
            audioSource.loop = audioConfig.loop;
            audioSource.Play();
        }
    }
}
