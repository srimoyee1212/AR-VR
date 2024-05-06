using UnityEngine;

namespace XRC.Assignments.Interaction
{
    /// <summary>
    /// The interactable that handles grabbing.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    public class GrabInteractable : MonoBehaviour
    {
        [Tooltip("Where this grabbable should align with the hand when grabbed, no auto alignment if left blank.")]
        [SerializeField]
        private Transform m_AttachTransform;

        [Tooltip("Whether this grabbable uses gravity. Will overwrite the use gravity and is kinematic in Rigidbody.")]
        [SerializeField]
        private bool m_UseGravity;

        private Rigidbody m_Rigidbody;

        /// <summary>
        /// The user assigned attach transform. The transform of this object if null.
        /// </summary>
        public Transform attachTransform()
        {
            if (m_AttachTransform == null) return transform;
            return m_AttachTransform;
        }

        /// <summary>
        /// The state of this interactable. Whether it is hovered by interactor.
        /// </summary>
        public bool isHovered { get; set; }

        /// <summary>
        /// The state of this interactable. Whether it is grabbed by interactor.
        /// </summary>
        public bool isGrabbed { get; set; }

        // Original Value
        private Transform m_OriginalParentTransform;

        // Visualization properties
        private Material m_OriginalMaterial;
        private Material m_HoverMaterial;
        private Material m_GrabbedMaterial;

        private void Start()
        {
            // Change gravity properties
            m_Rigidbody = GetComponent<Rigidbody>();
            m_Rigidbody.useGravity = m_UseGravity;
            m_Rigidbody.isKinematic = !m_UseGravity;
            // Gather original values
            m_OriginalParentTransform = transform.parent;
            m_OriginalMaterial = GetComponent<MeshRenderer>().material;
            // Initialize materials
            m_HoverMaterial = new Material(Shader.Find("Standard"));
            m_HoverMaterial.color = new Color(0.9f, 0.6f, 0.6f);
            m_GrabbedMaterial = new Material(Shader.Find("Standard"));
            m_GrabbedMaterial.color = new Color(0.6f, 0.9f, 0.6f);
        }

        /// <summary>
        /// Updates the state and visual of the object when it is hovered by an interactor.
        /// </summary>
        public void HoverEnter()
        {
            isHovered = true;
            GetComponent<MeshRenderer>().material = m_HoverMaterial;
        }

        /// <summary>
        /// Updates the state and visual of the object when the hover from an interactor has just stopped.
        /// </summary>
        public void HoverExit()
        {
            isHovered = false;
            if (!isGrabbed)
            {
                GetComponent<MeshRenderer>().material = m_OriginalMaterial;
            }
        }

        /// <summary>
        /// Handles the grabbing behaviors. Attaches to the grabbing interactor,
        /// changes rigidbody properties, and updates state and visual accordingly. 
        /// </summary>
        /// <param name="parentTransform">The transform of the interactor, the new parent.</param>
        public void Grab(Transform parentTransform)
        {
            isGrabbed = true;
            if (m_AttachTransform == null)
            {
                // No valid attach transform, use the interactor's transform instead
                Grab(transform, parentTransform);
            }
            else
            {
                GrabAttachTransform(transform, parentTransform, m_AttachTransform);
            }

            m_Rigidbody.useGravity = false;
            m_Rigidbody.isKinematic = true;
            GetComponent<MeshRenderer>().material = m_GrabbedMaterial;
        }
        
        /// <summary>
        /// Attaches <paramref name="thisTransform"> to the <paramref name="parentTransform">
        /// by setting its parent to <paramref name="parentTransform">
        /// </summary>
        /// <remarks>
        /// After implemented, you will be able to grab objects without attach transfrom with the grip button
        /// using both near and far interaction methods.
        /// </remarks>
        /// <param name="thisTransform">The transform of the this interactable.</param>
        /// <param name="parentTransform">The transform of the interactor that is grabbing this interactable.</param>
        public void Grab(Transform thisTransform, Transform parentTransform)
        {
            
            // TODO - Implement method
            // <solution>
            // Your code here
            thisTransform.parent = parentTransform;
            // </solution>
            
        }
        
        /// <summary>
        /// Updates the rotation and position of <paramref name="thisTransform"> such that
        /// the <paramref name="attachTransform"> is aligned with the <paramref name="parentTransform">.
        /// Then attaches <paramref name="thisTransform"> to the <paramref name="parentTransform">.
        /// </summary>
        /// <remarks>
        /// After implemented, you will be able to grab objects with attach transfrom using the grip button
        /// using both near and far interaction methods.
        /// </remarks>
        /// <param name="thisTransform">The transform of the this interactable.</param>
        /// <param name="parentTransform">The transform of the interactor that is grabbing this interactable.</param>
        /// <param name="attachTransform">The transform of the attach point assigned by the user.</param>
        public void GrabAttachTransform(Transform thisTransform, Transform parentTransform, Transform attachTransform)
        {
            
            // TODO - Implement method
            // <solution>
            // Your code here
            Quaternion relativeRotation = Quaternion.Inverse(attachTransform.rotation) * thisTransform.rotation;
            thisTransform.rotation = parentTransform.rotation * relativeRotation;
            Vector3 relativePosition = thisTransform.position - attachTransform.position;
            thisTransform.position = parentTransform.position + relativePosition;
            

            thisTransform.parent = parentTransform;
            // </solution>
            // </solution>
            
        }

        /// <summary>
        /// Handles the releasing behaviors. Attaches to the original parent transform,
        /// changes rigidbody properties, and updates state accordingly. 
        /// </summary>
        public void Release()
        {
            isGrabbed = false;
            ReleaseResetParent(transform, m_OriginalParentTransform);
            m_Rigidbody.useGravity = m_UseGravity;
            m_Rigidbody.isKinematic = !m_UseGravity;
            HoverExit();
        }
        
        /// <summary>
        /// Attaches <paramref name="thisTransform"> back to the <paramref name="originalParentTransform">.
        /// </summary>
        /// <remarks>
        /// After implemented, the grabbed object will be released and no longer follows your hands
        /// when you release the grip button.
        /// </remarks>
        /// <param name="thisTransform">The transform of the this interactable.</param>
        /// <param name="originalParentTransform">The transform of the original parent of the interactable
        /// before it was grabbed.</param>
        public void ReleaseResetParent(Transform thisTransform, Transform originalParentTransform)
        {
            
            // TODO - Implement method
            // <solution>
            // Your code here
            thisTransform.parent = originalParentTransform;
            // </solution>
            
        }
    }
}
