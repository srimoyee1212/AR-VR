using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace XRC.Assignments.Project.G07
{
    public class ScalerScript : MonoBehaviour
    {
        public XRGrabInteractable[] grabInteractables; // Assign your cone grab interactables through the Unity Inspector
        public float scaleSpeed = 0.02f;

        private Vector3 initialScale;
        private XRGrabInteractable activeCone;
        
        // Start is called before the first frame update
        void Start()
        {
            initialScale = transform.localScale;

            foreach (var grabInteractable in grabInteractables)
            {
                grabInteractable.onSelectEntered.AddListener(OnConeGrabbed);
                grabInteractable.onSelectExited.AddListener(OnConeReleased);
            }
        }

        // Update is called once per frame
        void Update()
        {
            // Check if a cone is grabbed
            if (activeCone != null)
            {
                // Get the direction of the pull/push
                Vector3 pullDirection = activeCone.selectingInteractor.transform.forward;

                // Scale the cube based on the pull direction
                ScaleCube(pullDirection);
            }
        }
        
        private void OnConeGrabbed(XRBaseInteractor interactor)
        {
            // Get the XRGrabInteractable component of the grabbed cone
            activeCone = interactor.GetComponent<XRGrabInteractable>();
        }

        private void OnConeReleased(XRBaseInteractor interactor)
        {
            // Reset the active cone when released
            activeCone = null;
        }
        
        private void ScaleCube(Vector3 scaleDirection)
        {
            // Calculate the scale factor based on the pull direction
            float scaleFactor = Vector3.Dot(scaleDirection, Vector3.up) > 0 ? 1f : -1f;

            // Apply the scaling
            Vector3 newScale = initialScale + scaleDirection * scaleSpeed * scaleFactor;
            transform.localScale = newScale;
        }
    }
}
