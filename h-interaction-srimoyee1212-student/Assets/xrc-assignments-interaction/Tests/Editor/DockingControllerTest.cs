using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools.Utils;
using XRC.Utilities.TestUtilities.Interaction;

namespace XRC.Assignments.Interaction.Tests
{
    public class DockingControllerTest : XRCBaseTest
    {
        private GameObject m_GameObject;
        
        // result, distanceThreshold, target position, dock position
        private (bool, float, Vector3, Vector3, string)[]
            m_DistanceThresholdTestCases = new[]
            {
                (true, 1f, Vector3.zero, Vector3.zero, "Testing Base Case"),
                (false, 1f, new Vector3(1f, 1f, 1f), Vector3.zero, "Testing Target Transform"),
                (false, 1f, Vector3.zero, new Vector3(1f, 1f, 1f), "Testing Dock Transform"),
                (true, 2f, new Vector3(1f, 1f, 1f), Vector3.zero, "Testing Threshold"),
                (true, 2f, Vector3.zero, new Vector3(1f, 1f, 1f), "Testing Threshold"),
            };
        
        // result, rotationThreshold, target rotation, dock rotation
        private (bool, float, Quaternion, Quaternion, string)[]
            m_RotationThresholdTestCases = new[]
            {
                (true, 1f, Quaternion.identity, Quaternion.identity, "Testing Base Case"),
                (false, 1f, Quaternion.Euler(1f, 1f, 1f), Quaternion.identity, "Testing Target Transform"),
                (false, 1f, Quaternion.identity, Quaternion.Euler(1f, 1f, 1f), "Testing Dock Transform"),
                (true, 5f, Quaternion.Euler(1f, 1f, 1f), Quaternion.identity, "Testing Threshold"),
                (true, 5f, Quaternion.identity, Quaternion.Euler(1f, 1f, 1f), "Testing Threshold"),
            };
        
        [Test]
        public void IsUnderDistanceThreshold_Correct()
        {
            // Initial Arrange
            m_GameObject = new GameObject();
            DockingController dockingController = m_GameObject.AddComponent<DockingController>();
            for (int i = 0; i < m_DistanceThresholdTestCases.Length; i++)
            {
                var inputs = m_DistanceThresholdTestCases[i];
                // Arrange
                GameObject target = new GameObject();
                target.name = "Target GameObject";
                target.transform.position = inputs.Item3;
                GameObject dock = new GameObject();
                dock.name = "Dock GameObject";
                dock.transform.position = inputs.Item4;
                // Act
                bool result =
                    dockingController.IsUnderDistanceThreshold(inputs.Item2, target.transform, dock.transform);
                // Assert
                Assert.That(result, Is.EqualTo(inputs.Item1), inputs.Item5);
            }
        }

        [Test]
        public void IsUnderRotationThreshold_Correct()
        {
            // Initial Arrange
            m_GameObject = new GameObject();
            DockingController dockingController = m_GameObject.AddComponent<DockingController>();
            for (int i = 0; i < m_RotationThresholdTestCases.Length; i++)
            {
                var inputs = m_RotationThresholdTestCases[i];
                // Arrange
                GameObject target = new GameObject();
                target.name = "Target GameObject";
                target.transform.rotation = inputs.Item3;
                GameObject dock = new GameObject();
                dock.name = "Dock GameObject";
                dock.transform.rotation = inputs.Item4;
                // Act
                bool result =
                    dockingController.IsUnderRotationThreshold(inputs.Item2, target.transform, dock.transform);
                // Assert
                Assert.That(result, Is.EqualTo(inputs.Item1), inputs.Item5);
            }
        }
    }
}