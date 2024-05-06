using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace XRC.Assignments.Project.G07
{
    public class DuplicateObjectInput : MonoBehaviour
    {
        [SerializeField] private InputActionReference m_DuplicateInputActionReference;
        private DuplicateObject m_DuplicateObject;

        void Start()
        {
            m_DuplicateObject = GetComponent<DuplicateObject>();
            m_DuplicateInputActionReference.action.performed += m_DuplicateObject.Duplicate;
        }

        private void OnEnable()
        {
            m_DuplicateInputActionReference.action.Enable();
        }

        private void OnDisable()
        {
            m_DuplicateInputActionReference.action.Disable();
        }

        private void OnDestroy()
        {
            m_DuplicateInputActionReference.action.performed -= m_DuplicateObject.Duplicate;
        }
    }
}