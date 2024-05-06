using UnityEngine;

namespace XRC.Assignments.Geometry
{
    public class MyQuaternion
    {
        // Note: This class follows the the conventions from LaValle ch. 3:
        // q = (a, b, c, d), where is a is the scalar value, and b,c,d are the vector components

        private float m_A, m_B, m_C, m_D;

        public float a
        {
            get => m_A;
            set => m_A = value;
        }

        public float b
        {
            get => m_B;
            set => m_B = value;
        }

        public float c
        {
            get => m_C;
            set => m_C = value;
        }

        public float d
        {
            get => m_D;
            set => m_D = value;
        }

        /// <summary>
        /// Constructor that initializes with identity rotation
        /// </summary>
        public MyQuaternion()
        {
            m_A = 0;
            m_B = 0;
            m_C = 0;
            m_D = 0;
        }

        /// <summary>
        /// Calculate the inverse of a quaternion
        /// </summary>
        /// <param name="q">Input quaternion</param>
        /// <returns></returns>
        public static MyQuaternion Inverse(MyQuaternion q)
        {
            MyQuaternion result = new MyQuaternion();

            // TODO - Implement the method
            // <solution>
            // Your code here
            result.a = q.a/(q.a*q.a+q.b*q.b+q.c*q.c+q.d*q.d);
            result.b = -q.b/(q.a*q.a+q.b*q.b+q.c*q.c+q.d*q.d);
            result.c = -q.c/(q.a*q.a+q.b*q.b+q.c*q.c+q.d*q.d);
            result.d = -q.d/(q.a*q.a+q.b*q.b+q.c*q.c+q.d*q.d);
            // </solution>

            return result;
        }

        /// <summary>
        /// Set the quaternion components based on the received angle and axis
        /// </summary>
        /// <param name="angle">Angle in radians</param>
        /// <param name="axis">Axis</param>
        public void AngleAxis(float angle, Vector3 axis)
        {
            // TODO - Implement the method. Calculate and set the backing fields, m_A, m_B etc.
            // <solution>
            // Your code here
            a = Mathf.Cos(0.5f * angle);
            b = axis.x * Mathf.Sin(0.5f * angle);
            c = axis.y * Mathf.Sin(0.5f * angle);
            d = axis.z * Mathf.Sin(0.5f * angle);
            // </solution>
        }

        /// <summary>
        /// Quaternion multiplication
        /// </summary>
        /// <param name="q1">Quaternion 1</param>
        /// <param name="q2">Quaternion 2</param>
        /// <returns></returns>
        public static MyQuaternion operator *(MyQuaternion q1, MyQuaternion q2)
        {
            MyQuaternion result = new MyQuaternion();

            // TODO - Implement the method
            // <solution>
            // Your code here
            result.a = q1.a * q2.a - q1.b * q2.b - q1.c * q2.c - q1.d * q2.d;
            result.b = q1.a * q2.b + q1.b * q2.a + q1.c * q2.d - q1.d * q2.c;
            result.c = q1.a * q2.c - q1.b * q2.d + q1.c * q2.a + q1.d * q2.b;
            result.d = q1.a * q2.d + q1.b * q2.c - q1.c * q2.b + q1.d * q2.a;
            // </solution>

            return result;
        }
    }
}