using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit;

namespace XRC.Assignments.Project.G07
{
    public class TargetObjectProvider : MonoBehaviour
    {
        [SerializeField] private InputActionReference m_TargetObjectProviderInputActionReference;
        [SerializeField] private XRBaseInteractor m_DirectInteractor;
        private ChangeObjectColor m_ChangeObjectColor;
        private ScaleObjectPerAxis m_ScaleObjectPerAxis;
        private MeshManipulation m_MeshManipulation;
        
        private Action<Transform, Vector3, Quaternion> m_ProvideTargetObject;
        
        // Start is called before the first frame update
        void Start()
        {
            m_ChangeObjectColor = GetComponent<ChangeObjectColor>();
            m_ScaleObjectPerAxis = GetComponent<ScaleObjectPerAxis>();
            m_MeshManipulation = GetComponent<MeshManipulation>();
            m_TargetObjectProviderInputActionReference.action.performed += TargetObjectProviderPerformed;
            if (m_ChangeObjectColor != null)
            {
                m_ProvideTargetObject += m_ChangeObjectColor.SetTargetObject;
            }
            if (m_ScaleObjectPerAxis != null)
            {
                Debug.Log("ScaleObjectPerAxis is not null.");
                m_ProvideTargetObject += m_ScaleObjectPerAxis.SetTargetObject;
            }
            if (m_MeshManipulation != null)
            {
                m_ProvideTargetObject += m_MeshManipulation.SetTargetObject;
            }
        }

        private void TargetObjectProviderPerformed(InputAction.CallbackContext obj)
        {
            if (m_DirectInteractor.selectTarget != null)
            {
                Debug.Log("Grip button pressed.");
                GameObject targetObject = m_DirectInteractor.selectTarget.gameObject;
                Vector3 targetObjectionPosition = new Vector3(targetObject.transform.position.x, targetObject.transform.position.y, targetObject.transform.position.z);
                Quaternion targetObjectRotation = new Quaternion(targetObject.transform.rotation.x, targetObject.transform.rotation.y, targetObject.transform.rotation.z, targetObject.transform.rotation.w);
                m_ProvideTargetObject?.Invoke(targetObject.transform, targetObjectionPosition, targetObjectRotation);
            }
        }

        public void ReleaseObjects()
        {
            if (m_DirectInteractor.selectTarget)
            {
                m_DirectInteractor.interactionManager.ClearInteractorSelection(m_DirectInteractor);
            }
        }
        
        private void OnEnable()
        {
            m_TargetObjectProviderInputActionReference.action.Enable();
        }
    }
}