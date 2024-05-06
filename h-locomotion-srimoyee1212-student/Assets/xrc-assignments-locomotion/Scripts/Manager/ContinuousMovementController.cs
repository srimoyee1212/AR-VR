using UnityEngine;
using UnityEngine.InputSystem;

namespace XRC.Assignments.Locomotion
{
    /// <summary>
    /// Locomotion controller that supports continuous movement. 
    /// </summary>
    public class ContinuousMovementController : LocomotionBase
    {
        [Tooltip("Movement speed, meter per second")] [SerializeField]
        private float m_Speed = 1.4f;

        /// <summary>
        /// The speed of movement in meter per second.
        /// </summary>
        public float speed
        {
            get => m_Speed;
            set => m_Speed = value;
        }

        [Tooltip("Allow moving sideways")] [SerializeField]
        private bool m_Strafing = true;

        /// <summary>
        /// Whether to enable strafing (side movements).
        /// </summary>
        public bool strafing
        {
            get => m_Strafing;
            set => m_Strafing = value;
        }

        private Vector3 m_VerticalVelocity;
        private CharacterController m_CharacterController;

        protected override void Start()
        {
            base.Start();
            m_ActionReference = InputActionReference.Create(m_Manager.inputAction["LeftHand/Primary2DAxis"]);
            m_ActionReference.action.Enable();
            m_CharacterController = m_PlayerRig.GetComponent<CharacterController>();
            base.OnEnable();
        }

        private void Update()
        {
            PlayerFall();
        }

        protected override void OnActionPerformed(InputAction.CallbackContext ctx) => MovePlayer(ctx);

        /// <summary>
        /// Moves the player rig based on the Vector2 value from <paramref name="ctx"/>.
        /// </summary>
        /// <param name="ctx">Input Action Callback Context, with a Vector2 value from left hand joystick.</param>
        private void MovePlayer(InputAction.CallbackContext ctx)
        {
            Vector2 controllerVec = ctx.ReadValue<Vector2>();
            Vector3 targetPosition = MovedPosition(m_PlayerRig.transform.position,
                Vector3.ProjectOnPlane(m_PlayerCamera.transform.forward, Vector3.up).normalized,
                controllerVec.y * m_Speed, Time.deltaTime);
            if (m_Strafing)
            {
                targetPosition = MovedPosition(targetPosition,
                    Vector3.ProjectOnPlane(m_PlayerCamera.transform.right, Vector3.up).normalized,
                    controllerVec.x * m_Speed, Time.deltaTime);
            }

            MovePlayerWithController(targetPosition);
        }
        
        /// <summary>
        /// Calculates the moved world position of the player rig,
        /// from <paramref name="startingPosition"/> towards <paramref name="direction"/>
        /// by <paramref name="speed"/> over <paramref name="deltaTime"/>.
        /// </summary>
        /// <remarks>
        /// After implemented, the Continuous Movement technique will be working.
        /// </remarks>
        /// <param name="startingPosition">The world position of the player rig before movement.</param>
        /// <param name="direction">A Vector3 pointing to movement direction.
        /// It is normalized and parallel to the ground plane. </param>
        /// <param name="speed">The speed of the player in meters per second.</param>
        /// <param name="deltaTime">The time in seconds that passed during this frame.</param>
        /// <returns>Returns world position of the player rig after movement.</returns>
        public Vector3 MovedPosition(Vector3 startingPosition, Vector3 direction, float speed, float deltaTime)
        {
            Vector3 movedPosition = new Vector3();
            
            // TODO - Implement method according to summary
            // <solution>
            // Your code here
            Vector3 step = direction * speed * deltaTime;
            movedPosition = startingPosition + step;
            // </solution>
            
            return movedPosition;
        }

        /// <summary>
        /// Creates a locomotion event to move the player rig to <param name="targetPosition"></param>
        /// </summary>
        /// <param name="targetPosition">The location that the player rig is moving to.</param>
        private void MovePlayerWithController(Vector3 targetPosition)
        {
            // Use locomotion event if Character Controller is enabled
            if (m_CharacterController && m_CharacterController.enabled)
            {
                Vector3 movement = targetPosition - m_PlayerRig.transform.position;
                m_CharacterController.Move(movement);
            }
            // Move directly when there is no active Character Controller
            else m_PlayerRig.transform.position = targetPosition;
        }

        /// <summary>
        /// Calculates vertical velocity to handle free fall when player is not grounded.
        /// </summary>
        private void PlayerFall()
        {
            if (!m_CharacterController.isGrounded)
            {
                m_VerticalVelocity += Physics.gravity * Time.deltaTime;
                m_CharacterController.Move(m_VerticalVelocity * Time.deltaTime);
            }
            else m_VerticalVelocity = Vector3.zero;
        }
    }
}