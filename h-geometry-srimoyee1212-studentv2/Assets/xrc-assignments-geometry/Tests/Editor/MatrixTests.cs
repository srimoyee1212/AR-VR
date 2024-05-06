using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools.Utils;
using Vector3 = UnityEngine.Vector3;
using XRC.Utilities.TestUtilities;

namespace XRC.Assignments.Geometry.Tests
{
    public class MatrixTests : XRCBaseTest
    {
        [SetUp]
        public void Init()
        {
             // Arrange
        }
        
        [Test]
        public void MatrixMultiplicationIsCorrect()
        {
            // Arrange
            float[] a = new float[] { 1, 1, 1, 1 };
            float[] b = new float[] { 1, 2, 3, 4 };
            MyMatrix matrixA = new MyMatrix(2, 2);
            MyMatrix matrixB = new MyMatrix(2, 2);
            matrixA.SetValues(a);
            matrixB.SetValues(b);

            // Act
            MyMatrix matrixC = matrixA * matrixB;
            float[] d = new float[] { 4, 6, 4, 6 };

            // Assert
            CollectionAssert.AreEqual(d, matrixC.GetValues());
            
        }

        [Test]
        public void YawColumnVectorsAreOrthonormal()
        {
            // Act
            MyMatrix matrix = MyMatrix.GetRotationMatrix(Mathf.PI / 2.0f, MyMatrix.RotationType.Yaw);
            
            // Assert
            OrthoNormalAssert(matrix);
        }
        
        [Test]
        public void PitchColumnVectorsAreOrthonormal()
        {
            // Act
            MyMatrix matrix = MyMatrix.GetRotationMatrix(Mathf.PI / 2.0f, MyMatrix.RotationType.Pitch);
            
            // Assert
            OrthoNormalAssert(matrix);
            
        }
        
        [Test]
        public void RollColumnVectorsAreOrthonormal()
        {
            // Act
            MyMatrix matrix = MyMatrix.GetRotationMatrix(Mathf.PI / 2.0f, MyMatrix.RotationType.Roll);
            
            // Assert
            OrthoNormalAssert(matrix);
            
        }

        private void OrthoNormalAssert(MyMatrix matrix)
        {
            // Arrange
            var comparer = FloatEqualityComparer.Instance;
            
            Vector3 x = new Vector3(matrix.GetValue(0, 0), matrix.GetValue(1, 0), matrix.GetValue(2, 0));
            Vector3 y = new Vector3(matrix.GetValue(0, 1), matrix.GetValue(1, 1), matrix.GetValue(2, 1));
            Vector3 z = new Vector3(matrix.GetValue(0, 2), matrix.GetValue(1, 2), matrix.GetValue(2, 2));
            
            // Assert
            Assert.That(x.magnitude, Is.EqualTo(1f).Using(comparer));
            Assert.That(y.magnitude, Is.EqualTo(1f).Using(comparer));
            Assert.That(z.magnitude, Is.EqualTo(1f).Using(comparer));
            
            Assert.That(Vector3.Dot(x, y), Is.EqualTo(0f).Using(comparer));
            Assert.That(Vector3.Dot(x, z), Is.EqualTo(0f).Using(comparer));
            Assert.That(Vector3.Dot(y, z), Is.EqualTo(0f).Using(comparer));
        }
    }
}
