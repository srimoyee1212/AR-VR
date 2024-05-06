using System;
using System.IO;
using NUnit.Framework;
using NUnit.Framework.Internal;
using UnityEngine;
using UnityEngine.Profiling.Experimental;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools.Utils;
using Matrix4x4 = UnityEngine.Matrix4x4;
using Vector3 = UnityEngine.Vector3;
using XRC.Utilities.TestUtilities;

namespace XRC.Assignments.Geometry.Tests
{
    public class QuaternionTests : XRCBaseTest
    {
        [SetUp]
        public void Init()
        {
        }

        [Test]
        public void InverseMultiplicationIsIdentityRotation()
        {
            var comparer = FloatEqualityComparer.Instance;

            MyQuaternion quaternion = GetTestQuaternion();
            MyQuaternion inverseQuaternion = MyQuaternion.Inverse(quaternion);
            MyQuaternion identityQuaternion = quaternion * inverseQuaternion;

            Quaternion unityIdentityQuaternion = Quaternion.identity;

            // Assert
            Assert.That(identityQuaternion.a, Is.EqualTo(unityIdentityQuaternion.w).Using(comparer));
            Assert.That(identityQuaternion.b, Is.EqualTo(unityIdentityQuaternion.x).Using(comparer));
            Assert.That(identityQuaternion.c, Is.EqualTo(unityIdentityQuaternion.y).Using(comparer));
            Assert.That(identityQuaternion.d, Is.EqualTo(unityIdentityQuaternion.z).Using(comparer));
        }

        [Test]
        public void MultiplicationQuaternionIsUnitLength()
        {
            var comparer = FloatEqualityComparer.Instance;

            MyQuaternion quaternionA = GetTestQuaternion(0);
            MyQuaternion quaternionB = GetTestQuaternion(0);
            

            Debug.Log("Quaternion: " + quaternionA.a + ", " + quaternionA.b + ", " + quaternionA.c + ", " + quaternionA.d);

            MyQuaternion multiplicationQuaternion = quaternionA * quaternionB;

            // Assert
            float magnitude = Mathf.Sqrt(Mathf.Pow(multiplicationQuaternion.a, 2) +
                                         Mathf.Pow(multiplicationQuaternion.b, 2) +
                                         Mathf.Pow(multiplicationQuaternion.c, 2) +
                                         Mathf.Pow(multiplicationQuaternion.d, 2));

            Assert.That(magnitude, Is.EqualTo(1f).Using(comparer));
        }

        [Test]
        public void MultiplicationIsEqualToUnityMultiplication()
        {
            // Arrange
            var comparer = FloatEqualityComparer.Instance;

            MyQuaternion quaternionA = GetTestQuaternion(0);
            MyQuaternion quaternionB = GetTestQuaternion(1);

            Quaternion unityQuaternionA = new Quaternion(quaternionA.b, quaternionA.c, quaternionA.d, quaternionA.a);
            Quaternion unityQuaternionB = new Quaternion(quaternionB.b, quaternionB.c, quaternionB.d, quaternionB.a);
            Quaternion unityMultiplicationResult = unityQuaternionA * unityQuaternionB;

            // Act
            MyQuaternion multiplicationResult = quaternionA * quaternionB;

            // Assert
            Assert.That(multiplicationResult.a, Is.EqualTo(unityMultiplicationResult.w).Using(comparer));
            Assert.That(multiplicationResult.b, Is.EqualTo(unityMultiplicationResult.x).Using(comparer));
            Assert.That(multiplicationResult.c, Is.EqualTo(unityMultiplicationResult.y).Using(comparer));
            Assert.That(multiplicationResult.d, Is.EqualTo(unityMultiplicationResult.z).Using(comparer));
        }

        /// <summary>
        /// Generates a quaternion to be used for testing, from a standard Unity quaternion
        /// </summary>
        private MyQuaternion GetTestQuaternion(int index = 0)
        {
            MyQuaternion quaternion = new MyQuaternion();
            Quaternion unityQuaternion = new Quaternion();
            switch (index)
            {
                case 0:
                    unityQuaternion = Quaternion.LookRotation(Vector3.left * 0.4f + Vector3.forward * 11.1f,
                        Vector3.up + Vector3.right * 0.3f);
                    break;
                case 1:
                    unityQuaternion = Quaternion.LookRotation(Vector3.right * 14.2f + Vector3.down * 1.1f,
                        Vector3.down * 0.8f + Vector3.left * 8.33f);
                    break;
                default:
                    unityQuaternion = Quaternion.identity;
                    break;
            }

            quaternion.a = unityQuaternion.w;
            quaternion.b = unityQuaternion.x;
            quaternion.c = unityQuaternion.y;
            quaternion.d = unityQuaternion.z;

            return quaternion;
        }

        [Test]
        public void InverseQuaternionIsUnitLength()
        {
            // Arrange
            var comparer = FloatEqualityComparer.Instance;
            MyQuaternion quaternion = new MyQuaternion();
            Quaternion unityQuaternion = Quaternion.LookRotation(Vector3.left + Vector3.forward * 4, Vector3.up);
            quaternion.a = unityQuaternion.w;
            quaternion.b = unityQuaternion.x;
            quaternion.c = unityQuaternion.y;
            quaternion.d = unityQuaternion.z;

            Quaternion.Inverse(unityQuaternion);


            // Act
            MyQuaternion inverseQuaternion = MyQuaternion.Inverse(quaternion);

            // Assert
            float magnitude = Mathf.Sqrt(Mathf.Pow(inverseQuaternion.a, 2) + Mathf.Pow(inverseQuaternion.b, 2) +
                                         Mathf.Pow(inverseQuaternion.c, 2) + Mathf.Pow(inverseQuaternion.d, 2));

            Assert.That(magnitude, Is.EqualTo(1f).Using(comparer));
        }

        [Test]
        public void InverseQuaternionIsEqualToUnityInverseQuaternion()
        {
            // Arrange
            var comparer = FloatEqualityComparer.Instance;
            MyQuaternion quaternion = new MyQuaternion();
            Quaternion unityQuaternion = Quaternion.LookRotation(Vector3.left + Vector3.forward * 4, Vector3.up);
            quaternion.a = unityQuaternion.w;
            quaternion.b = unityQuaternion.x;
            quaternion.c = unityQuaternion.y;
            quaternion.d = unityQuaternion.z;
            Quaternion inverseUnityQuaternion = Quaternion.Inverse(unityQuaternion);

            // Act
            MyQuaternion inverseQuaternion = MyQuaternion.Inverse(quaternion);

            // Assert
            Assert.That(inverseQuaternion.a, Is.EqualTo(inverseUnityQuaternion.w).Using(comparer));
            Assert.That(inverseQuaternion.b, Is.EqualTo(inverseUnityQuaternion.x).Using(comparer));
            Assert.That(inverseQuaternion.c, Is.EqualTo(inverseUnityQuaternion.y).Using(comparer));
            Assert.That(inverseQuaternion.d, Is.EqualTo(inverseUnityQuaternion.z).Using(comparer));
        }
    }
}