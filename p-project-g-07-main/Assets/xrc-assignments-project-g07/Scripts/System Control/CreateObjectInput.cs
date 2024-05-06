using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace XRC.Assignments.Project.G07
{
    public class CreateObjectInput : MonoBehaviour
    {
        [SerializeField] private InputActionReference m_CreateObjectInputActionReference;
        private CreateObject m_CreateObject;
    
        // Start is called before the first frame update
        void Start()
        {
            m_CreateObject = GetComponent<CreateObject>();
            m_CreateObjectInputActionReference.action.started += m_CreateObject.CreateObjectStarted;
            m_CreateObjectInputActionReference.action.performed += m_CreateObject.CreateObjectPerformed;
            m_CreateObjectInputActionReference.action.canceled += m_CreateObject.CreateObjectCancelled;
        }

        private void OnEnable()
        {
            m_CreateObjectInputActionReference.action.Enable();
        }

        private void OnDisable()
        {
            m_CreateObjectInputActionReference.action.Disable();
        }
    }
}

