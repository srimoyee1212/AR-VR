using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.InputSystem;

namespace XRC.Assignments.Interaction
{
    /// <summary>
    /// The interactor that supports near interaction.
    /// </summary>
    public class NearInteractor : BaseInteractor
    {
        private List<GrabInteractable> m_CollidedGrabbables = new List<GrabInteractable>();
        private Vector3 m_MinColliderScale = new Vector3(0.05f, 0.05f, 0.05f);
        private Vector3 m_MaxColliderScale = new Vector3(0.5f, 0.5f, 0.5f);
        private float m_ScaleSpeed = 2f;
        private bool m_IsGrabbing;

        private void OnTriggerEnter(Collider other)
        {
            GrabInteractable grabbable = other.GetComponent<GrabInteractable>();
            if (grabbable && m_CollidedGrabbables.IndexOf(grabbable) == -1 && !grabbable.isGrabbed)
            {
                m_CollidedGrabbables.Add(grabbable);
                grabbable.HoverEnter();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            GrabInteractable grabbable = other.GetComponent<GrabInteractable>();
            if (grabbable)
            {
                if (!grabbable.isGrabbed) m_CollidedGrabbables.Remove(grabbable);
                grabbable.HoverExit();
            }
        }

        protected override void OnGripPressed(InputAction.CallbackContext ctx) => GripPressed(ctx);
        protected override void OnGripReleased(InputAction.CallbackContext ctx) => GripReleased(ctx);
        protected override void OnJoystickPerformed(InputAction.CallbackContext ctx) => JoystickPerformed(ctx);

        /// <summary>
        /// Grabs the hovered interactables when the grip button is pressed.
        /// </summary>
        /// <param name="ctx">Input Action Callback Context from the grip button.</param>
        private void GripPressed(InputAction.CallbackContext ctx)
        {
            m_IsGrabbing = true;
            HideVolume();
            foreach (GrabInteractable grabbable in m_CollidedGrabbables)
            {
                grabbable.Grab(transform);
            }
        }

        /// <summary>
        /// Releases the hovered interactables when the grip button is released.
        /// </summary>
        /// <param name="ctx">Input Action Callback Context from the grip button.</param>
        private void GripReleased(InputAction.CallbackContext ctx)
        {
            m_IsGrabbing = false;
            ShowVolume();
            foreach (GrabInteractable grabbable in m_CollidedGrabbables.ToList())
            {
                grabbable.Release();
                if (!grabbable.isHovered)
                {
                    m_CollidedGrabbables.Remove(grabbable);
                }
            }
        }

        /// <summary>
        /// Changes the size of the near interaction area when joystick moves sideways.
        /// </summary>
        /// <param name="ctx">Input Action Callback Context. A vector2 from the joystick.</param>
        private void JoystickPerformed(InputAction.CallbackContext ctx)
        {
            Vector2 inputVector = ctx.ReadValue<Vector2>();
            if (!m_IsGrabbing && Math.Abs(inputVector.x) > 0.2f)
            {
                Vector3 newScale = NearSphereScale(transform.localScale, inputVector.x * m_ScaleSpeed, 
                    Time.deltaTime, m_MinColliderScale, m_MaxColliderScale);
                transform.localScale = newScale;
                m_TransformVisualizer.transform.localScale =
                    new Vector3(1f / newScale.x, 1f / newScale.y, 1f / newScale.z) * 0.1f;
            }
        }
        
        /// <summary>
        /// Change the current scale of the near interactor sphere to allow more controlled interaction.
        /// Clamp the result based on input range.
        /// </summary>
        /// <remarks>
        /// After implemented, you will be able to change the scale of the sphere selection area in
        /// the near interaction method.
        /// </remarks>
        /// <param name="currentScale">The current scale of the sphere.</param>
        /// <param name="scaleSpeed">The scaling speed in scale/second. It's sign indicates the scaling direction.
        /// This is affected by the joystick input from player controller.</param>
        /// <param name="deltaTime">The time in seconds that passed during this frame.</param>
        /// <param name="minScale">The minimum scale of the sphere.</param>
        /// <param name="maxScale">The maximum scale of the sphere.</param>
        /// <returns>Returns the new scale of the near interactor sphere.</returns>
        public Vector3 NearSphereScale(Vector3 currentScale, float scaleSpeed, float deltaTime, Vector3 minScale, Vector3 maxScale)
        {
            Vector3 scale = 0.1f * Vector3.one;
            
            // TODO - Implement method
            // <solution>
            // Your code here
            scale = currentScale;
            float scaleAmount = scaleSpeed * deltaTime;
            scale += scaleAmount * currentScale;
            scale = Vector3.Max(minScale, Vector3.Min(maxScale, scale));
            // </solution>
            
            return scale;
        }

        /// <summary>
        /// Hides the visual indication of the near interactor volume.
        /// </summary>
        private void HideVolume()
        {
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
        }

        /// <summary>
        /// Displays the visual indication of the near interactor volume.
        /// </summary>
        private void ShowVolume()
        {
            GetComponent<MeshRenderer>().enabled = true;
            GetComponent<Collider>().enabled = true;
        }
    }
}
