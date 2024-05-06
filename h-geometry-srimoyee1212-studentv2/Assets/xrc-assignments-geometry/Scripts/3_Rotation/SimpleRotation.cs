using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XRC.Assignments.Geometry
{
    public class SimpleRotation : MonoBehaviour
    {
        [SerializeField]
        private AngleGenerator m_AngleGenerator;

        void Update()
        {
            Quaternion q = Quaternion.Euler(m_AngleGenerator.angle, m_AngleGenerator.angle, m_AngleGenerator.angle);
            transform.rotation = q;
        }
    }
}