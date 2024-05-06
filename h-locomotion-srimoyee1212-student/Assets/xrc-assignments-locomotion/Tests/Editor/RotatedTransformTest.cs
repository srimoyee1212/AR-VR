using NUnit.Framework;
using UnityEngine;
using System.IO;
using UnityEngine.TestTools.Utils;
using XRC.Utilities.TestUtilities.Locomotion;

namespace XRC.Assignments.Locomotion.Tests
{
    public class RotatedTransformTest : XRCBaseTest
    {
        private (Matrix4x4, Matrix4x4, Vector3, Vector3, float, string)[] m_RotationTestCases =
            new (Matrix4x4, Matrix4x4, Vector3, Vector3, float, string)[]
            {
                (m_MatrixIdentity, m_MatrixIdentity, Vector3.zero, Vector3.up, 0f, "Testing Base Case"),
                (Matrix4x4.TRS(Vector3.zero, new Quaternion(0f, 0.2588191f, 0f, 0.9659259f), Vector3.one),
                    m_MatrixIdentity, Vector3.zero, Vector3.up, 30f, "Testing Angle Degrees"),
                (Matrix4x4.TRS(new Vector3(5f, -10f, 2f), new Quaternion(0f, 0.5f, 0f, 0.8660254f), Vector3.one),
                    Matrix4x4.TRS(new Vector3(5f, -10f, 2f), new Quaternion(0f, 0.2588191f, 0f, 0.9659259f),
                        Vector3.one),
                    new Vector3(5f, -10f, 2f), Vector3.up, 30f, "Testing Starting Transform"),
                (Matrix4x4.TRS(new Vector3(5.330127f, -10f, -0.7679492f), new Quaternion(0f, 0.5f, 0f, 0.8660254f), Vector3.one),
                    Matrix4x4.TRS(new Vector3(5f, -10f, 2f), new Quaternion(0f, 0.2588191f, 0f, 0.9659259f),
                        Vector3.one),
                    Vector3.zero, Vector3.up, 30f, "Testing Center Position"),
                (Matrix4x4.TRS(new Vector3(3.95833f, -10f, -4.687564f), new Quaternion(0f, -0.1305261f, 0f, 0.9914449f), Vector3.one),
                    Matrix4x4.TRS(new Vector3(5f, -10f, 2f), new Quaternion(0f, -0.3826834f, 0f, 0.9238795f),
                        Vector3.one),
                    new Vector3(-8f, 20f, 0.6f), Vector3.up, 30f, "Testing Center Position")
            };
        
        private static Matrix4x4 m_MatrixIdentity = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, Vector3.one);
        private LocomotionBase m_LocomotionBase = new GameObject().AddComponent<LocomotionBase>();
        
        private void AssertMatrix4x4(Matrix4x4 expected, Matrix4x4 actual, string message)
        {
            Assert.That(actual.GetPosition(),
                Is.EqualTo(expected.GetPosition()).Using(Vector3EqualityComparer.Instance),
                message + ", Error in position of resulting Matrix4x4");
            Assert.That(actual.rotation, Is.EqualTo(expected.rotation).Using(QuaternionEqualityComparer.Instance),
                message + ", Error in rotation of resulting Matrix4x4");
            Assert.That(actual.lossyScale, Is.EqualTo(expected.lossyScale).Using(Vector3EqualityComparer.Instance),
                message + ", Error in scale of resulting Matrix4x4");
        }

        public void RunRotatedTransformTest(int min, int max)
        {
            for (int i = min; i < max; i++)
            {
                var inputs = m_RotationTestCases[i];
                var result = m_LocomotionBase.RotatedTransform(inputs.Item2, inputs.Item3, inputs.Item4, inputs.Item5);
                AssertMatrix4x4(inputs.Item1, result, inputs.Item6);
            }
        }
        
        [Test]
        public void BaseCaseIsCorrect()
        {
            RunRotatedTransformTest(0, 1);
        }
        
        [Test]
        public void AngleDegreesIsCorrect()
        {
            RunRotatedTransformTest(1, 2);
        }
        
        [Test]
        public void StartingTransformIsCorrect()
        {
            RunRotatedTransformTest(2, 3);
        }
        
        [Test]
        public void CenterPositionIsCorrect()
        {
            RunRotatedTransformTest(3, 5);
        }
    }
}
