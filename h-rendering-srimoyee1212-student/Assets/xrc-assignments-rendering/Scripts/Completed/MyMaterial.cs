using UnityEngine;

namespace XRC.Assignments.Rendering
{
    /// <summary>
    /// Class for representing a material with diffuse and specular colors, as well as shading type
    /// </summary>
    public class MyMaterial : MonoBehaviour
    {
        /// <summary>
        /// Diffuse color for material
        /// </summary>
        [SerializeField]
        private Color m_DiffuseColor = Color.white;

        /// <summary>
        /// Specular color for material
        /// </summary>
        [SerializeField]
        private Color m_SpecularColor = Color.gray;

        /// <summary>
        /// Shading type for material
        /// </summary>
        [SerializeField]
        private MyShadingType m_MyShadingType = MyShadingType.Smooth;

        /// <summary>
        /// Specular color for material
        /// </summary>
        public Color specularColor
        {
            get => m_SpecularColor;
            set => m_SpecularColor = value;
        }

        /// <summary>
        /// Diffuse color for material
        /// </summary>
        public Color diffuseColor
        {
            get => m_DiffuseColor;
            set => m_DiffuseColor = value;
        }

        /// <summary>
        /// Shading type for material
        /// </summary>
        public MyShadingType myShadingType
        {
            get => m_MyShadingType;
            set => m_MyShadingType = value;
        }
    }
}