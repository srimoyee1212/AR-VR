using UnityEngine;
using UnityEngine.InputSystem;

namespace XRC.Assignments.Interaction
{
    /// <summary>
    /// The interactor that is the base of both near and far interaction.
    /// </summary>
    public class BaseInteractor : MonoBehaviour
    {
        [Tooltip("Input Action Asset")] [SerializeField]
        private InputActionAsset m_InputAction = null;

        [Tooltip("Whether this is for left hand")] [SerializeField]
        private bool m_IsLeftHand;

        [Tooltip("TransformVisualizer")] [SerializeField]
        protected GameObject m_TransformVisualizer;

        protected InputActionReference m_TriggerReference = null;
        protected InputActionReference m_JoystickReference = null;

        private void Awake()
        {
            // assign input actions
            if (m_IsLeftHand)
            {
                m_TriggerReference = InputActionReference.Create(m_InputAction["LeftHand/GripPress"]);
                m_JoystickReference = InputActionReference.Create(m_InputAction["LeftHand/Primary2DAxis"]);
                transform.localPosition = new Vector3(0.005f, 0f, 0f);
                transform.localRotation = Quaternion.Euler(-3f, 0f, -6f);
            }
            else
            {
                m_TriggerReference = InputActionReference.Create(m_InputAction["RightHand/GripPress"]);
                m_JoystickReference = InputActionReference.Create(m_InputAction["RightHand/Primary2DAxis"]);
                transform.localPosition = new Vector3(-0.005f, 0f, 0f);
                transform.localRotation = Quaternion.Euler(-3f, 0f, 6f);
            }
        }

        private void OnEnable()
        {
            if (m_TriggerReference) m_TriggerReference.action.Enable();
            if (m_JoystickReference) m_JoystickReference.action.Enable();
            m_TriggerReference.action.started += OnGripPressed;
            m_TriggerReference.action.canceled += OnGripReleased;
            m_JoystickReference.action.performed += OnJoystickPerformed;
        }

        private void OnDisable()
        {
            if (m_TriggerReference) m_TriggerReference.action.Disable();
            if (m_JoystickReference) m_JoystickReference.action.Disable();
            m_TriggerReference.action.started -= OnGripPressed;
            m_TriggerReference.action.canceled -= OnGripReleased;
            m_JoystickReference.action.performed -= OnJoystickPerformed;
        }

        protected virtual void OnGripPressed(InputAction.CallbackContext ctx)
        {
        }

        protected virtual void OnGripReleased(InputAction.CallbackContext ctx)
        {
        }

        protected virtual void OnJoystickPerformed(InputAction.CallbackContext ctx)
        {
        }
    }
}
