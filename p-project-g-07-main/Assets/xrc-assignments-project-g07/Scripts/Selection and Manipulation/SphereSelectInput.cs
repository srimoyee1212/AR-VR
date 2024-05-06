using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace XRC.Assignments.Project.G07
{
    public class SphereSelectInput : MonoBehaviour
    {
        [SerializeField] private InputActionReference m_SphereSelectInputActionReference;
        private SphereSelect m_SphereSelect;
        
        void Start()
        {
            m_SphereSelect = GetComponent<SphereSelect>();
            m_SphereSelectInputActionReference.action.started += m_SphereSelect.SphereSelectStarted;
            m_SphereSelectInputActionReference.action.performed += m_SphereSelect.SphereSelectOngoing;
            
        }
        

        private void OnEnable()
        {
            m_SphereSelectInputActionReference.action.Enable();
           
        }
        
        
    }
}