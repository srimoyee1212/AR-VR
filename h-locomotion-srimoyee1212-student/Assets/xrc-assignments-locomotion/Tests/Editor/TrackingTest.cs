using NUnit.Framework;
using UnityEngine;
using System.IO;
using UnityEngine.TestTools.Utils;
using XRC.Utilities.TestUtilities.Locomotion;

namespace XRC.Assignments.Locomotion.Tests
{
    public class TrackingTest : XRCBaseTest
    {
        private static Matrix4x4 m_MatrixIdentity = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, Vector3.one);
        private TrackingInput m_TrackingInput = new GameObject().AddComponent<TrackingInput>();

        private (Matrix4x4, Matrix4x4, Matrix4x4, string)[] m_TrackingTestCases =
            new (Matrix4x4, Matrix4x4, Matrix4x4, string)[]
            {
                (m_MatrixIdentity, m_MatrixIdentity, m_MatrixIdentity, "Testing Base Case"),
                (Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(0.5f, 0.5f, 0.5f)),
                    Matrix4x4.TRS(Vector3.zero, Quaternion.identity, Vector3.one),
                    Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(2, 2, 2)),
                    "Testing Scale"),
                (Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(4f, 4f, 4f)),
                    Matrix4x4.TRS(Vector3.zero, Quaternion.identity, Vector3.one),
                    Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(0.25f, 0.25f, 0.25f)),
                    "Testing Scale"),
                (Matrix4x4.TRS(new Vector3(18, 6, -3), Quaternion.identity, Vector3.one),
                    Matrix4x4.TRS(new Vector3(2, 4, 8), Quaternion.identity, Vector3.one),
                    Matrix4x4.TRS(new Vector3(-20, -10, -5), Quaternion.identity, Vector3.one),
                    "Testing Position"),
                (Matrix4x4.TRS(new Vector3(6, -7.5f, -4.5f), Quaternion.identity, Vector3.one),
                    Matrix4x4.TRS(new Vector3(-6, 0.5f, 8f), Quaternion.identity, Vector3.one),
                    Matrix4x4.TRS(new Vector3(0, 7, -3.5f), Quaternion.identity, Vector3.one),
                    "Testing Position"),
                (Matrix4x4.TRS(Vector3.zero, new Quaternion(0.7663488f, -0.03313505f, 0.2043794f, 0.6081452f), Vector3.one),
                    Matrix4x4.TRS(Vector3.zero, new Quaternion(0.3919038f, 0.3604234f, 0.4396797f, 0.7233174f),
                        Vector3.one),
                    Matrix4x4.TRS(Vector3.zero, new Quaternion(-0.704416f, 0.0616284f, -0.704416f, 0.0616284f),
                        Vector3.one),
                    "Testing Rotation"),
                (Matrix4x4.TRS(new Vector3(26.86702f, 221.336f, 22.05292f), new Quaternion(-0.149428f, -0.02858854f, -0.455073f, 0.8773611f), new Vector3(2f, 2f, 2f)),
                    Matrix4x4.TRS(new Vector3(40, 5, 10),
                        new Quaternion(0.3067311f, 0.5660486f, 0.3437951f, 0.6836007f), Vector3.one),
                    Matrix4x4.TRS(new Vector3(-25, -90, 30),
                        new Quaternion(-0.4147297f, -0.3888735f, 0.0852703f, 0.8182333f),
                        new Vector3(0.5f, 0.5f, 0.5f)),
                    "Testing Transform")
            };
        
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
        
        private void TrackingIsCorrect(int min, int max)
        {
            for (int i = min; i < max; i++)
            {
                var inputs = m_TrackingTestCases[i];
                var result = m_TrackingInput.TrackedWorldTransform(inputs.Item2, inputs.Item3);
                AssertMatrix4x4(inputs.Item1, result, inputs.Item4);
            }
        }

        [Test]
        public void BaseCaseIsCorrect()
        {
            TrackingIsCorrect(0, 1);
        }
        
        [Test]
        public void ResultScaleIsCorrect()
        {
            TrackingIsCorrect(1, 3);
        }
        
        [Test]
        public void ResultPositionIsCorrect()
        {
            TrackingIsCorrect(3, 5);
        }
        [Test]
        public void ResultRotationIsCorrect()
        {
            TrackingIsCorrect(5, 6);
        }
        
        [Test]
        public void ResultTransformIsCorrect()
        {
            TrackingIsCorrect(6, 7);
        }
    }
}
