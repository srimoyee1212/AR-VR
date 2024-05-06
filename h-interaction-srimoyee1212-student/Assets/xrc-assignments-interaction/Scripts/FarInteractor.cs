using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace XRC.Assignments.Interaction
{
    /// <summary>
    /// The interactor that supports far interaction.
    /// </summary>
    [RequireComponent(typeof(LineRenderer))]
    public class FarInteractor : BaseInteractor
    {
        private LineRenderer m_LineRenderer;

        [Tooltip("The color of the ray when hit point is grabbable")] [SerializeField]
        private Gradient m_ValidLineColor;

        [Tooltip("The color of the ray when hit point is not grabbable")] [SerializeField]
        private Gradient m_InvalidLineColor;

        private float m_InteractRange = 10f;

        private GrabInteractable m_TargetGrabbable;
        private bool m_IsGrabbing;

        private float m_MoveSpeed = 1f;
        private float m_RotateSpeed = 180f;

        protected override void OnGripPressed(InputAction.CallbackContext ctx) => GripPressed(ctx);
        protected override void OnGripReleased(InputAction.CallbackContext ctx) => GripReleased(ctx);
        protected override void OnJoystickPerformed(InputAction.CallbackContext ctx) => JoystickPerformed(ctx);

        /// <summary>
        /// Grabs the interactable when the grip button is pressed.
        /// </summary>
        /// <param name="ctx">Input Action Callback Context from the grip button.</param>
        private void GripPressed(InputAction.CallbackContext ctx)
        {
            if (m_TargetGrabbable)
            {
                m_TargetGrabbable.Grab(transform);
                m_IsGrabbing = true;
                HideLine();
            }
        }

        /// <summary>
        /// Releases the interactable when the grip button is released.
        /// </summary>
        /// <param name="ctx">Input Action Callback Context from the grip button.</param>
        private void GripReleased(InputAction.CallbackContext ctx)
        {
            if (m_IsGrabbing) m_TargetGrabbable.Release();
            m_IsGrabbing = false;
        }

        private void Start()
        {
            m_LineRenderer = GetComponent<LineRenderer>();
        }

        private void Update()
        {
            if (!m_IsGrabbing)
            {
                Vector3 endPoint = ShootInteractorRay(transform.position, transform.forward, m_InteractRange);
                ShowLine(transform.position, endPoint, m_TargetGrabbable);
            }
        }

        /// <summary>
        /// Handles the interactor ray. Displays it and detects hit point.
        /// Updates the hovered interactable based on the hit.
        /// </summary>
        /// <param name="startingPosition">The position at the hand.</param>
        /// <param name="pointingVector">The forward direction of the hand.</param>
        /// <param name="range">The length of the interactor ray.</param>
        /// <returns>Returns the hit position. It will be the end point of the ray if no valid hit point.</returns>
        private Vector3 ShootInteractorRay(Vector3 startingPosition, Vector3 pointingVector, float range)
        {
            RaycastHit hit;
            if (Physics.Raycast(startingPosition, pointingVector, out hit, range))
            {
                GrabInteractable grabbable = hit.transform.gameObject.GetComponent<GrabInteractable>();
                if (grabbable && !grabbable.isGrabbed)
                {
                    //Debug.Log(grabbable);
                    if (m_TargetGrabbable && m_TargetGrabbable != grabbable) m_TargetGrabbable.HoverExit();
                    m_TargetGrabbable = grabbable;
                    m_TargetGrabbable.HoverEnter();
                }
                else ResetGrabbable();

                return hit.point;
            }

            ResetGrabbable();
            return startingPosition + pointingVector * range;
        }

        /// <summary>
        /// Removes the hovered grabbable object from the target grabbable field.
        /// Called when the interactor ray stops hovering the target grabbable.
        /// </summary>
        private void ResetGrabbable()
        {
            if (m_TargetGrabbable)
            {
                m_TargetGrabbable.HoverExit();
                m_TargetGrabbable = null;
            }
        }

        /// <summary>
        /// Displays the interactor ray.
        /// </summary>
        /// <param name="startPoint">The start point of the ray.</param>
        /// <param name="endPoint">The end point of the ray.</param>
        /// <param name="isValid">whether the hit point is a valid interactable.</param>
        private void ShowLine(Vector3 startPoint, Vector3 endPoint, bool isValid)
        {

            m_LineRenderer.positionCount = 2;
            m_LineRenderer.SetPosition(0, startPoint);
            m_LineRenderer.SetPosition(1, endPoint);
            m_LineRenderer.colorGradient = isValid ? m_ValidLineColor : m_InvalidLineColor;
        }

        /// <summary>
        /// Hides the interactor ray.
        /// </summary>
        private void HideLine()
        {
            m_LineRenderer.positionCount = 0;
        }




        /// <summary>
        /// Manipulates the grabbed object when joystick is pressed.
        /// It moves the object closer and farther when joystick's y value is changed,
        /// and rotates the object around its up axis when x value is changed. 
        /// </summary>
        /// <param name="ctx">Input Action Callback Context. A vector2 from the joystick.</param>
        private void JoystickPerformed(InputAction.CallbackContext ctx)
        {
            Vector2 inputVector = ctx.ReadValue<Vector2>();
            if (m_IsGrabbing && m_TargetGrabbable.isGrabbed)
            {
                Transform attachTransform = m_TargetGrabbable.attachTransform();
                // manipulation
                if (Math.Abs(inputVector.y) > 0.3f)
                {
                    float distance = Vector3.Project(attachTransform.position - transform.position, transform.forward)
                        .magnitude;
                    FarManipulationMove(m_TargetGrabbable.transform, transform, attachTransform,
                        (distance + m_MoveSpeed) * inputVector.y, Time.deltaTime);
                }

                if (Math.Abs(inputVector.x) > 0.3f)
                {
                    // rotate around up vector
                    FarManipulationRotate(m_TargetGrabbable.transform, attachTransform,
                        inputVector.x * m_RotateSpeed, Time.deltaTime);
                }
            }

        }

        /// <summary>
        /// Adjust interactable transform to move the grabbed interactable away and closer to the interactor,
        /// along the forward direction of the interactor, by distance determined by speed and delta time.
        /// Edge Cases: Make sure that the attach transform's projection on the forward direction always stays
        /// on the non-negative side, such that the object is not moved to the back of the hand.
        /// Note: the attach transform could be equal to the interactable object itself if it is not assigned
        /// by the user.
        /// </summary>
        /// <remarks>
        /// After implemented, you will be able to move the grabbed interactable along the ray with the joystick
        /// using the far interaction method.
        /// </remarks>
        /// <param name="interactableTransform">The transform of the interactable that is being manipulated.</param>
        /// <param name="interactorTransform">The transform of the interactor that is manipulating the interactable.</param>
        /// <param name="attachTransform">The transform of the attach point. For interactable without attach transform,
        /// this value will be the interactable transform. </param>
        /// <param name="moveSpeed">The speed of the movement in meter/second. Its sign and magnitude is already
        /// determined by the joystick input and affects the movement direction. Forward is positive. </param>
        /// <param name="deltaTime">The time in seconds that passed during this frame.</param>
        public void FarManipulationMove(Transform interactableTransform, Transform interactorTransform,
            Transform attachTransform, float moveSpeed, float deltaTime)
        {
            
            // TODO - Implement method
            // <solution>
            // Your code here
            Vector3 direction =attachTransform.position - interactorTransform.position;

            
            float projection = Vector3.Dot(direction, interactorTransform.forward);
            float distanceToMove = moveSpeed * deltaTime;

            if (projection + distanceToMove < 0f)
            {
                
                distanceToMove = -projection;
            }

            
            interactableTransform.position += interactorTransform.forward * distanceToMove;
            // </solution>
            
        }

        /// <summary>
        /// Rotate grabbed interactable around the position and up vector of <paramref name="attachTransform">.
        /// You can use the Transform.RotateAround() method.
        /// </summary>
        /// <remarks>
        /// After implemented, you will be able to rotate the grabbed interactable with the joystick
        /// using the far interaction method.
        /// </remarks>
        /// <param name="interactableTransform">The transform of the interactable that is being manipulated.</param>
        /// <param name="attachTransform">The transform of the attach point. For interactable without attach transform,
        /// this value will be the interactable transform, i.e. it will rotate around it's own transform. </param>
        /// <param name="angleSpeed">The speed of the rotation in degree/second. Its sign and magnitude is already
        /// determined by the joystick input and affects the rotation direction. Clockwise is positive. </param>
        /// <param name="deltaTime">The time in seconds that passed during this frame.</param>
        public void FarManipulationRotate(Transform interactableTransform, Transform attachTransform, float angleSpeed,
            float deltaTime)
        {
            
            // TODO - Implement method
            // <solution>
            // Your code here
            float rotationAngle = angleSpeed * deltaTime;

            Vector3 rotationAxis = attachTransform.up;

            interactableTransform.RotateAround(attachTransform.position, rotationAxis, rotationAngle);
            // </solution>
            
        }
    }
}