using UnityEngine;

namespace XRC.Assignments.Geometry
{
    public class MyMatrixRotation : MonoBehaviour
    {
        [SerializeField]
        private AngleGenerator m_AngleGenerator;

        private MeshFilter m_MeshFilter;
        private Vector3[] m_OriginalVertices;
        private Vector3[] m_TransformedVertices;

        private void Start()
        {
            // Get the original vertices from the MeshFilter
            m_MeshFilter = GetComponent<MeshFilter>();
            m_OriginalVertices = m_MeshFilter.mesh.vertices;
            m_TransformedVertices = new Vector3[m_OriginalVertices.Length];
        }

        void Update()
        {
            float angle = Mathf.Deg2Rad * m_AngleGenerator.angle;

            MyMatrix m = CalculateRotationMatrix(angle);
            m_TransformedVertices = TransformVertices(m, m_OriginalVertices);

            m_MeshFilter.mesh.vertices = m_TransformedVertices;
        }

        private Vector3[] TransformVertices(MyMatrix rotationMatrix, Vector3[] originalVertices)
        {
            Vector3[] transformedVertices = new Vector3[originalVertices.Length];
            
            // TODO - Transform the vertices with the final rotation matrix
            // <solution>
            // Your code here
            for (int v = 0; v < originalVertices.Length; v++)
            {
                Vector3 temp = originalVertices[v];

                float p = (rotationMatrix.GetValue(0, 0) * temp.x + rotationMatrix.GetValue(0, 1) * temp.y +
                           rotationMatrix.GetValue(0, 2) * temp.z);
                float q = (rotationMatrix.GetValue(1, 0) * temp.x + rotationMatrix.GetValue(1, 1) * temp.y +
                           rotationMatrix.GetValue(1, 2) * temp.z);
                float r = (rotationMatrix.GetValue(2, 0) * temp.x + rotationMatrix.GetValue(2, 1) * temp.y +
                           rotationMatrix.GetValue(2, 2) * temp.z);

                Vector3 vertex = new Vector3(p, q, r);
                transformedVertices[v] = vertex;
            }
            
            // </solution>

            return transformedVertices;
        }

        /// <summary>
        /// Calculate the combined rotation matrix
        /// </summary>
        /// <param name="angle">Angle</param>
        /// <returns></returns>
        private static MyMatrix CalculateRotationMatrix(float angle)
        {
            MyMatrix resultMatrix = new MyMatrix(3, 3);
            
            // TODO - Get the rotation matrices and multiply them accordingly
            // <solution>
            // Your code here
            MyMatrix yaw = MyMatrix.GetRotationMatrix(angle, MyMatrix.RotationType.Yaw);
            MyMatrix pitch = MyMatrix.GetRotationMatrix(angle, MyMatrix.RotationType.Pitch);
            MyMatrix roll = MyMatrix.GetRotationMatrix(angle, MyMatrix.RotationType.Roll);
            resultMatrix = yaw * pitch * roll;
            // </solution>
            return resultMatrix;
        }
    }
}