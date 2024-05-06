using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

namespace XRC.Assignments.Project.G07
{
    public class DuplicateObject : MonoBehaviour
    {
        [SerializeField] private XRBaseInteractor m_DirectInteractor;
        [SerializeField] private InputActionReference m_CreateObjectInputActionReference;
        
        public float m_SpawnOffset = 0.1f;
        
        public void Duplicate(InputAction.CallbackContext context)
        {
            if (m_DirectInteractor.selectTarget != null)
            {
                m_CreateObjectInputActionReference.action.Disable();
                GameObject originalObject = m_DirectInteractor.selectTarget.gameObject;
                Vector3 offsetVector = originalObject.transform.forward * m_SpawnOffset;
                Instantiate(originalObject, originalObject.transform.position + offsetVector, originalObject.transform.rotation);
                Invoke(nameof(EnableCreateObject), 2f);
            }
        }
        
        private void EnableCreateObject()
        {
            m_CreateObjectInputActionReference.action.Enable();
        }
    }
}