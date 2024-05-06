using UnityEngine;
using UnityEngine.InputSystem;

namespace XRC.Assignments.Locomotion
{
    /// <summary>
    /// Assigns its tracked position and rotation to a tracked object.
    /// </summary>
    public class TrackingInput : MonoBehaviour
    {
        [Tooltip("Tracked object")] [SerializeField]
        private GameObject m_TrackedObject;

        [SerializeField] private GameObject m_PlayerRig;

        private void OnEnable()
        {
            InputSystem.onAfterUpdate += UpdateCallback;
        }

        private void OnDisable()
        {
            InputSystem.onAfterUpdate -= UpdateCallback;
        }

        /// <summary>
        /// Calculates tracked transform and assigns it to tracked object.
        /// Called on after update.
        /// </summary>
        private void UpdateCallback()
        {
            Matrix4x4 trackingTransform = Matrix4x4.TRS(transform.localPosition, transform.localRotation,
                transform.localScale);
            Matrix4x4 rigTransform = m_PlayerRig.transform.localToWorldMatrix;
            Matrix4x4 newTransformMatrix =
                TrackedWorldTransform(trackingTransform.inverse, rigTransform.inverse);
            m_TrackedObject.transform.rotation = newTransformMatrix.rotation;
            m_TrackedObject.transform.position = newTransformMatrix.GetPosition();
        }
        
        /// <summary>
        /// Calculates the world transform of a tracked object based on the transform of 
        /// the player rig and the tracking input.
        /// </summary>
        /// <remarks>
        /// After implemented, the transform of your controllers in VR will follow
        /// that of your actual hand, and it will overlap with the transparent controllers
        /// in the default scene.
        /// </remarks>
        /// <param name="tTrack">The transform input from the tracking system.</param>
        /// <param name="tCart">The transform of the player rig, or player cart (Textbook SL-10.2).</param>
        /// <returns>Returns the world transform of the tracked object, in this case, the player hand.</returns>
        public Matrix4x4 TrackedWorldTransform(Matrix4x4 tTrack, Matrix4x4 tCart)
        {
            Matrix4x4 tHand = new Matrix4x4();
            tHand = Matrix4x4.identity;
            
            // TODO - Implement method
            // <solution>
            // Your code here
            Matrix4x4 tTrackInverse = tTrack.inverse;
            Matrix4x4 tCartInverse = tCart.inverse;
            tHand = tCartInverse * tTrackInverse;
            // </solution>
            
            return tHand;
        }
    }
}
