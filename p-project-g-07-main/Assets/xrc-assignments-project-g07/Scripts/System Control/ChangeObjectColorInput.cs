using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace XRC.Assignments.Project.G07
{
    public class ChangeObjectColorInput : MonoBehaviour
    {
        [SerializeField] private InputActionReference m_ChangeObjectColorInputActionReference;
        private ChangeObjectColor m_ChangeObjectColor;
        
        // Start is called before the first frame update
        void Start()
        {
            m_ChangeObjectColor = GetComponent<ChangeObjectColor>();
            m_ChangeObjectColorInputActionReference.action.performed += m_ChangeObjectColor.ChooseColorActionPerformed;
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnEnable()
        {
            m_ChangeObjectColorInputActionReference.action.Enable();
        }
    }
}