using UnityEngine;
using UnityEngine.InputSystem;

namespace XRC.Assignments.Project.G07
{
    public class DeleteObjectInput : MonoBehaviour
    {
        [SerializeField] private InputActionReference m_DeleteInputActionReference;
        private DeleteObject m_DeleteObject;

        void Start()
        {
            m_DeleteObject = GetComponent<DeleteObject>();
            m_DeleteInputActionReference.action.performed += m_DeleteObject.Delete;
        }

        private void OnEnable()
        {
            m_DeleteInputActionReference.action.Enable();
        }

        private void OnDisable()
        {
            m_DeleteInputActionReference.action.Disable();
        }
        
        private void OnDestroy()
        {
            m_DeleteInputActionReference.action.performed -= m_DeleteObject.Delete; 
        }
    }
}