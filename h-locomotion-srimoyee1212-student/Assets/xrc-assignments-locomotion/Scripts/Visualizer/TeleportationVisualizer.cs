using System.Collections.Generic;
using UnityEngine;

namespace XRC.Assignments.Locomotion.Visualizer
{
    /// <summary>
    /// Locomotion visualizer that manages visualization for teleportation.
    /// </summary>
    public class TeleportationVisualizer : MonoBehaviour
    {
        private TeleportationController m_Controller;
        private LineRenderer m_LineRenderer;

        [Tooltip("The color of the ray when hit point is valid for teleportation")] [SerializeField]
        private Gradient m_ValidLineColor;

        [Tooltip("The color of the ray when hit point is invalid for teleportation")] [SerializeField]
        private Gradient m_InvalidLineColor;

        [Tooltip("The teleportation indicator")] [SerializeField]
        private GameObject m_TeleportationIndicator;

        private void Start()
        {
            m_LineRenderer = GetComponent<LineRenderer>();
            HideLine();
        }

        private void LateUpdate()
        {
            if (m_Controller.shootRay)
            {
                RenderLine(m_Controller.samplePoints, m_Controller.readyToTeleport,
                    m_Controller.targetPosition, m_Controller.targetDirection);
            }
            else HideLine();
        }

        /// <summary>
        /// Renders the teleportation ray with line renderer. 
        /// </summary>
        /// <param name="samplePoints">The sampled points along the ray. </param>
        /// <param name="valid">Whether the hit point is a valid teleportation position. </param>
        /// <param name="position">The position of the hit point. </param>
        /// <param name="direction">The facing direction at the hit point. </param>
        private void RenderLine(List<Vector3> samplePoints, bool valid, Vector3 position, Vector3 direction)
        {
            m_LineRenderer.positionCount = samplePoints.Count;
            m_LineRenderer.SetPositions(samplePoints.ToArray());
            if (valid)
            {
                m_LineRenderer.colorGradient = m_ValidLineColor;
                ShowIndicator(position, direction);
            }
            else
            {
                m_LineRenderer.colorGradient = m_InvalidLineColor;
                HideIndicator();
            }
        }

        /// <summary>
        /// Hide the teleportation ray. 
        /// </summary>
        private void HideLine()
        {
            m_LineRenderer.positionCount = 0;
            HideIndicator();
        }

        /// <summary>
        /// Show teleportation indicator and align it with the position and direction. 
        /// </summary>
        /// <param name="position">The position of the hit point. </param>
        /// <param name="direction">The facing direction at the hit point. </param>
        private void ShowIndicator(Vector3 position, Vector3 direction)
        {
            m_TeleportationIndicator.SetActive(true);
            m_TeleportationIndicator.transform.position = position;
            m_TeleportationIndicator.transform.rotation = Quaternion.LookRotation(direction);
        }

        /// <summary>
        /// Hide the teleportation indicator. 
        /// </summary>
        private void HideIndicator()
        {
            m_TeleportationIndicator.SetActive(false);
        }

        /// <summary>
        /// Set the initial parameters for this visualizer. 
        /// </summary>
        /// <param name="controller">The teleportation controller. </param>
        public void TeleportationVisualizerInit(TeleportationController controller)
        {
            m_Controller = controller;
        }
    }
}
