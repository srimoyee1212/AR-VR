using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
//using Vector3 = System.Numerics.Vector3;

namespace XRC.Assignments.Project.G07
{
    public class SphereSelect : MonoBehaviour
    {
        [SerializeField] private GameObject m_DirectInteractor;
        [SerializeField] public float m_MinRadius;
        [SerializeField] public float m_MaxRadius;
        [SerializeField] public Vector3 offset;
        

        public XRDirectInteractor directInteractor;
        [SerializeField] public SphereCollider sphereCollider;
        private Renderer sphereRenderer;
        private float initialRadius;
        private float inputSensitivity = 1.0f;
        
        void Start()
        {
            directInteractor = m_DirectInteractor.GetComponent<XRDirectInteractor>();
            sphereCollider = m_DirectInteractor.GetComponent<SphereCollider>();
            sphereRenderer = m_DirectInteractor.GetComponentInChildren<Renderer>();
            offset = new Vector3(-0.05f, 0.0f, 0.0f);
            if (directInteractor == null)
            {
                Debug.LogError("Direct Interactor not found on the provided GameObject.");
                enabled = false;
            }

            if (sphereRenderer == null)
            {
                Debug.LogError("Renderer component not found on the provided GameObject or its children. Make sure to attach a Renderer component to the Direct Interactor or one of its child GameObjects.");
                enabled = false;
            }
        }

        void Update()
        {
            if (directInteractor.selectTarget)
            {
                Collider[] colliders = Physics.OverlapSphere(sphereCollider.transform.position, sphereCollider.radius);

                
                List<XRGrabInteractable> grabbableObjects = new List<XRGrabInteractable>();

                foreach (var collider in colliders)
                {
                    
                    XRGrabInteractable grabInteractable = collider.GetComponent<XRGrabInteractable>();
                    if (grabInteractable != null && !grabInteractable.isSelected)
                    {
                        grabbableObjects.Add(grabInteractable);
                    }
                }
                GrabObjects(grabbableObjects);
            }
            else
            {
                ReleaseObjects();
            }
        }
        
        void GrabObjects(List<XRGrabInteractable> grabbableObjects)
        {
            if (grabbableObjects.Count > 0)
            {
                foreach (var grabbableObject in grabbableObjects)
                {
                    
                    directInteractor.interactionManager.ForceSelect(directInteractor, grabbableObject);
                }
            }
        }
        
        void ReleaseObjects()
        {
            if (directInteractor.selectTarget)
            {
                directInteractor.interactionManager.ClearInteractorSelection(directInteractor);
            }
        }
        
        public void SphereSelectStarted(InputAction.CallbackContext obj)
        {
            initialRadius = sphereCollider.radius;
        }
        
        public void SphereSelectOngoing(InputAction.CallbackContext obj)
        {
            Vector2 thumbstickInput = obj.ReadValue<Vector2>();
            float deltaValue = thumbstickInput.x * inputSensitivity * Time.deltaTime;
            
            float newRadius = initialRadius + deltaValue;
            
            newRadius = Mathf.Clamp(newRadius, m_MinRadius, m_MaxRadius);
            sphereCollider.radius = newRadius;
        }
    }
}