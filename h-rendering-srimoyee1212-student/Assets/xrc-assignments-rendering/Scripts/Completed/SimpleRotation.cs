using UnityEngine;

namespace XRC.Assignments.Rendering
{
    public class SimpleRotation : MonoBehaviour
    {
        [SerializeField]
        private float m_AngleX, m_AngleY, m_AngleZ;
        

        // Update is called once per frame
        void Update()
        {
            transform.Rotate(m_AngleX * Time.deltaTime, m_AngleY * Time.deltaTime, m_AngleZ * Time.deltaTime, Space.World);
        }
    }
}