using UnityEngine;

namespace XRC.Assignments.Locomotion
{
    /// <summary>
    /// Controls the height and center location of a <see cref="CharacterController"/>.
    /// </summary>
    public class CharacterControllerDriver : MonoBehaviour
    {
        private CharacterController m_CharacterController;
        [SerializeField] private Transform m_PlayerHeadTransform;

        private void Start()
        {
            m_CharacterController = GetComponent<CharacterController>();
            UpdatePositionAndHeight();
        }

        private void Update()
        {
            UpdatePositionAndHeight();
        }

        /// <summary>
        /// Updates the position and height of character controller according to
        /// the relative transform of player head.
        /// </summary>
        private void UpdatePositionAndHeight()
        {
            m_CharacterController.height = m_PlayerHeadTransform.localPosition.y;
            m_CharacterController.center = new Vector3(m_PlayerHeadTransform.localPosition.x,
                Mathf.Clamp(m_CharacterController.height / 2, m_CharacterController.radius,
                    m_CharacterController.height / 2), m_PlayerHeadTransform.localPosition.z);
        }
    }
}
