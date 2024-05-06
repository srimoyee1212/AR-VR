using UnityEngine;
using UnityEngine.InputSystem;

namespace XRC.Assignments.Geometry
{
    public class PlayerControl : MonoBehaviour
    {
        private const float k_MoveSpeed = 5;
        private const float k_RotateSpeed = 100;

        private DefaultInputActions m_Controls;
        private Vector2 m_Rotation;

        public void Awake()
        {
            m_Controls = new DefaultInputActions();
            m_Rotation = Vector2.zero;
            m_Controls.Player.Enable();
        }

        public void OnEnable()
        {
            m_Controls.Enable();
        }

        public void OnDisable()
        {
            m_Controls.Disable();
        }

        public void Update()
        {
            var input = m_Controls.Player.Move.ReadValue<Vector2>();

            Rotate(input);
            Translate(input);
        }

        private void Translate(Vector2 direction)
        {
            if (direction.sqrMagnitude < 0.01)
            {
                return;
            }

            var scaledMoveSpeed = k_MoveSpeed * Time.deltaTime;
            var move = Quaternion.Euler(0, transform.eulerAngles.y, 0) * new Vector3(0, 0, direction.y);
            transform.position += move * scaledMoveSpeed;
        }

        private void Rotate(Vector2 rotate)
        {
            if (rotate.sqrMagnitude < 0.01)
            {
                return;
            } 
            var scaledRotateSpeed = k_RotateSpeed * Time.deltaTime;
            m_Rotation.y += rotate.x * scaledRotateSpeed;
            transform.localEulerAngles = m_Rotation;
        }
    }
}