using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace UndeadWarfare.AI
{
    // Enumerator for potential AI states
    public enum EnemyState
    {
        Idle,
        Seek,
        Attack,
        Flank,
        Flee,
        Dead
    }

    public class BaseAI : MonoBehaviour
    {
        // -----Public Properties----
        // Getter & Setters for proximatity to target and speed of orientation to target
        public float ProximityDistance { get => proximityDistance; set => proximityDistance = value; }
        public int RotationSpeedTowardsTarget { get => rotationSpeedTowardsTarget; set => rotationSpeedTowardsTarget = value; }
        // Public getter of NavMeshAgent
        public NavMeshAgent NavAgent { get => navAgent; }
        public bool IsTargetVisible { get => isTargetVisible; set => IsTargetVisible = value; }

        [Header("---Targeting and Movement---")]
        public GameObject Target;
        [SerializeField] private int rotationSpeedTowardsTarget;
        [SerializeField] protected NavMeshAgent navAgent;

        [Header("---Detection Settings---")]
        public int targetDetectionRange;
        [SerializeField] protected float proximityDistance;     // Distance for proximtiy check
        [SerializeField, Range(-1f, 1f)] private float minAngleToDetection;

        [Header("---State and Direction Tracking---")]
        public Vector3 targetDirection;     // Direction vector to the target
        public Vector3 targetDirectionNormalized;       // Normalized direction to the target
        public float targetFacingAlignment;     // Alignment value indicating facing direction towards the target
        protected Vector3 desiredMovePosition;      // Desired positon to move towards
        protected float StoppingDistanceOriginal;   // Orignal stopping distance for NavMeshAgent

        private bool isTargetVisible;
        private bool isNearTarget;
        private float lastProximityCheckTimestamp;      // Stores the last time proximity was checked

        private void Start()
        {
            // NOOP
        }

        // ---- Target Detection and Visiblity Methods ----
        
        // Calculate and updates direction to the target; returns this direction 
        public Vector3 CalculateTargetDirection()
        {
            targetDirection = Target.transform.position - transform.position;       // Vector from AI to target
            return targetDirection;
        }
        // Updates and returns normalized direction to target (Vector3)
        public Vector3 CalculateNormalizedTargetDirection()
        {
            targetDirectionNormalized = CalculateTargetDirection().normalized;      // Normalize target direction 
            return targetDirectionNormalized;
        }
        // Updates and returns alignment with target based on AI's facing direction
        public float CalculateTargetFacingAligment()
        {
            targetFacingAlignment = Vector3.Dot(transform.forward, CalculateNormalizedTargetDirection()); // Dot product for alignment
            return targetFacingAlignment;
        }
        // Check if the target is visible based on line of sight and angle requirements
        public void CheckTargetVisiblityy()
        {
            if (Physics.Raycast(transform.position, CalculateTargetDirection(), out RaycastHit visibilityHit, targetDetectionRange))
            {
                Debug.DrawRay(transform.position, targetDirection, Color.green);        // Debug Ray
                isTargetVisible = visibilityHit.collider.CompareTag("Player") && CalculateTargetFacingAligment() > minAngleToDetection;
            }
            else
                isTargetVisible = false;        // No visiblilty if the raycast did not hit the target
        }
        // Check if taget is within proximity distance, with a delay interval to limit checks
        public void CheckTargetProximity(float delayBetweenChecks = 0.1f)
        {
            if (Time.time - lastProximityCheckTimestamp > delayBetweenChecks)
            {
                isNearTarget = CalculateDistanceToTarget() <= proximityDistance;        // Check proximity distance to target
                lastProximityCheckTimestamp = Time.time;        // Update last time checked
            }
        }
        // Checks if a specific spot has an ubstucted view to the target
        public bool IsTargetVisibleFrom(Vector3 observationSpot)
        {
            Vector3 directionToTarget = Target.transform.position - observationSpot;        // Direction to Target
            float distanceToTarget = directionToTarget.magnitude;       // Distance to Target
            Debug.DrawRay(observationSpot, directionToTarget, Color.blue);      // Visualize Ray
            if (Physics.Raycast(Target.transform.position, directionToTarget, out RaycastHit hit, distanceToTarget))
                return hit.collider.CompareTag("Player");       // True if Player 

            return true;        // Return true if no obstacles found
        }
       
        // ---- Movement and Positioning Methods ----
        
        // Smoothly rotates the AI to face the target
        public void RotateTowardsTarget()
        {
            Quaternion targetRotoation = Quaternion.LookRotation(targetDirection);      // Target rotation towards direction of target 
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotoation, Time.deltaTime * rotationSpeedTowardsTarget);     // Smoothen transition
        }
        // Calculates and returns the distance to the target    
        public float CalculateDistanceToTarget()
        {
            return Vector3.Distance(transform.position, Target.transform.position);     // Distance to target
        }
        // Move the AI to the designated move position
        public void MoveToPosition()
        {
            if (!navAgent.enabled) return;      // Check if the NavMeshAgent is enabled
            navAgent.stoppingDistance = StoppingDistanceOriginal;       // Set the stopping distance 
            navAgent.SetDestination(desiredMovePosition);       // Move to the desire 
        }
    }
}