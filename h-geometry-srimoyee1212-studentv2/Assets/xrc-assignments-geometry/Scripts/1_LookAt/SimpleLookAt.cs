using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XRC.Assignments.Geometry
{
    /// <summary>
    /// This script is rotates the game object towards the referenced target transform using the provided LookAt method by Unity
    /// </summary>
    public class SimpleLookAt : MonoBehaviour
    {
        [SerializeField]
        private Transform m_Target;

        void Update()
        {
            transform.LookAt(m_Target, Vector3.up);
        }
    }
}