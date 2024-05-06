using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

namespace XRC.Assignments.Project.G07
{
    public class ScaleObjectPerAxisInput : MonoBehaviour
    {
        [SerializeField] private InputActionReference m_ScaleObjectPerAxisInputActionReference;
        
        private ScaleObjectPerAxis m_ScaleObjectPerAxis;
        
        // Start is called before the first frame update
        void Start()
        {
            m_ScaleObjectPerAxis = GetComponent<ScaleObjectPerAxis>();
            m_ScaleObjectPerAxisInputActionReference.action.performed += m_ScaleObjectPerAxis.ScaleObjectPerAxisActionPerformed;
        }

        private void OnEnable()
        {
            m_ScaleObjectPerAxisInputActionReference.action.Enable();
        }
        
        private void OnDisable()
        {
            m_ScaleObjectPerAxisInputActionReference.action.Disable();
        }
    }
}