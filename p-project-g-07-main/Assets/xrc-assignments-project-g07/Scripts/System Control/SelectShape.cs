using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace XRC.Assignments.Project.G07
{
    public class SelectShape : MonoBehaviour
    {
        [SerializeField] private GameObject m_ChooseShapeCanvas;
        [SerializeField] private GameObject m_RayInteractor;
        [SerializeField] private Button m_CubeButton;
        [SerializeField] private Button m_SphereButton;
        [SerializeField] private Button m_CapsuleButton;
        [SerializeField] private Button m_CylinderButton;
        [SerializeField] private InputActionReference m_CreateObjectInputActionReference;
        
        private Action<PrimitiveShape> m_PrimitiveSelected;
        private CreateObject m_CreateObject;
        
        // Start is called before the first frame update
        void Start()
        {
            m_CreateObject = GetComponent<CreateObject>();
            m_PrimitiveSelected += m_CreateObject.SetSelectedPrimitive;
            m_CubeButton.onClick.AddListener((() => m_PrimitiveSelected?.Invoke(PrimitiveShape.Cube)));
            m_SphereButton.onClick.AddListener((() => m_PrimitiveSelected?.Invoke(PrimitiveShape.Sphere)));
            m_CapsuleButton.onClick.AddListener((() => m_PrimitiveSelected?.Invoke(PrimitiveShape.Capsule)));
            m_CylinderButton.onClick.AddListener((() => m_PrimitiveSelected?.Invoke(PrimitiveShape.Cylinder)));
        }

        public void EnableShapeSelection(InputAction.CallbackContext obj)
        {
            if (!m_ChooseShapeCanvas.activeSelf)
            {
                m_CreateObjectInputActionReference.action.Disable();
            }
            else
            {
                m_CreateObjectInputActionReference.action.Enable();
            }
            
            m_ChooseShapeCanvas.SetActive(!m_ChooseShapeCanvas.activeSelf);
            m_RayInteractor.SetActive(!m_RayInteractor.activeSelf);
        }
    }
}
