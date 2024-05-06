using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace XRC.Assignments.Interaction
{
    /// <summary>
    /// Docking controller that handles snap placement and docking task.
    /// </summary>
    public class DockingController : MonoBehaviour
    {
        [Tooltip("The grab instractable that will perform the docking at this dock.")] [SerializeField]
        private GrabInteractable m_TargetInteractable;

        [Tooltip("The maximum distance for the snap placement to happen.")] [SerializeField]
        private float m_DistanceThreshold = 0.01f;

        [Tooltip("The maximum difference in rotation for the snap placement to happen.")] [SerializeField]
        private float m_AngleThreshold = 2f;

        [Tooltip("Whether this dock will check for the rotational alignment.")] [SerializeField]
        private bool m_CheckRotation;

        [Tooltip("Whether to randomly reposition and reorient this dock after the docking task is completed.")]
        [SerializeField]
        private bool m_MoveAfterDocked;

        [Tooltip("The rectangular range around the original position for the random docking generation to happen.")]
        [SerializeField]
        private Vector3 m_Bounds;
        
        [Tooltip("The particle effect.")]
        [SerializeField]
        private ParticleSystem m_Particle;

        private Vector3 m_StartPosition;
        private bool m_JustDocked;

        private void Start()
        {
            m_StartPosition = transform.position;
        }

        private void Update()
        {
            if (IsUnderThreshold(m_CheckRotation, m_TargetInteractable.transform, transform) && !m_JustDocked)
            {
                Dock();
                StartCoroutine(MoveDockCoroutine());
            }
        }

        /// <summary>
        /// Snaps the interactable to the docking position.
        /// </summary>
        private void Dock()
        {
            m_TargetInteractable.Release();
            m_TargetInteractable.transform.position = transform.position;
            m_TargetInteractable.transform.rotation = transform.rotation;
            m_JustDocked = true;
            // particle effect
            if (m_Particle)
            {
                m_Particle.Play();
            }
        }

        /// <summary>
        /// Moves the dock after a short delay.
        /// </summary>
        IEnumerator MoveDockCoroutine()
        {
            yield return new WaitForSeconds(1.5f);
            MoveDock();
        }

        /// <summary>
        /// Randomly reposition and reorient the dock within the user assigned bound around the original position.
        /// </summary>
        private void MoveDock()
        {
            if (m_MoveAfterDocked)
            {
                transform.position = m_StartPosition + new Vector3(
                    (Random.value - 0.5f) * m_Bounds.x,
                    (Random.value - 0.5f) * m_Bounds.y,
                    (Random.value - 0.5f) * m_Bounds.z
                );
                transform.rotation = Random.rotation;
            }
        }

        /// <summary>
        /// Check if the target interactable is within the docking threshold.
        /// </summary>
        /// <param name="checkRotation">Whether the rotation will be checked.</param>
        /// <param name="target">The transform of the interactable.</param>
        /// <param name="dock">The transform of this dock.</param>
        /// <returns>Returns whether the target is within threshold.</returns>
        private bool IsUnderThreshold(bool checkRotation, Transform target, Transform dock)
        {
            if (IsUnderDistanceThreshold(m_DistanceThreshold, target, dock) &&
                (!checkRotation || IsUnderRotationThreshold(m_AngleThreshold, target, dock)))
            {
                return true;
            }

            m_JustDocked = false;
            return false;
        }

        /// <summary>
        /// Check if the distance in between the target interactable and the dock is under the distance threshold.
        /// </summary>
        /// <remarks>
        /// After implemented, the snap placement will be working.
        /// </remarks>
        /// <param name="distanceThreshold">The distance threshold.</param>
        /// <param name="targetTransform">The transform of the target interactable that will be placed at this dock.</param>
        /// <param name="dockTransform">The transform of this dock.</param>
        /// <returns>Returns whether the distance is under the threshold.</returns>
        public bool IsUnderDistanceThreshold(float distanceThreshold, Transform targetTransform, Transform dockTransform)
        {
            bool result = false;
            
            // TODO - Implement method
            // <solution>
            // Your code here
            float distance = Vector3.Distance(targetTransform.position, dockTransform.position);
            if (distance < distanceThreshold)
            {
                result = true;
            }
            // </solution>
            
            return result;
        }
    
        /// <summary>
        /// Check if the angle in between the rotation of target interactable and that of the dock is under the rotation threshold.
        /// </summary>
        /// <remarks>
        /// After the IsUnderDistanceThreshold() and this method are both implemented, the docking task will be working.
        /// </remarks>
        /// <param name="angleThreshold">The angle threshold.</param>
        /// <param name="targetTransform">The transform of the target interactable that will be placed at this dock.</param>
        /// <param name="dockTransform">The transform of this dock.</param>
        /// <returns>Returns whether the distance is under the threshold.</returns>
        public bool IsUnderRotationThreshold(float angleThreshold, Transform targetTransform, Transform dockTransform)
        {
            bool result = false;
            
            // TODO - Implement method
            // <solution>
            // Your code here
            float angle = Quaternion.Angle(targetTransform.rotation, dockTransform.rotation);
            if (angle < angleThreshold)
            {
                result = true;
            }
            // </solution>
            
            return result;
        }
    }
}
