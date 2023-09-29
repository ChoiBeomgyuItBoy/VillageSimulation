using UnityEngine;
using System.Collections.Generic;

namespace ArtGallery.Villagers
{
    public class Villager : MonoBehaviour
    {
        [SerializeField] LocationInfo[] locations;
        [SerializeField] Occupation occupation;
        [SerializeField] Transform rightHandTransform;
        [SerializeField] Transform leftHandTransform;
        [SerializeField] Transform spawnerTranform;
        Dictionary<Location, Dictionary<int, Vector3>> locationLookup;

        [System.Serializable]
        class LocationInfo
        {
            public Transform[] possibleDestinations;
            public Location locationName;
        }

        public enum EquipType
        {
            RightHand,
            LeftHand,
            InSpawner
        }

        public Vector3 GetLocation(Location locationName, int locationIndex)
        {
            if(locationLookup == null)
            {
                BuildLookup();
            }

            return locationLookup[locationName][locationIndex];
        }

        public Vector3 GetRandomLocation(Location locationName)
        {
            if(locationLookup == null)
            {
                BuildLookup();
            }

            var innerLookup = locationLookup[locationName];
            int randomIndex = Random.Range(0, innerLookup.Count);

            return innerLookup[randomIndex];
        }

        public Occupation GetOccupation()
        {
            return occupation;
        }

        public void Equip(GameObject prefab, EquipType equipType)
        {
            switch(equipType)
            {
                case EquipType.RightHand:
                    Instantiate(prefab, rightHandTransform);
                    break;
                case EquipType.LeftHand:
                    Instantiate(prefab, leftHandTransform);
                    break;
                case EquipType.InSpawner:
                    Instantiate(prefab, spawnerTranform);
                    break;
            }
        }

        public void Unequip(GameObject prefab)
        {
            Transform oldObject = rightHandTransform.Find(prefab.name + "(Clone)");

            if(oldObject == null)
            {
                oldObject = leftHandTransform.Find(prefab.name + "(Clone)");
            }

            if(oldObject == null) 
            {
                oldObject = spawnerTranform.Find(prefab.name + "(Clone)");
            }

            if(oldObject == null) return;

            Destroy(oldObject.gameObject);
        }

        private void BuildLookup()
        {
            locationLookup = new Dictionary<Location, Dictionary<int, Vector3>>();

            foreach(var location in locations)
            {
                var innerLookup = new Dictionary<int, Vector3>();

                for(int i = 0; i < location.possibleDestinations.Length; i++)
                {
                    innerLookup[i] = location.possibleDestinations[i].position;
                }   

                locationLookup[location.locationName] = innerLookup;
            }
        }
    }
}