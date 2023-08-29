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

        public bool Open()
        {
            if(isLocked) 
            {
                return false;
            }
            else
            {
                GetComponent<NavMeshObstacle>().enabled = false;
                return true;
            }
        }
    }
}