using System;
using UnityEngine;

namespace ArtGallery.Core
{
    public class Boredom : MonoBehaviour
    {   
        [SerializeField] [Range(0,200)] float initialBoredom = 50;
        [SerializeField] float maxBoredom = 200;
        const float boredomIncreaseRate = 2f;
        float boredom = 0;


        public float GetBoredom()
        {
            return boredom;
        }

        public float GetBoredomFraction()
        {
            return boredom / maxBoredom;
        }

        public void ChangeBoredom(float boredomChange)
        {
            boredom = Mathf.Max(0, boredom + boredomChange);
        }

        private void Start()
        {
            ChangeBoredom(initialBoredom);
        }

        private void Update()
        {
            if(boredom < maxBoredom)
            {
                boredom += Time.deltaTime * boredomIncreaseRate;

                if(boredom > maxBoredom)
                {
                    boredom = maxBoredom;
                }
            }
        }
    }
}