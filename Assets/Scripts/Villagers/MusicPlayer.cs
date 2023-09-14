using System.Collections;
using UnityEngine;

namespace ArtGallery.Villagers
{
    [RequireComponent(typeof(AudioSource))]
    public class MusicPlayer : MonoBehaviour
    {
        [SerializeField] MusicConfig[] music;
        [SerializeField] float secondsToReduceBoredom = 1;
        AudioSource audioSource;
        Boredom boredom;
        MusicConfig currentConfig;

        [System.Serializable]
        class MusicConfig
        {
            public AudioClip clip;
            [Range(0,1)] public float volume;
            public bool loop;
            public float boredomReduceRate = 5;
        }

        public void PlayAtIndex(int configIndex)
        {
            Play(music[configIndex]);
        }

        public void PlayRandom()
        {
            Play(GetRandomConfig());
        }

        public void Stop()
        {
            currentConfig = null;
            audioSource.clip = null;
            audioSource.Stop();
            StopCoroutine(ReduceBoredom());
        }

        public bool IsActive()
        {
            return audioSource.clip != null && audioSource.isPlaying;
        }

        void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            boredom = GetComponent<Boredom>();
        }

        void Play(MusicConfig audioConfig)
        {
            currentConfig = audioConfig;
            audioSource.clip = currentConfig.clip;
            audioSource.volume = currentConfig.volume;
            audioSource.loop = currentConfig.loop;
            audioSource.Play();
            StartCoroutine(ReduceBoredom());
        }

        IEnumerator ReduceBoredom()
        {
            while(IsActive())
            {
                boredom.ChangeBoredom(-currentConfig.boredomReduceRate);
                yield return new WaitForSeconds(secondsToReduceBoredom);
            }
        }

        MusicConfig GetRandomConfig()
        {
            return music[Random.Range(0, music.Length)];
        }
    }
}