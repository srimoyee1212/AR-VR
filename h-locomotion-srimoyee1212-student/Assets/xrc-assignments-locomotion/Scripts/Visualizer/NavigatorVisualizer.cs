using UnityEngine;

namespace XRC.Assignments.Locomotion.Visualizer
{
    /// <summary>
    /// Display instruction that leads the player through the scene.
    /// </summary>
    public class NavigatorVisualizer : MonoBehaviour
    {
        [SerializeField] private TextMesh m_InstructionText;
        [Multiline] [SerializeField] private string[] m_Instructions;

        /// <summary>
        /// Updates the instruction text.
        /// </summary>
        /// <param name="i">The number (index + 1) of the target instruction text.</param>
        public void UpdateVisualizer(int i)
        {
            m_InstructionText.text = m_Instructions[i - 1];
        }
    }
}