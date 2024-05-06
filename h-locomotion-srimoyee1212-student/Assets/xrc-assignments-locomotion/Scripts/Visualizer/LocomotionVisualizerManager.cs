using UnityEngine;

namespace XRC.Assignments.Locomotion.Visualizer
{
    /// <summary>
    /// Locomotion visualizer that manages all visualizers.
    /// </summary>
    public class LocomotionVisualizerManager : MonoBehaviour
    {
        [Tooltip("The locomotion manager")] [SerializeField]
        private LocomotionManager m_Manager;

        [Tooltip("The teleportation visualizer prefab")] [SerializeField]
        private GameObject m_TeleportationVisualizer;

        [Tooltip("The mode visualizer prefab")] [SerializeField]
        private GameObject m_ModeVisualizer;

        [Tooltip("The player hand that controls movement")] [SerializeField]
        private GameObject m_MovementHand;

        [Tooltip("The player hand that controls rotation")] [SerializeField]
        private GameObject m_RotationHand;

        private void Awake()
        {
            GameObject mode = Instantiate(m_ModeVisualizer);
            mode.GetComponent<ModeVisualizer>().ModeVisualizerInit(m_Manager, m_MovementHand, m_RotationHand);
            GameObject teleportation = Instantiate(m_TeleportationVisualizer);
            teleportation.GetComponent<TeleportationVisualizer>().TeleportationVisualizerInit
                (m_Manager.GetComponent<TeleportationController>());
        }
    }
}