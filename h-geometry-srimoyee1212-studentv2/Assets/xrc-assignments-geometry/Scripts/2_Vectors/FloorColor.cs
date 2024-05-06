using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XRC.Assignments.Geometry
{
    public class FloorColor : MonoBehaviour
    {
        [SerializeField]
        private Transform m_Target;
        private const float k_DistanceThreshold = 5.0f;
        private Renderer m_Renderer;

        void Start()
        {
            m_Renderer = GetComponent<Renderer>();
        }

        void Update()
        {
            Color color = CalculateColor(transform.position, m_Target.position, m_Target.forward, k_DistanceThreshold);
            m_Renderer.material.SetColor("_Color", color);
        }

        public static Color CalculateColor(Vector3 position, Vector3 targetPosition, Vector3 targetForward, float distanceThreshold = k_DistanceThreshold)
        {
            Color color = new Color(0.5f, 0.5f, 0.5f);
            
            // TODO - Modify the color value according to instructions
            // <solution>
            // Your code here
            float d = Vector3.Distance(position, targetPosition);
            float alpha = Mathf.Max((distanceThreshold - d )/ distanceThreshold, 0);
            Vector3 v = (position-targetPosition);
            v.y = targetForward.y;
            //v.y = 0;
            //v.z = targetForward.z;
            float dotProduct = Vector3.Dot(v.normalized, targetForward.normalized);
            float r,g,b;;
            r = 1-alpha;
            g = 0;
            b = alpha;
            
            if (dotProduct>=0.98f)
            {
                //r = 1-alpha;
                g = 1;
                //b = alpha;
            }
            color = new Color(r, g, b);
            
            
            
            
            // </solution>

            return color;
        }
    }
}