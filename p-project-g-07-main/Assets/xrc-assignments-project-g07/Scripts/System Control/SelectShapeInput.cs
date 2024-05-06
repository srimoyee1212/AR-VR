using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace XRC.Assignments.Project.G07
{
    public class SelectShapeInput : MonoBehaviour
    {
        [SerializeField] private InputActionReference m_SelectShapeInputActionReference;
        private SelectShape m_SelectShape;
        
        // Start is called before the first frame update
        void Start()
        {
            m_SelectShape = GetComponent<SelectShape>();
            m_SelectShapeInputActionReference.action.performed += m_SelectShape.EnableShapeSelection;
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnEnable()
        {
            m_SelectShapeInputActionReference.action.Enable();
        }
    }
}
