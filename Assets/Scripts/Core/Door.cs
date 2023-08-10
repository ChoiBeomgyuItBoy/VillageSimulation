using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ArtGallery.Core
{
    public class Door : MonoBehaviour
    {
        [SerializeField] bool isLocked = false;
        static Dictionary<string, Door> doorLookup = null;

        public static Door GetWithName(string name)
        {
            if(doorLookup == null)
            {
                doorLookup = new Dictionary<string, Door>();

                foreach(var door in FindObjectsOfType<Door>())
                {
                    doorLookup[door.name] = door;
                }
            }

            return doorLookup[name];
        }

        public bool IsLocked()
        {
            return isLocked;
        }

        public void Open()
        {
            GetComponent<NavMeshObstacle>().enabled = false;
        }
    }
}