using UnityEngine;
using UnityEngine.InputSystem;

namespace XRC.Assignments.Project.G07
{
    public class HelpMenu : MonoBehaviour
    {
        [SerializeField] private GameObject m_HelpMenuScreen;
        [SerializeField] private GameObject m_HelpMenuCanvas;
        [SerializeField] private GameObject m_RayInteractor;
        [SerializeField] private InputActionReference m_CreateObjectInputActionReference;
        
        public void EnableHelpMenu(InputAction.CallbackContext obj)
        {
            m_HelpMenuScreen.SetActive(!m_HelpMenuScreen.activeSelf);
            m_HelpMenuCanvas.SetActive(!m_HelpMenuCanvas.activeSelf);
            m_RayInteractor.SetActive(!m_RayInteractor.activeSelf);
            if (m_CreateObjectInputActionReference.action.enabled)
            {
                m_CreateObjectInputActionReference.action.Disable();
            }
            else
            {
                m_CreateObjectInputActionReference.action.Enable();
            }
        }
    }
}