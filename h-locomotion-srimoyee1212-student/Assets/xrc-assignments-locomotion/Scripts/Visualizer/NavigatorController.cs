using UnityEngine;

namespace XRC.Assignments.Locomotion.Visualizer
{
    /// <summary>
    /// Controls a navigator that leads the player through the scene.
    /// </summary>
    public class NavigatorController : MonoBehaviour
    {
        [SerializeField] private NavigatorVisualizer m_NavigatorVisualizer;
        [SerializeField] private GameObject m_NavigatorTarget;
        [SerializeField] private Transform[] m_NavigatorLocations;

        [SerializeField] private GameObject m_PlayerHand;
        private Camera m_PlayerCamera;

        private int _locationIndex;

        private void Start()
        {
            m_PlayerCamera = Camera.main;
            // attach visualizer to hand
            m_NavigatorVisualizer.transform.position = m_PlayerHand.transform.position;
            m_NavigatorVisualizer.transform.rotation = m_PlayerHand.transform.rotation;
            m_NavigatorVisualizer.transform.parent = m_PlayerHand.transform;
            // move indicator to first location
            NextLocation();
        }

        private void Update()
        {
            if (PlayerIsClose())
            {
                NextLocation();
            }
        }

        /// <summary>
        /// Check if the player is close to the navigator target.
        /// </summary>
        /// <returns>Returns true if player is within threshold, false otherwise. </returns>
        private bool PlayerIsClose()
        {
            return (Vector3.Distance(m_PlayerCamera.transform.position, m_NavigatorTarget.transform.position) < 2);
        }

        /// <summary>
        /// Updates the location index to that of the next location.
        /// </summary>
        private void NextLocation()
        {
            if (m_NavigatorLocations.Length > _locationIndex + 1)
            {
                _locationIndex++;
                GoToLocation(_locationIndex);
            }
        }

        /// <summary>
        /// Move the navigator target to location and update visualizer text.
        /// </summary>
        /// <param name="i">The index of the target navigator location.</param>
        private void GoToLocation(int i)
        {
            // move target to location
            m_NavigatorTarget.transform.position = m_NavigatorLocations[i].position;
            // update indicator
            m_NavigatorVisualizer.UpdateVisualizer(i);
        }
    }
}
