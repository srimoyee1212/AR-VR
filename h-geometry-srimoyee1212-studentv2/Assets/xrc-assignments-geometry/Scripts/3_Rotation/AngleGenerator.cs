using System;
using UnityEngine;

namespace XRC.Assignments.Geometry
{
    public class AngleGenerator : MonoBehaviour
    {
        private float m_Angle = 0;
        
        private float m_AngleSpeed = 100;

        public float angle => m_Angle;

        private void Update()
        {
            m_Angle += m_AngleSpeed * Time.deltaTime;
            m_Angle %= 360;
        }
    }
}