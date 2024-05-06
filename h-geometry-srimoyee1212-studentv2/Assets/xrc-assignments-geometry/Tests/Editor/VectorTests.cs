using System;
using System.IO;
using NUnit.Framework;
using NUnit.Framework.Internal;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools.Utils;
using Matrix4x4 = UnityEngine.Matrix4x4;
using Vector3 = UnityEngine.Vector3;
using XRC.Utilities.TestUtilities;

namespace XRC.Assignments.Geometry.Tests
{
    public class VectorTests : XRCBaseTest
    {
        [SetUp]
        public void Init()
        {
        }

        [Test]
        public void ColorIsCorrectWhenBehindTarget()
        {
            // Arrange
            var comparer = FloatEqualityComparer.Instance;
            Vector3 position = Vector3.zero;
            Vector3 targetPosition = Vector3.forward * 5 + Vector3.right * 2;
            Vector3 targetForward = Vector3.forward;

            // Act
            Color color = FloorColor.CalculateColor(position, targetPosition, targetForward);

            // Assert
            Assert.That(color.g, Is.EqualTo(0f).Using(comparer));
        }

        [Test]
        public void ColorIsCorrectWhenDirectToTarget()
        {
            // Arrange
            var comparer = FloatEqualityComparer.Instance;

            Vector3 position = Vector3.zero;
            Vector3 targetPosition = Vector3.forward * 5 + Vector3.right * 2;
            Vector3 targetForward = (position - targetPosition).normalized;

            // Act
            Color color = FloorColor.CalculateColor(position, targetPosition, targetForward);

            // Assert
            Assert.That(color.g, Is.EqualTo(1f).Using(comparer));
        }
    }
}