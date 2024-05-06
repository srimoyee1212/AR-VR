using UnityEngine;

namespace XRC.Assignments.Geometry
{
    /// <summary>
    /// Static class for calculating the look-at direction. External usage should be through the <see cref="LookAt"/> method.
    /// </summary>
    public static class MyLookAt
    {
        /// <summary>
        /// Method for calculating the look-at direction
        /// </summary>
        /// <param name="e">Position of the eye - see textbook for context</param>
        /// <param name="p">Target point - see textbook for context</param>
        /// <param name="originalVertices">Original vertices of the game object</param>
        /// <returns>Look-at direction</returns>
        public static Vector3[] LookAt(Vector3 e, Vector3 p, Vector3[] originalVertices)
        {
            Vector3 c = CalculateCentralLookingDirection(e, p);
            (Vector3 x, Vector3 y, Vector3 z) = CalculateColumnVectors(c, Vector3.up);
            Matrix4x4 rotationMatrix = CalculateMatrix(x, y, z);
            return CalculateTransformedVertices(rotationMatrix, originalVertices);
        }
        
        /// <summary>
        /// Calculate the central looking direction based the eye position and target point
        /// </summary>
        /// <param name="e">Eye position</param>
        /// <param name="p">Target point</param>
        /// <returns></returns>
        public static Vector3 CalculateCentralLookingDirection(Vector3 e, Vector3 p)
        {
            Vector3 c = Vector3.zero;

            // TODO - Implement method
            // <solution>
            // Your code here
            Vector3 direction = p - e;
            float magnitude = direction.magnitude;
            c = direction / magnitude;
            //c.Normalize();
            // </solution>

            return c;
        }

        /// <summary>
        /// Calculate the column vectors based on the central looking direction and the up vector
        /// </summary>
        /// <param name="c">Central looking direction</param>
        /// <param name="u">Up vector</param>
        /// <returns>Column vectors for the matrix</returns>
        public static (Vector3 x, Vector3 y, Vector3 z) CalculateColumnVectors(Vector3 c, Vector3 u)
        {
            Vector3 x = Vector3.zero;
            Vector3 y = Vector3.zero;
            Vector3 z = Vector3.zero;
            
            // TODO - Implement method
            // <solution>
            // Your code here
            z = c;
            x = Vector3.Cross(u, z).normalized;
            y = Vector3.Cross(z, x).normalized;
            // </solution>
            
            return (x, y, z);
        }

        /// <summary>
        /// Create the 4x4 matrix from the 3x3 column vectors
        /// </summary>
        /// <param name="x">Column vector x</param>
        /// <param name="y">Column vector y</param>
        /// <param name="z">Column vector z</param>
        /// <returns>The rotation matrix in a 4x4 format</returns>
        public static Matrix4x4 CalculateMatrix(Vector3 x, Vector3 y, Vector3 z)
        {
            // This is a 4x4 version of R_eye presented in SL-3.4
            Matrix4x4 rotationMatrix = Matrix4x4.zero;
            
            // TODO - Implement method
            // <solution>
            // Your code here
            rotationMatrix.SetColumn(0, new Vector4(x.x, x.y, x.z, 0));
            rotationMatrix.SetColumn(1, new Vector4(y.x, y.y, y.z, 0));
            rotationMatrix.SetColumn(2, new Vector4(z.x, z.y, z.z, 0));
            rotationMatrix.SetColumn(3, new Vector4(0, 0, 0, 1));
            // </solution>
            
            return rotationMatrix;
        }

        /// <summary>
        /// Transforms the original vertices with the matrix
        /// </summary>
        /// <param name="matrix">Transformation matrix</param>
        /// <param name="originalVertices">Original vertices for the game objects</param>
        /// <returns>Transformed vertices</returns>
        private static Vector3[] CalculateTransformedVertices(Matrix4x4 matrix, Vector3[] originalVertices)
        {
            Vector3[] transformedVertices = new Vector3[originalVertices.Length];

            // TODO - Implement method
            // Consider the the differences between MultiplyPoint3x4 and MultiplyPoint
            // <solution>
            // Your code here
            for (int i = 0; i < originalVertices.Length; i++)
            {
                transformedVertices[i] = matrix.MultiplyPoint3x4(originalVertices[i]);
            }
            // </solution>

            return transformedVertices;
        }

       
    }
}