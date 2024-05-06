using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace XRC.Assignments.Locomotion
{
    /// <summary>
    /// Locomotion controller that supports snap turn.
    /// </summary>
    public class SnapTurnController : LocomotionBase
    {
        [Tooltip("The angle degree of each snap turn")] [SerializeField]
        private float m_AngleDegree = 30f;

        /// <summary>
        /// The angle degree of rotation per time.
        /// </summary>
        public float angleDegree
        {
            get => m_AngleDegree;
            set => m_AngleDegree = value;
        }

        private bool turned;

        protected override void Start()
        {
            base.Start();
            m_ActionReference = InputActionReference.Create(m_Manager.inputAction["RightHand/Primary2DAxis"]);
            m_ActionReference.action.Enable();
            base.OnEnable();
        }

        protected override void OnActionPerformed(InputAction.CallbackContext ctx) => TurnPlayer_Snap(ctx);

        protected override void OnActionCanceled(InputAction.CallbackContext ctx)
        {
            turned = false;
        }
        
        /// <summary>
        /// Turns the player rig based on the Vector2 value from <paramref name="ctx"/>.
        /// </summary>
        /// <param name="ctx">Input Action Callback Context, with a Vector2 value from right hand joystick.</param>
        private void TurnPlayer_Snap(InputAction.CallbackContext ctx)
        {
            Vector2 controllerVec = ctx.ReadValue<Vector2>();
            if (Math.Abs(controllerVec.x) > 0.3f)
            {
                if (!turned)
                {
                    float degrees = (controllerVec.x > 0f) ? m_AngleDegree : -m_AngleDegree;
                    Matrix4x4 rotatedTransform = RotatedTransform(
                        m_PlayerRig.transform.localToWorldMatrix,
                        m_PlayerCamera.transform.position, Vector3.up, degrees);
                    m_PlayerRig.transform.rotation = rotatedTransform.rotation;
                    m_PlayerRig.transform.position = rotatedTransform.GetPosition();
                    turned = true;
                }
            }
            else turned = false;
        }
    }
}
