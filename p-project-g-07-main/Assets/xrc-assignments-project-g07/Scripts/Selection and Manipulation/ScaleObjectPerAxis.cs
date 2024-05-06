using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

namespace XRC.Assignments.Project.G07
{
    public class ScaleObjectPerAxis : MonoBehaviour
    {
        [SerializeField] private GameObject m_ScaleObjectPerAxisMenu;
        [SerializeField] private GameObject m_RayInteractorObject;
        [SerializeField] private InputActionReference m_CreateObjectInputActionReference;
        [SerializeField] private Slider m_XAxisSlider;
        [SerializeField] private Slider m_YAxisSlider;
        [SerializeField] private Slider m_ZAxisSlider;
        
        private Transform m_TargetObjectTransform;
        private Vector3 m_TargetObjectInitialPosition;
        private Quaternion m_TargetObjectInitialRotation;
        private TargetObjectProvider m_TargetObjectProvider;
        
        // Start is called before the first frame update
        void Start()
        {
            m_TargetObjectProvider = GetComponent<TargetObjectProvider>();
        }

        // Update is called once per frame
        void Update()
        {
            if (m_TargetObjectTransform)
            {
                m_TargetObjectTransform.localScale = new Vector3(m_XAxisSlider.value, m_YAxisSlider.value, m_ZAxisSlider.value);
            }
        }

        public void ScaleObjectPerAxisActionPerformed(InputAction.CallbackContext obj)
        {
            // m_TargetObjectProvider.ReleaseObjects();
            // m_TargetObjectTransform.SetPositionAndRotation(m_TargetObjectInitialPosition, m_TargetObjectInitialRotation);
            m_ScaleObjectPerAxisMenu.SetActive(!m_ScaleObjectPerAxisMenu.activeSelf);
            m_RayInteractorObject.SetActive(!m_RayInteractorObject.activeSelf);
            if (m_CreateObjectInputActionReference.action.enabled)
            {
                m_CreateObjectInputActionReference.action.Disable();
            }
            else
            {
                m_CreateObjectInputActionReference.action.Enable();
            }
        }

        public void SetTargetObject(Transform targetObjectTransform, Vector3 targetObjectPosition, Quaternion targetObjectRotation)
        {
            m_TargetObjectTransform = targetObjectTransform;
            m_TargetObjectInitialPosition = targetObjectPosition;
            m_TargetObjectInitialRotation = targetObjectRotation;

            Vector3 targetObjectScale = m_TargetObjectTransform.localScale;
            m_XAxisSlider.value = targetObjectScale.x;
            m_YAxisSlider.value = targetObjectScale.y;
            m_ZAxisSlider.value = targetObjectScale.z;
        }
    }
}