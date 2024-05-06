using UnityEngine;

namespace XRC.Assignments.Rendering
{
    /// <summary>
    /// A simple component representing a light for the ray tracer
    /// </summary>
    public class MyLight : MonoBehaviour
    {
        /// <summary>
        /// The color of the light
        /// </summary>
        [SerializeField]
        private Color m_Color = Color.white;
        
        /// <summary>
        /// The color of the light
        /// </summary>
        public Color color
        {
            get => m_Color;
            private set => m_Color = value;
        }
    }
}