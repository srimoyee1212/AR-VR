using UnityEngine;

namespace XRC.Assignments.Rendering
{
    public class SimpleRotationOscillation : MonoBehaviour
    {
        [SerializeField]
        private float m_AngleX;

        [SerializeField]
        private float m_AngleY;

        [SerializeField]
        private float m_AngleZ;

        void Update()
        {
            Vector3 rotation = new Vector3(m_AngleX, m_AngleY, m_AngleZ) * Mathf.Cos(Time.time * 0.1f);
            transform.localEulerAngles = (rotation);
        }
    }
}