using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace XRC.Assignments.Project.G07
{
    public class CreateObject : MonoBehaviour
    {
        [SerializeField] private GameObject m_CubePrimitive;
        [SerializeField] private GameObject m_SpherePrimitive;
        [SerializeField] private GameObject m_CylinderPrimitive;
        [SerializeField] private GameObject m_CapsulePrimitive;
        [SerializeField] private Transform m_LeftControllerTransform;
        [SerializeField] private Material m_OpaqueMaterial;
        
        private PrimitiveShape m_SelectedPrimitive;
        private Vector3 m_LeftControllerPosition;
        private Vector3 m_RightControllerPosition;
        private GameObject m_CreatedObject;
        private Renderer m_CreatedObjectRenderer;
        private HapticFeedback m_HapticFeedback;
        private UndoRedo m_UndoRedo;

        private void Start()
        {
            m_HapticFeedback = GetComponent<HapticFeedback>();
            m_UndoRedo = GetComponent<UndoRedo>();
        }

        public void CreateObjectStarted(InputAction.CallbackContext context)
        {
            m_LeftControllerPosition = m_LeftControllerTransform.position;
            m_RightControllerPosition = context.ReadValue<Vector3>();
            Vector3 instantiatePosition = (m_LeftControllerPosition + m_RightControllerPosition) / 2;
            CreatePrimitiveShape(instantiatePosition);
            m_CreatedObjectRenderer = m_CreatedObject.GetComponent<Renderer>();
        }
        
        public void CreateObjectPerformed(InputAction.CallbackContext context)
        {
            m_LeftControllerPosition = m_LeftControllerTransform.position;
            m_RightControllerPosition = context.ReadValue<Vector3>();
            float controllerDistance = Vector3.Distance(m_LeftControllerPosition, m_RightControllerPosition);
            m_CreatedObject.transform.localScale = new Vector3(controllerDistance, controllerDistance, controllerDistance);
            m_HapticFeedback.TriggerHaptic();
        }
        
        public void CreateObjectCancelled(InputAction.CallbackContext context)
        {
            m_CreatedObjectRenderer.material = m_OpaqueMaterial;
            UndoStackItem undoStackItem = new UndoStackItem(m_CreatedObject, "Create Object");
            m_UndoRedo.RecordAction(undoStackItem);
        }

        public void CreatePrimitiveShape(Vector3 instantiatePosition)
        {
            switch (m_SelectedPrimitive)
            {
                case PrimitiveShape.Cube:
                    m_CreatedObject = Instantiate(m_CubePrimitive, instantiatePosition, Quaternion.identity);
                    break;
                case PrimitiveShape.Sphere:
                    m_CreatedObject = Instantiate(m_SpherePrimitive, instantiatePosition, Quaternion.identity);
                    break;
                case PrimitiveShape.Cylinder:
                    m_CreatedObject = Instantiate(m_CylinderPrimitive, instantiatePosition, Quaternion.identity);
                    break;
                case PrimitiveShape.Capsule:
                    m_CreatedObject = Instantiate(m_CapsulePrimitive, instantiatePosition, Quaternion.identity);
                    break;
                default:
                    m_CreatedObject = Instantiate(m_CubePrimitive, instantiatePosition, Quaternion.identity);
                    break;
            }
        }

        public void SetSelectedPrimitive(PrimitiveShape primitiveShape)
        {
            m_SelectedPrimitive = primitiveShape;
        }
    }
}
