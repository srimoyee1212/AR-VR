using UnityEngine;

namespace XRC.Assignments.Geometry
{
    public class MyQuaternionRotation : MonoBehaviour
    {
        // Note: You are only allowed to use MyQuaternion class, not Unity's Quaternion class

        [SerializeField]
        private AngleGenerator m_AngleGenerator;

        // Vertices
        private MeshFilter m_MeshFilter;
        private Vector3[] m_OriginalVertices;
        private Vector3[] m_TransformedVertices;


        private void Start()
        {
            m_MeshFilter = GetComponent<MeshFilter>();
            m_OriginalVertices = m_MeshFilter.mesh.vertices;
            m_TransformedVertices = new Vector3[m_OriginalVertices.Length];
        }

        void Update()
        {
            // Get the angle to be used
            float radAngle = Mathf.Deg2Rad * m_AngleGenerator.angle;

            // Calculate the quaternion and transform the vertices
            MyQuaternion myQuaternion = CalculateQuaternion(radAngle);
            m_TransformedVertices = TransformVertices(m_OriginalVertices, myQuaternion);

            // Update the mesh with transformed vertices
            m_MeshFilter.mesh.vertices = m_TransformedVertices;
        }


        private Vector3[] TransformVertices(Vector3[] originalVertices, MyQuaternion myQuaternion)
        {
            MyQuaternion myQuaternionPoint = new MyQuaternion();

            // TODO - Transform the vertices using the quaternion
            // <solution>
            // Your code here
            //m_TransformedVertices = new Vector3[originalVertices.Length];
            for (int i = 0; i < originalVertices.Length; i++)
            {
                float b = originalVertices[i].x;
                float c = originalVertices[i].y;
                float d = originalVertices[i].z;
                MyQuaternion q = new MyQuaternion();
                q.a = 0;
                q.b = b;
                q.c = c;
                q.d = d;

               

                MyQuaternion rq = myQuaternion * q * MyQuaternion.Inverse(myQuaternion);
                
                m_TransformedVertices[i] = new Vector3(rq.b, rq.c, rq.d);
            
            }
            
            // </solution>

            return m_TransformedVertices;
        }

        private MyQuaternion CalculateQuaternion(float angle)
        {
            MyQuaternion myQuaternionYaw = new MyQuaternion();
            MyQuaternion myQuaternionRoll = new MyQuaternion();
            MyQuaternion myQuaternionPitch = new MyQuaternion();

            MyQuaternion myResultQuaternion = new MyQuaternion();

            // TODO - Calculate the quaternions for yaw, pitch, and roll. Use the AngleAxis(...) method in MyQuaternion and then multiply the the quaternions.
            // Order of rotation (to match Unity's Quaternion.Euler): first roll, then pitch, and finally yaw.
            // <solution>
            // Your code here
            myQuaternionYaw.AngleAxis(angle, Vector3.up); //y axis
            myQuaternionRoll.AngleAxis(angle, Vector3.forward); //z axis
            myQuaternionPitch.AngleAxis(angle, Vector3.right); //x axis
            

            myResultQuaternion = myQuaternionYaw * myQuaternionPitch * myQuaternionRoll;
            
            // </solution>

            return myResultQuaternion;
        }
    }
}