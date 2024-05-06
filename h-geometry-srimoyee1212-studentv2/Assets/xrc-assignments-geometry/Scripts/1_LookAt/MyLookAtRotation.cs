using UnityEngine;

namespace XRC.Assignments.Geometry
{/// <summary>
 /// This script can be attached to a game object. The game object's vertices will be transformed so the object appears to be oriented towards the referenced target transform.
 /// This script uses the <see cref="MyLookAt"/> class for calculating the transformed vertices.
 /// </summary>
    [RequireComponent(typeof(MeshFilter))]
    public class MyLookAtRotation : MonoBehaviour
    {
        [SerializeField]
        private Transform m_Target;

        // MeshFilter is used to access the vertices
        private MeshFilter m_MeshFilter;
        private Vector3[] m_OriginalVertices;
        private Vector3[] m_TransformedVertices;

        void Start()
        {
            m_MeshFilter = GetComponent<MeshFilter>();
            m_OriginalVertices = m_MeshFilter.mesh.vertices;
            m_TransformedVertices = new Vector3[m_OriginalVertices.Length];
        }

        void Update()
        {
            m_TransformedVertices = MyLookAt.LookAt(transform.position, m_Target.position, m_OriginalVertices);
            m_MeshFilter.mesh.vertices = m_TransformedVertices;
        }
    }
}