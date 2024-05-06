using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace XRC.Assignments.Project.G07
{
    public class HelpMenuInput : MonoBehaviour
    {
        [SerializeField] private InputActionReference m_HelpMenuInputActionReference;
        private HelpMenu m_HelpMenu;
        
        // Start is called before the first frame update
        void Start()
        {
            m_HelpMenu = GetComponent<HelpMenu>();
            m_HelpMenuInputActionReference.action.performed += m_HelpMenu.EnableHelpMenu;
        }
        
        private void OnEnable()
        {
            m_HelpMenuInputActionReference.action.Enable();
        }

        private void OnDisable()
        {
            m_HelpMenuInputActionReference.action.Disable();
        }

        private void OnDestroy()
        {
            m_HelpMenuInputActionReference.action.performed -= m_HelpMenu.EnableHelpMenu;
        }
    }
}