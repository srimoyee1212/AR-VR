using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools.Utils;
using Matrix4x4 = UnityEngine.Matrix4x4;
using Vector3 = UnityEngine.Vector3;
using XRC.Utilities.TestUtilities;

namespace XRC.Assignments.Geometry.Tests
{
    public class LookAtTests : XRCBaseTest
    {
        private Vector3 m_E;
        private Vector3 m_P;
        private Vector3 c;
        private Vector3 m_U;
        private Vector3 x;
        private Vector3 y;
        private Vector3 z;
        private Matrix4x4 T_eye;

        [SetUp]
        public void Init()
        {
             // Arrange
            m_E = 4f * Vector3.forward + 3.5f * Vector3.down;
            m_P = Vector3.right - 14.4f * Vector3.up;
            m_U = Vector3.up;
        }

        [Test]
        public void CIsUnitVector()
        {
            // Arrange
            var comparer = Vector3EqualityComparer.Instance;
            
            // Act
            c = MyLookAt.CalculateCentralLookingDirection(m_E, m_P);
            
            // Assert
            Assert.That(c.magnitude, Is.EqualTo(1f).Using(comparer));
        }

        [Test]
        public void ColumnVectorsAreOrthonormal()
        {
            // Arrange
            var comparer = FloatEqualityComparer.Instance;
            
            // Act
            c = MyLookAt.CalculateCentralLookingDirection(m_E, m_P);
            (x, y, z) = MyLookAt.CalculateColumnVectors(c, m_U);
            
            // Assert
            Assert.That(x.magnitude, Is.EqualTo(1f).Using(comparer));
            Assert.That(y.magnitude, Is.EqualTo(1f).Using(comparer));
            Assert.That(z.magnitude, Is.EqualTo(1f).Using(comparer));
                   
            Assert.That(Vector3.Dot(x, y), Is.EqualTo(0f).Using(comparer));
            Assert.That(Vector3.Dot(x, z), Is.EqualTo(0f).Using(comparer));
            Assert.That(Vector3.Dot(y, z), Is.EqualTo(0f).Using(comparer));
            }

        [Test]
        public void MatrixIsValid()
        {
            // Act
            c = MyLookAt.CalculateCentralLookingDirection(m_E, m_P);
            (x, y, z) = MyLookAt.CalculateColumnVectors(c, m_U);
            T_eye = MyLookAt.CalculateMatrix(x, y, z);
            
            // Assert
            Assert.IsTrue(T_eye.ValidTRS());
        }

        [Test]
        public void IsEqualToStandardMatrix()
        {
            // Arrange
            Matrix4x4 standard = Matrix4x4.LookAt(m_E, m_P, m_U);
            var comparer = Vector4EqualityComparer.Instance;
            
            // Act
            c = MyLookAt.CalculateCentralLookingDirection(m_E, m_P);
            (x, y, z) = MyLookAt.CalculateColumnVectors(c, m_U);
            T_eye = MyLookAt.CalculateMatrix(x, y, z);
            
            // Assert
            Assert.That(standard.GetColumn(0), Is.EqualTo(T_eye.GetColumn(0)).Using(comparer));
            Assert.That(standard.GetColumn(1), Is.EqualTo(T_eye.GetColumn(1)).Using(comparer));
            Assert.That(standard.GetColumn(2), Is.EqualTo(T_eye.GetColumn(2)).Using(comparer));
        }
        
        [Test]
        public void IsEqualToStandardQuaternion()
        {
            // Arrange
            Matrix4x4 standard = Matrix4x4.LookAt(m_E, m_P, m_U);
            Quaternion standardQuaternion = standard.rotation;
            
            // Act
            c = MyLookAt.CalculateCentralLookingDirection(m_E, m_P);
            (x, y, z) = MyLookAt.CalculateColumnVectors(c, m_U);
            T_eye = MyLookAt.CalculateMatrix(x, y, z);
            Quaternion quaternion = T_eye.rotation;
            
            // Assert
            Assert.That(standardQuaternion, Is.EqualTo(quaternion).Using(QuaternionEqualityComparer.Instance));
        }
    }
}
