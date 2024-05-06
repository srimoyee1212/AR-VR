using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

namespace XRC.Assignments.Project.G07
{
    public class ControllerTexture : MonoBehaviour
    {
        [SerializeField] private Material m_WhiteButtonMaterial;
        [SerializeField] private Material m_TriggerPressedMaterial;
        [SerializeField] private Material m_GripPressedMaterial;
        [SerializeField] private InputActionReference m_LeftTriggerButtonAction;
        [SerializeField] private InputActionReference m_RightTriggerButtonAction;
        [SerializeField] private InputActionReference m_LeftGripButtonAction;
        [SerializeField] private InputActionReference m_RightGripButtonAction;
        [SerializeField] private XRBaseController m_LeftHandController;
        [SerializeField] private XRBaseController m_RightHandController;
        [SerializeField] private Sprite m_DeleteButtonSprite;
        [SerializeField] private Sprite m_UndoButtonSprite;
        [SerializeField] private SpriteRenderer m_UndoAndDeleteSpriteRenderer;
        
        private Renderer m_LeftHandControllerGripButtonRenderer; 
        private Renderer m_LeftHandControllerTriggerButtonRenderer; 
        private Renderer m_RightHandControllerGripButtonRenderer; 
        private Renderer m_RightHandControllerTriggerButtonRenderer; 
        private bool m_ClonesCreated = false;
        
        // Start is called before the first frame update
        void Start()
        {
            m_LeftTriggerButtonAction.action.performed += LeftTriggerButtonActionPerformed;
            m_LeftTriggerButtonAction.action.canceled += LeftTriggerButtonActionCanceled;
            m_RightTriggerButtonAction.action.performed += RightTriggerButtonActionPerformed;
            m_RightTriggerButtonAction.action.canceled += RightTriggerButtonActionCanceled;
            m_LeftGripButtonAction.action.performed += LeftGripButtonActionPerformed;
            m_LeftGripButtonAction.action.canceled += LeftGripButtonActionCanceled;
            m_RightGripButtonAction.action.performed += RightGripButtonActionPerformed;
            m_RightGripButtonAction.action.canceled += RightGripButtonActionCanceled;
        }

        private void LeftGripButtonActionCanceled(InputAction.CallbackContext obj)
        {
            m_LeftHandControllerGripButtonRenderer.material = m_WhiteButtonMaterial;
        }

        private void LeftGripButtonActionPerformed(InputAction.CallbackContext obj)
        {
            m_LeftHandControllerGripButtonRenderer.material = m_GripPressedMaterial;
        }

        private void RightTriggerButtonActionCanceled(InputAction.CallbackContext obj)
        {
            m_RightHandControllerTriggerButtonRenderer.material = m_WhiteButtonMaterial;
        }

        private void RightTriggerButtonActionPerformed(InputAction.CallbackContext obj)
        {
            m_RightHandControllerTriggerButtonRenderer.material = m_TriggerPressedMaterial;
        }

        private void RightGripButtonActionPerformed(InputAction.CallbackContext obj)
        {
            m_RightHandControllerGripButtonRenderer.material = m_GripPressedMaterial;
            m_UndoAndDeleteSpriteRenderer.sprite = m_DeleteButtonSprite;
        }

        private void RightGripButtonActionCanceled(InputAction.CallbackContext obj)
        {
            m_RightHandControllerGripButtonRenderer.material = m_WhiteButtonMaterial;
            m_UndoAndDeleteSpriteRenderer.sprite = m_UndoButtonSprite;
        }

        private void LeftTriggerButtonActionCanceled(InputAction.CallbackContext obj)
        {
            m_LeftHandControllerTriggerButtonRenderer.material = m_WhiteButtonMaterial;
        }

        private void LeftTriggerButtonActionPerformed(InputAction.CallbackContext obj)
        {
            m_LeftHandControllerTriggerButtonRenderer.material = m_TriggerPressedMaterial;
        }

        // Update is called once per frame
        void Update()
        {
            if (!m_ClonesCreated)
            {
                m_LeftHandControllerGripButtonRenderer = m_LeftHandController.model.Find("Bumper").gameObject.GetComponent<Renderer>();
                m_LeftHandControllerTriggerButtonRenderer = m_LeftHandController.model.Find("Trigger").gameObject.GetComponent<Renderer>();
                m_RightHandControllerGripButtonRenderer = m_RightHandController.model.Find("Bumper").gameObject.GetComponent<Renderer>();
                m_RightHandControllerTriggerButtonRenderer = m_RightHandController.model.Find("Trigger").gameObject.GetComponent<Renderer>();
                m_ClonesCreated = true;
            }
        }
    }
}
