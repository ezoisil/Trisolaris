using Trisolaris.Core;
using UnityEngine;
using UnityEngine.AI;
using Trisolaris.Saving;

namespace Trisolaris.Movement
{
    public class Mover : MonoBehaviour, IAction, ISaveable
    {
        [SerializeField] float maxSpeed = 6f;
        [SerializeField] float maxNavPathLength = 40;


        private NavMeshAgent navMeshAgent;
        private Animator animator;

        private void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
        }
        void Update()
        {
            UpdateAnimator();
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float forwardSpeed = localVelocity.z;
            animator.SetFloat("forwardSpeed", forwardSpeed);
        }

        public void StartMoveAction(Vector3 destination, float speedFraction)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination, speedFraction);
        }
        
        public bool CanMoveTo(Vector3 target)
        {
            // If target position is too far away, or the path calculated is a partial path, the path 
            //will be discarded.

            NavMeshPath path = new NavMeshPath();
            bool hasPath = NavMesh.CalculatePath(transform.position, target, NavMesh.AllAreas, path);

            if (!hasPath) return false;
            if (path.status != NavMeshPathStatus.PathComplete) return false;
            if (GetPathLength(path) > maxNavPathLength) return false;

            return true;

        }
        public void MoveTo(Vector3 destination, float speedFraction)
        {
            navMeshAgent.destination = destination;
            navMeshAgent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
            navMeshAgent.isStopped = false;
        }

        public void Cancel()
        {
            navMeshAgent.isStopped = true;
        }

        private float GetPathLength(NavMeshPath path)
        {
            Vector3[] corners = path.corners;
            float pathLength = 0;

            if (path.corners.Length < 2) return pathLength;

            for (int i = 0; i < corners.Length - 1; i++)
            {
                pathLength += Vector3.Distance(corners[i], corners[i + 1]);
            }

            return pathLength;
        }

        [System.Serializable]
        struct MoverSaveData
        {
            public SerializableVector3 position;
            public SerializableVector3 rotation;
        }
        
        public object CaptureState()
        {
            MoverSaveData data = new MoverSaveData();
            data.position = new SerializableVector3(transform.position);
            data.rotation = new SerializableVector3(transform.eulerAngles);
            return data;
        }

        public void RestoreState(object state)
        {
            GetComponent<NavMeshAgent>().enabled = false;
            MoverSaveData data = (MoverSaveData)state;
            transform.position = data.position.ToVector();
            transform.eulerAngles = data.rotation.ToVector();
            GetComponent<NavMeshAgent>().enabled = true;
        }
    }
}
