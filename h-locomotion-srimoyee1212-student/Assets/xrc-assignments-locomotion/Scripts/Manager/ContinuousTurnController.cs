using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace XRC.Assignments.Locomotion
{
    /// <summary>
    /// Locomotion controller that supports continuous turn. 
    /// </summary>
    public class ContinuousTurnController : LocomotionBase
    {
        [Tooltip("Rotation speed, angle degree per second")] [SerializeField]
        private float m_AngleDegreeSpeed = 30f;

        /// <summary>
        /// The speed of rotation in angle degree per second.
        /// </summary>
        public float angleDegreeSpeed
        {
            get => m_AngleDegreeSpeed;
            set => m_AngleDegreeSpeed = value;
        }

        protected override void Start()
        {
            base.Start();
            m_ActionReference = InputActionReference.Create(m_Manager.inputAction["RightHand/Primary2DAxis"]);
            m_ActionReference.action.Enable();
            base.OnEnable();
        }

        protected override void OnActionPerformed(InputAction.CallbackContext ctx) => TurnPlayer_Continuous(ctx);

        /// <summary>
        /// Turns the player rig based on the Vector2 value from <paramref name="ctx"/>.
        /// </summary>
        /// <param name="ctx">Input Action Callback Context, with a Vector2 value from right hand joystick.</param>
        private void TurnPlayer_Continuous(InputAction.CallbackContext ctx)
        {
            Vector2 controllerVec = ctx.ReadValue<Vector2>();
            if (Math.Abs(controllerVec.x) > 0.3f)
            {
                float degrees = (controllerVec.x > 0f)
                    ? m_AngleDegreeSpeed * Time.deltaTime
                    : -m_AngleDegreeSpeed * Time.deltaTime;
                Matrix4x4 rotatedTransform = RotatedTransform(
                    m_PlayerRig.transform.localToWorldMatrix,
                    m_PlayerCamera.transform.position, Vector3.up, degrees);
                m_PlayerRig.transform.rotation = rotatedTransform.rotation;
                m_PlayerRig.transform.position = rotatedTransform.GetPosition();
            }
        }
    }
}