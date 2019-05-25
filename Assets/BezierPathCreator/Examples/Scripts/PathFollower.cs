using UnityEngine;

namespace PathCreation.Examples
{
    // Moves along a path at constant speed.
    // Depending on the end of path instruction, will either loop, reverse, or stop at the end of the path.
    public class PathFollower : MonoBehaviour
    {
        // slightly modified code
        [HideInInspector]
        public PathCreator pathCreator;
        public EndOfPathInstruction endOfPathInstruction;
        [HideInInspector]
        public float speed = 0; // speed gets set by enemy in the start function
        [HideInInspector]
        public float distanceTravelled;

        void Update()
        {
            if (pathCreator != null)
            {
                distanceTravelled += speed * Time.deltaTime;
                transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction) + Vector3.up * 0.5f;
                transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
            }
            else
            {
                pathCreator = GameObject.FindGameObjectWithTag("Path").GetComponent<PathCreator>();
            }
        }
    }
}