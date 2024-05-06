using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XRC.Assignments.Rendering
{

    public class SimpleLine : MonoBehaviour
    {
        [SerializeField]
        private MyCamera m_MyCamera;

        private LineRenderer m_LineRenderer;
        // Start is called before the first frame update
        void Start()
        {
            m_LineRenderer = GetComponent<LineRenderer>();
            m_LineRenderer.positionCount = 2;
            m_LineRenderer.material.color = Color.red;
            m_LineRenderer.material.shader = Shader.Find("Unlit/Color");
            m_LineRenderer.startWidth = 0.02f;
            m_LineRenderer.endWidth = 0.02f;
        }

        // Update is called once per frame
        void Update()
        {
            // Vector2 rayPositions = m_LineRenderer.GetVisualizerData();
            // m_LineRenderer.SetPosition(0,rayPositions.x);

            var (origin, hitPoint, color) = m_MyCamera.GetVisualizerData();
            m_LineRenderer.SetPosition(0, origin);
            m_LineRenderer.SetPosition(1,hitPoint);
            m_LineRenderer.material.color = color;
        }
    }
}