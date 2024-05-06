using UnityEngine;

namespace XRC.Assignments.Locomotion.Visualizer
{
    /// <summary>
    /// Locomotion visualizer that manages the texts to represent the active locomotion techniques.
    /// </summary>
    public class ModeVisualizer : MonoBehaviour
    {
        private GameObject m_MovementHand;
        [SerializeField] private TextMesh m_MovementText;
        private GameObject m_RotationHand;
        [SerializeField] private TextMesh m_RotationText;
        private LocomotionManager m_Manager;

        private void Start()
        {
            // Attaches the movement and rotation text to corresponding hands.
            transform.rotation = m_MovementHand.transform.rotation * transform.rotation;
            m_MovementText.transform.position += m_MovementHand.transform.position;
            m_RotationText.transform.position += m_RotationHand.transform.position;
            m_MovementText.transform.parent = m_MovementHand.transform;
            m_RotationText.transform.parent = m_RotationHand.transform;
        }

        private void Update()
        {
            if (m_Manager.modeChanged)
            {
                UpdateMovementText();
                UpdateRotationText();
                m_Manager.modeChanged = false;
            }
        }

        /// <summary>
        /// Changes the movement text to reflect the current movement technique.
        /// </summary>
        private void UpdateMovementText()
        {
            switch (m_Manager.movementMode)
            {
                case MovementMode.None:
                    m_MovementText.text = "None";
                    break;
                case MovementMode.Teleportation:
                    m_MovementText.text = "Teleportation";
                    break;
                case MovementMode.ContinuousMovement:
                    m_MovementText.text = "Continuous Movement";
                    break;
            }
        }

        /// <summary>
        /// Changes the rotation text to reflect the current rotation technique.
        /// </summary>
        private void UpdateRotationText()
        {
            switch (m_Manager.rotationMode)
            {
                case RotationMode.None:
                    m_RotationText.text = "None";
                    break;
                case RotationMode.ContinuousTurn:
                    m_RotationText.text = "Continuous Turn";
                    break;
                case RotationMode.SnapTurn:
                    m_RotationText.text = "Snap Turn";
                    break;
            }
        }

        /// <summary>
        /// Initializes this mode visualizer with parameters.
        /// </summary>
        /// <param name="manager">The locomotion manager.</param>
        /// <param name="movementHand">The player hand that controls movement.</param>
        /// <param name="rotationHand">The player hand that controls rotation.</param>
        public void ModeVisualizerInit(LocomotionManager manager, GameObject movementHand, GameObject rotationHand)
        {
            m_Manager = manager;
            m_MovementHand = movementHand;
            m_RotationHand = rotationHand;
        }
    }
}