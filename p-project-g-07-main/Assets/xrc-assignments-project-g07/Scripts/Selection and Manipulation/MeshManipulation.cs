using UnityEngine;
using UnityEngine.InputSystem;

public class MeshManipulation : MonoBehaviour
{
    [SerializeField] private GameObject m_ErrorMessageObject;
    [SerializeField] private Transform m_Head;
    private const float k_SpawnDistance = 0.7f;

    private void DisplayErrorMessage()
    {
        m_ErrorMessageObject.SetActive(!m_ErrorMessageObject.activeSelf);
        m_ErrorMessageObject.transform.position = m_Head.position +
                                                     new Vector3(m_Head.forward.x, -1, m_Head.forward.z).normalized * k_SpawnDistance;
        m_ErrorMessageObject.transform.LookAt(new Vector3(m_Head.position.x, m_ErrorMessageObject.transform.position.y, m_Head.position.z));
        m_ErrorMessageObject.transform.forward *= -1;
    }
    
    
    private Transform m_TargetObjectTransform;
    private Vector3 m_TargetObjectInitialPosition;
    private Quaternion m_TargetObjectInitialRotation;
    
    void Start()
    {
        /*// Get the MeshFilter component from the cube game object
        meshFilter = cube.GetComponent<MeshFilter>();

        // Get the mesh from the MeshFilter
        Mesh mesh = meshFilter.mesh;

        // Create an array of XR Grab Interactables for each vertex in the mesh
        vertexInteractables = new XRGrabInteractable[mesh.vertices.Length];

        // Loop through each vertex in the mesh
        for (int i = 0; i < mesh.vertices.Length; i++)
        {
            // Create a new XR Grab Interactable for this vertex
            XRGrabInteractable interactable = new XRGrabInteractable();

            // Set the interactable's position to the vertex's position
            interactable.transform.position = mesh.vertices[i];

            // Add the interactable to the scene
            interactable.gameObject.AddComponent<XRGrabInteractable>();

            // Store the interactable in the array
            vertexInteractables[i] = interactable;
            
            // Set the parent of the interactable to the cube
            interactable.transform.parent = cube.transform;
            
            prevInteractablePosition[i] = interactable.transform.position;
        }*/
    }
    
    public void MeshManipulationPerformed(InputAction.CallbackContext context)
    {
        DisplayErrorMessage();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if any of the XR Grab Interactables are being grabbed

        /*counter = 0;
        foreach (XRGrabInteractable interactable in vertexInteractables)
        {
            // If the interactable is being grabbed, update the mesh
            if (interactable.isSelected)
            {
                // Get the current position of the interactable
                Vector3 currentPosition = interactable.transform.position;

                // Calculate the displacement between the current position and the original position
                Vector3 displacement = currentPosition - prevInteractablePosition[counter];

                // Apply the displacement to the mesh
                meshFilter.mesh.vertices[Array.IndexOf(vertexInteractables, interactable)] += displacement;
                prevInteractablePosition[counter] = currentPosition;
            }
            counter++;
        }*/
    }

    public void SetTargetObject(Transform targetObjectTransform, Vector3 targetObjectPosition, Quaternion targetObjectRotation)
    {
        m_TargetObjectTransform = targetObjectTransform;
        m_TargetObjectInitialPosition = targetObjectPosition;
        m_TargetObjectInitialRotation = targetObjectRotation;
    }
}