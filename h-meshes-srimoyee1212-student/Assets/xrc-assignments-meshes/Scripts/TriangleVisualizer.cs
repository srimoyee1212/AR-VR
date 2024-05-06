using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace XRC.Assignments.Meshes
{
    /// <summary>
    /// This script visualizes the mesh of its game object by drawing lines between each triangle vertex, using a line renderer
    /// </summary>
    [RequireComponent(typeof(MeshFilter))]
    public class TriangleVisualizer : MonoBehaviour
    {
        private List<LineRenderer> m_LineRenderers;
        private Mesh m_Mesh;

        void Start()
        {
            m_Mesh = gameObject.GetComponent<MeshFilter>().mesh;
            m_LineRenderers = new List<LineRenderer>();
        }

        void LateUpdate()
        {
            // By doing the following in Update(), changes to the mesh during runtime are represented by the visualizer
            VisualizeTriangles(m_Mesh.vertices, m_Mesh.triangles);
        }

        /// <summary>
        /// This method is called by Update() every frame.
        /// The method calls SetupLineRenderer() and SetLineRendererPositions()
        /// </summary>
        /// <param name="vertices">Vertices</param>
        /// <param name="triangles">Triangle indices</param>
        private void VisualizeTriangles(Vector3[] vertices, int[] triangles)
        {
            // TODO - Implement method according to summary and instructions
            // <solution>]
            if (triangles.Length % 3 != 0)
            {
                return;
            }

            int count = triangles.Length / 3;

            while (m_LineRenderers.Count < count)
            {
                SetupLineRenderer();
            }

            for (int i = 0; i < count; i++)
            {
                Vector3 vertex1 = vertices[triangles[i * 3]];
                Vector3 vertex2 = vertices[triangles[i * 3 + 1]];
                Vector3 vertex3 = vertices[triangles[i * 3 + 2]];
                
                SetLineRendererPositions(vertex1, vertex2, vertex3, i);
            }

            // Your code here
            // </solution>
        }

        /// <summary>
        /// Instantiates a new game object and adds a LineRenderer component to it.
        /// The new game object is set as a child of the current game object.
        /// Each line renderer is named "TriangleLineRenderer".
        /// The LineRenderer has width 0.02 and the appropriate number of positions to draw a closed triangle
        /// The line renderer component is added to the m_LineRenderer list.
        /// </summary>
        private void SetupLineRenderer()
        {
            // TODO - Implement method according to summary and instructions
            // <solution>
            // Your code here
            GameObject triangleRenderer = new GameObject("TriangleLineRenderer");
            triangleRenderer.transform.parent = transform;
            LineRenderer renderer = triangleRenderer.AddComponent<LineRenderer>();
            Material lineMaterial = new Material(Shader.Find("Sprites/Default"));
            lineMaterial.color=Color.red;
            renderer.material = lineMaterial;
            renderer.startWidth = renderer.endWidth = 0.02f;
            renderer.transform.SetParent(transform);
            m_LineRenderers.Add(renderer);
            // </solution>
        }

        /// <summary>
        /// This method sets the positions for the line renderer used to visualize the triangle
        /// </summary>
        /// <param name="vertex1">First vertex</param>
        /// <param name="vertex2">Second vertex</param>
        /// <param name="vertex3">Third vertex</param>
        /// <param name="lineRendererIndex">The index for the line renderer in the list of line renderers</param>
        private void SetLineRendererPositions(Vector3 vertex1, Vector3 vertex2, Vector3 vertex3, int lineRendererIndex)
        {
            // TODO - Implement method according to summary and instructions
            // <solution>
            // Your code here
            LineRenderer renderer = m_LineRenderers[lineRendererIndex];
            Vector3[] v = new Vector3[] { vertex1, vertex2, vertex3, vertex1 };

            for (int i = 0; i < v.Length; i++)
            {
                v[i] = transform.TransformPoint(v[i]);
            }

            renderer.positionCount = v.Length;
            renderer.SetPositions(v);
            // </solution>
        }
    }
}