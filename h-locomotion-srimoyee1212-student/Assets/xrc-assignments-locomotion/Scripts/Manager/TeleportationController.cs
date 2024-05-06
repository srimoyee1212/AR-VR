using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace XRC.Assignments.Locomotion
{
    /// <summary>
    /// Locomotion controller that supports teleportation.
    /// </summary>
    public class TeleportationController : LocomotionBase
    {
        [Tooltip("Whether to rotate player direction with the joystick while teleporting")] [SerializeField]
        private bool m_RotatePlayer = true;

        /// <summary>
        /// Whether to rotate player direction with the joystick while teleporting.
        /// </summary>
        public bool rotatePlayer
        {
            get => m_RotatePlayer;
            set => m_RotatePlayer = value;
        }

        // Properties of the teleport ray
        private List<Vector3> m_SamplePoints = new List<Vector3>();
        private float m_FlightTime = 3f;
        private int m_LineSegmentCount = 30;
        private float m_Distance = 8f;
        private GameObject m_PlayerHandAim;

        // Helper fields
        private bool m_ShootRay;
        private bool m_ReadyToTeleport;
        private Vector3 m_TargetPosition;
        private Vector3 m_TargetDirection;

        /// <summary>
        /// Whether to shoot the teleport ray.
        /// </summary>
        public bool shootRay
        {
            get => m_ShootRay;
        }

        /// <summary>
        /// Whether it is ready to teleport (the hit point from the teleport ray is valid).
        /// </summary>
        public bool readyToTeleport
        {
            get => m_ReadyToTeleport;
        }

        /// <summary>
        /// The target position received from the teleport ray.
        /// </summary>
        public Vector3 targetPosition
        {
            get => m_TargetPosition;
        }

        /// <summary>
        /// The target direction determined by the current player orientation (and the left joystick
        /// if <paramref name="rotatePlayer"/>).
        /// </summary>
        public Vector3 targetDirection
        {
            get => m_TargetDirection;
        }

        /// <summary>
        /// The list of points sampled along the teleport ray.
        /// </summary>
        public List<Vector3> samplePoints
        {
            get => m_SamplePoints;
        }

        protected override void OnActionPerformed(InputAction.CallbackContext ctx) => ShootTeleportRay(ctx);

        protected override void OnActionCanceled(InputAction.CallbackContext ctx)
        {
            MoveAndRotatePlayer();
        }

        protected override void Start()
        {
            base.Start();
            m_PlayerHandAim = m_Manager.playerHandAim;
            m_ActionReference = InputActionReference.Create(m_Manager.inputAction["LeftHand/Primary2DAxis"]);
            m_ActionReference.action.Enable();
            base.OnEnable();
        }

        /// <summary>
        /// Handles teleportation: shoots teleport ray, moves and rotate player when teleport.
        /// </summary>
        /// <param name="ctx">Input Action Callback Context, with a Vector2 value from left hand joystick.</param>
        private void ShootTeleportRay(InputAction.CallbackContext ctx)
        {
            Vector2 controllerVec = ctx.ReadValue<Vector2>();
            if (controllerVec.magnitude > 0.3f)
            {
                // Joystick moved. Draw trajectory and get target facing direction
                Trajectory(m_PlayerHandAim.transform.forward * -m_Distance, m_PlayerHandAim.transform.position);
                m_ShootRay = true;
                m_TargetDirection = m_RotatePlayer
                    ? GetFacingDirection(m_PlayerHandAim.transform.forward, controllerVec)
                    : Vector3.ProjectOnPlane(m_PlayerCamera.transform.forward, Vector3.up).normalized;
            } 
            else MoveAndRotatePlayer();
        }

        /// <summary>
        /// If <paramref name="readyToTeleport"/>, moves and rotates the player rig so that player camera
        /// is at the <paramref name="targetPosition"/> facing <paramref name="targetDirection"/>
        /// </summary>
        private void MoveAndRotatePlayer()
        {
            m_ShootRay = false;
            if (m_ReadyToTeleport)
            {
                // move player
                m_PlayerRig.transform.position = GetTeleportedPosition(m_TargetPosition,
                    m_PlayerRig.transform.position,
                    m_PlayerCamera.transform.position);
                m_ReadyToTeleport = false;
                // rotate player
                float angleDegree =
                    Vector3.SignedAngle(Vector3.ProjectOnPlane(m_PlayerCamera.transform.forward, Vector3.up).normalized,
                        m_TargetDirection, Vector3.up);
                Matrix4x4 rotatedTransform = RotatedTransform(m_PlayerRig.transform.localToWorldMatrix, 
                    m_PlayerCamera.transform.position, Vector3.up, angleDegree);
                m_PlayerRig.transform.rotation = rotatedTransform.rotation;
                m_PlayerRig.transform.position = rotatedTransform.GetPosition();
            }
        }
        
        /// <summary>
        /// Calculates the moved world position of the player rig that aligns <paramref name="startingCameraPosition"/>
        /// with <paramref name="targetPosition"/> on the horizontal (xz) plane projection, and aligns
        /// the vertical (y) value of the player rig with that of the <paramref name="targetPosition"/>
        /// </summary>
        /// <remarks>
        /// After implemented, the teleportation's position will be working.
        /// </remarks>
        /// <param name="targetPosition">The position of the hit point between
        /// teleportation ray and the ground plane.</param>
        /// <param name="startingRigPosition">The initial world position of the player rig. </param>
        /// <param name="startingCameraPosition">The initial world position of the player camera.</param>
        /// <returns>Returns world position of the player rig after movement.</returns>
        public Vector3 GetTeleportedPosition(Vector3 targetPosition, Vector3 startingRigPosition,
            Vector3 startingCameraPosition)
        {
            Vector3 newRigPosition = new Vector3();
            
            // TODO - Implement method
            // <solution>
            // Your code here
            Vector2 horizontalOffset = new Vector2(targetPosition.x - startingCameraPosition.x, targetPosition.z - startingCameraPosition.z);

            // Update the starting rig position using the horizontal offset
            newRigPosition = new Vector3(
                startingRigPosition.x + horizontalOffset.x,
                targetPosition.y,
                startingRigPosition.z + horizontalOffset.y);

            // </solution>
            
            return newRigPosition;
        }

        /// <summary>
        /// Calculates the facing direction of the player after teleportation based on 
        /// <paramref name="handPointingDirection"/> and <paramref name="inputVector"/>.
        /// The result needs to be normalized and projected onto the horizontal (xz) plane.
        /// </summary>
        /// <remarks>
        /// After implemented, teleportation indicator will rotate following joystick input.
        /// Teleportation's rotation will be fully working with the RotatedTransform() method.
        /// </remarks>
        /// <param name="handPointingDirection">The forward vector that the player's hand is pointing towards. </param>
        /// <param name="inputVector">The Vector2 read from player controller's left joystick. </param>
        /// <returns>Returns the forward direction that the player is supposed to face after teleportation.</returns>
        public Vector3 GetFacingDirection(Vector3 handPointingDirection, Vector2 inputVector)
        {
            Vector3 facingDirection = new Vector3();
            
            // TODO - Implement method
            // <solution>
            // Your code here
            Vector3 inputDirection = new Vector3(inputVector.x, 0, inputVector.y);

            facingDirection = Quaternion.LookRotation(inputDirection) * handPointingDirection;

            facingDirection.y = 0;
            
            facingDirection.Normalize();
            // </solution>
            
            return facingDirection;
        }

        /// <summary>
        /// Handles the shape and ray casting of the teleport ray.
        /// </summary>
        /// <param name="pointingVector">The initial forward vector of the ray when it emits from player's hand. </param>
        /// <param name="startingPosition">The position of the point where the ray is emitted. </param>
        private void Trajectory(Vector3 pointingVector, Vector3 startingPosition)
        {
            m_SamplePoints.Clear();
            float stepTime = m_FlightTime / m_LineSegmentCount;
            m_SamplePoints.Add(startingPosition);
            for (int i = 1; i < m_LineSegmentCount; i++)
            {
                float stepTimePassed = stepTime * i;
                Vector3 MovementVec = new Vector3(pointingVector.x * stepTimePassed,
                    pointingVector.y * stepTimePassed - 0.4f * Physics.gravity.y * stepTimePassed * stepTimePassed,
                    pointingVector.z * stepTimePassed);
                Vector3 newPoint = -MovementVec + startingPosition;

                RaycastHit hit;
                if (Physics.Raycast(m_SamplePoints[i - 1], newPoint - m_SamplePoints[i - 1], out hit,
                    (newPoint - m_SamplePoints[i - 1]).magnitude))
                {
                    if (Vector3.Project(hit.normal, Vector3.up).magnitude > 0.5f)
                    {
                        // hit point is a valid teleportation area
                        GetReadyToTeleport(hit.point);
                    }
                    else NotReadyToTeleport();

                    m_SamplePoints.Add(hit.point);
                    break;
                }

                if (i == m_LineSegmentCount - 1) NotReadyToTeleport();
                m_SamplePoints.Add(newPoint);
            }
        }

        /// <summary>
        /// Set <paramref name="readyToTeleport"/> to true and updates <paramref name="targetPosition"/>
        /// when a valid teleportation point is hit.
        /// </summary>
        private void GetReadyToTeleport(Vector3 position)
        {
            m_ReadyToTeleport = true;
            m_TargetPosition = position;
        }

        /// <summary>
        /// Set <paramref name="readyToTeleport"/> to false when no teleportation point is hit.
        /// </summary>
        private void NotReadyToTeleport()
        {
            m_ReadyToTeleport = false;
        }
    }
}
