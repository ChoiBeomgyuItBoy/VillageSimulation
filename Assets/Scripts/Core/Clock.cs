using System;
using UnityEngine;

namespace ArtGallery.Core
{
    public class Clock : MonoBehaviour
    {
        [SerializeField] float timeScale = 1;
        [SerializeField] float timeSpeed = 1;
        [SerializeField] [Range(0,24)] float initalTime = 6;
        float currentTime = 0;

        public float GetCurrentTime()
        {
            return currentTime;
        }

        public float GetTimeScale()
        {
            return timeScale;
        }

        void Start()
        {
            Time.timeScale = timeScale;
            currentTime = initalTime;
        }

        void Update()
        {
            currentTime += Time.deltaTime * timeSpeed;

            if(currentTime >= 24)
            {
                currentTime = 0;
            }
        }
    }
}
