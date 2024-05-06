using UnityEngine;

namespace XRC.Assignments.Project.G07
{
    public class GrabMoveFeedback : MonoBehaviour
    {
        [SerializeField] private GameObject m_PivotSphere;
        [SerializeField] private Color m_PivotColor;
        [SerializeField] private Color m_LineColor;

        private LineRenderer m_LineRenderer;
        
        // Start is called before the first frame update
        void Start()
        {
            m_PivotSphere.GetComponent<Renderer>().material.color = m_PivotColor;
            // Create LineRenderer component
            m_LineRenderer = gameObject.AddComponent<LineRenderer>();
            m_LineRenderer.positionCount = 2;
            m_LineRenderer.startWidth = 0.02f;
            m_LineRenderer.endWidth = 0.02f;
            m_LineRenderer.material.color = m_LineColor;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void DrawSphere(Vector3 midPoint)
        {
            m_PivotSphere.SetActive(true);
            m_PivotSphere.transform.position = midPoint;
        }

        public void DisableSphere()
        {
            m_PivotSphere.SetActive(false);
        }
        
        public void DrawLine(Vector3 startPoint, Vector3 endPoint)
        {
            m_LineRenderer.SetPosition(0, startPoint);
            m_LineRenderer.SetPosition(1, endPoint);
        }
        
        public void DisableLine()
        {
            m_LineRenderer.SetPosition(0, Vector3.zero);
            m_LineRenderer.SetPosition(1, Vector3.zero);
        }
    }
}
