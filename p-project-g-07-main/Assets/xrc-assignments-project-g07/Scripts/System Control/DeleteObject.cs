using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

namespace XRC.Assignments.Project.G07
{
    public class DeleteObject : MonoBehaviour
    {
        [SerializeField] private XRBaseInteractor m_DirectInteractor;
        
        public void Delete(InputAction.CallbackContext context)
        {
            if (m_DirectInteractor.selectTarget != null)
            {
                Destroy(m_DirectInteractor.selectTarget.gameObject);
            }
        }
    }
}