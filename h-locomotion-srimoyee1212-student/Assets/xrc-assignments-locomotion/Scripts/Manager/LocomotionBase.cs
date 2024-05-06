using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.OpenXR.Input;

namespace XRC.Assignments.Locomotion
{
    /// <summary>
    /// Locomotion controller that is the base of all techniques. 
    /// </summary>
    public class LocomotionBase : MonoBehaviour
    {
        protected InputActionReference m_ActionReference = null;
        protected LocomotionManager m_Manager;
        protected GameObject m_PlayerRig;
        protected Camera m_PlayerCamera;

        protected virtual void OnEnable()
        {
            if (m_ActionReference == null || m_ActionReference.action == null)
                return;

            m_ActionReference.action.started += OnActionStarted;
            m_ActionReference.action.performed += OnActionPerformed;
            m_ActionReference.action.canceled += OnActionCanceled;
            //StartCoroutine(UpdateBinding());
        }

        protected virtual void OnDisable()
        {
            if (m_ActionReference == null || m_ActionReference.action == null)
                return;

            m_ActionReference.action.started -= OnActionStarted;
            m_ActionReference.action.performed -= OnActionPerformed;
            m_ActionReference.action.canceled -= OnActionCanceled;
        }

        protected virtual void Start()
        {
            m_Manager = GetComponent<LocomotionManager>();
            m_PlayerRig = m_Manager.playerRig;
            m_PlayerCamera = Camera.main;
        }

        private IEnumerator UpdateBinding()
        {
            while (isActiveAndEnabled)
            {
                if (m_ActionReference.action != null &&
                    m_ActionReference.action.controls.Count > 0 &&
                    m_ActionReference.action.controls[0].device != null &&
                    OpenXRInput.TryGetInputSourceName(m_ActionReference.action, 0, out var actionName,
                        OpenXRInput.InputSourceNameFlags.Component, m_ActionReference.action.controls[0].device))
                {
                    OnActionBound();
                    break;
                }

                yield return new WaitForSeconds(1.0f);
            }
        }

        protected virtual void OnActionStarted(InputAction.CallbackContext ctx) { }
        protected virtual void OnActionPerformed(InputAction.CallbackContext ctx) { }
        protected virtual void OnActionCanceled(InputAction.CallbackContext ctx) { }
        protected virtual void OnActionBound() { }

        /// <summary>
        /// Rotate the world transform of the player rig,
        /// from <paramref name="startingTransform"/>, around <paramref name="centerPosition"/>
        /// and <paramref name="axis"/> by <paramref name="angleDegrees"/>.
        /// The input <paramref name="axis"/> and sign of <paramref name="angleDegrees"/> follows Unity's "left hand rule".
        /// </summary>
        /// <remarks>
        /// This method is called in ContinuousTurnController, SnapTurnController, and TeleportationController.
        /// After implemented, the Continuous and Snap Turn techniques will both be working.
        /// Teleportation's rotation will be partially working.
        /// Hint: Quaternion.AngleAxis() converts angle and axis to rotation.
        /// </remarks>
        /// <param name="startingTransform">The world transform of the player rig before rotation.</param>
        /// <param name="centerPosition">The center of the rotation. Is equal to player camera position. </param>
        /// <param name="axis">The axis of the rotation. Is equal to Vector3.up. </param>
        /// <param name="angleDegrees">The signed angle degrees to rotate.</param>
        /// <returns>Returns the world transform of the player rig after rotation.</returns>
        public Matrix4x4 RotatedTransform(Matrix4x4 startingTransform, Vector3 centerPosition, Vector3 axis,
            float angleDegrees)
        {
            Matrix4x4 rotatedTransform = new Matrix4x4();
            rotatedTransform = Matrix4x4.identity;
            
            // TODO - Implement method according to summary
            // <solution>
            // Your code here
            Vector3 startingPosition = startingTransform.GetColumn(3);
            Quaternion startingRotation = Quaternion.LookRotation(
                startingTransform.GetColumn(2), startingTransform.GetColumn(1));
            
            Vector3 positionOffset = startingPosition - centerPosition;
            
            Quaternion rotation = Quaternion.AngleAxis(angleDegrees, axis);
            
            positionOffset = rotation * positionOffset;
            
            Vector3 newPosition = centerPosition + positionOffset;
            
            Quaternion newRotation = rotation * startingRotation;
            
            rotatedTransform = Matrix4x4.TRS(newPosition, newRotation, Vector3.one);
            // </solution>
            
            return rotatedTransform;
        }
    }
}