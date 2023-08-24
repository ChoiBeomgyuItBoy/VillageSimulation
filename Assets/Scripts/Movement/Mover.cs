using System;
using UnityEngine;
using UnityEngine.AI;

namespace ArtGallery.Movement
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] float maxSpeed = 6;
        NavMeshAgent agent = null;
        Animator animator = null;

        const float destinationTollerance = 5;

        public void MoveTo(Vector3 destination, float speedFraction, bool isPlayer = false)
        {
            agent.destination = isPlayer? transform.position + destination : destination;
            agent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
        }

        public bool AtDestination(Vector3 destination)
        {
            return Vector3.Distance(destination, transform.position) < destinationTollerance;
        }

        public bool CanGoTo(Vector3 destination)
        {
            return Vector3.Distance(agent.pathEndPosition, destination) < destinationTollerance;
        }

        void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            animator.SetFloat("movementSpeed", GetLocalVelocity(), 0.1f, Time.deltaTime);
        }

        float GetLocalVelocity()
        {
            return transform.InverseTransformDirection(agent.velocity).magnitude;
        }
    }
}