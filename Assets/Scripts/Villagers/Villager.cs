using UnityEngine;
using System.Collections.Generic;

namespace ArtGallery.Villagers
{
    public class Villager : MonoBehaviour
    {
        [SerializeField] LocationInfo[] locations;
        [SerializeField] Occupation occupation;
        Dictionary<Location, Vector3> locationLookup;

        [System.Serializable]
        class LocationInfo
        {
            public Transform destination;
            public Location locationName;
        }

        public Vector3 GetLocation(Location locationName)
        {
            if(locationLookup == null)
            {
                locationLookup = new Dictionary<Location, Vector3>();

                foreach(var location in locations)
                {
                    locationLookup[location.locationName] = location.destination.position;
                }
            }

            return locationLookup[locationName];
        }

        public Occupation GetOccupation()
        {
            return occupation;
        }
    }
}