using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools.Utils;
using XRC.Utilities.TestUtilities.Interaction;

namespace XRC.Assignments.Interaction.Tests
{
    public class NearInteractorTest: XRCBaseTest
    {
        private GameObject m_GameObject;
        
        // result scale, currentScale, scaleSpeed, deltaTime, minScale, maxScale
        private (Vector3, Vector3, float, float, Vector3, Vector3, string)[]
            m_NearScalingTestCases = new[]
            {
                (Vector3.one, Vector3.one, 0f, 1f, Vector3.zero, Vector3.one, "Testing Base Case"),
                (new Vector3(2f, 2f, 2f), Vector3.one, 1f, 1f, Vector3.zero, new Vector3(10f, 10f, 10f), "Testing Scale Speed"),
                (new Vector3(4f, 4f, 4f), new Vector3(2f, 2f, 2f), 1f, 1f, Vector3.zero, new Vector3(10f, 10f, 10f), "Testing Current Scale"),
                (new Vector3(3f, 3f, 3f), new Vector3(2f, 2f, 2f), 1f, 1f, Vector3.zero, new Vector3(3f, 3f, 3f), "Testing Max Scale"),
                (new Vector3(1.8f, 1.8f, 1.8f), new Vector3(2f, 2f, 2f), -1f, 1f, new Vector3(1.8f, 1.8f, 1.8f), new Vector3(3f, 3f, 3f), "Testing Min Scale"),
            };
        
        [Test]
        public void NearSphereScale_ScalingIsCorrect()
        {
            // Initial Arrange
            m_GameObject = new GameObject();
            NearInteractor nearInteractor = m_GameObject.AddComponent<NearInteractor>();
            for (int i = 0; i < 3; i++)
            {
                var inputs = m_NearScalingTestCases[i];
                // Act
                Vector3 result = nearInteractor.NearSphereScale(inputs.Item2, inputs.Item3, 
                    inputs.Item4, inputs.Item5, inputs.Item6);
                // Assert
                Assert.That(result, Is.EqualTo(inputs.Item1).Using(Vector3EqualityComparer.Instance),
                    inputs.Item7);
            }
        }
        
        [Test]
        public void NearSphereScale_MinScaleClampedCorrectly()
        {
            // Initial Arrange
            m_GameObject = new GameObject();
            NearInteractor nearInteractor = m_GameObject.AddComponent<NearInteractor>();
            for (int i = 4; i < 5; i++)
            {
                var inputs = m_NearScalingTestCases[i];
                // Act
                Vector3 result = nearInteractor.NearSphereScale(inputs.Item2, inputs.Item3, 
                    inputs.Item4, inputs.Item5, inputs.Item6);
                // Assert
                Assert.That(result, Is.EqualTo(inputs.Item1).Using(Vector3EqualityComparer.Instance),
                    inputs.Item7);
            }
        }
        
        [Test]
        public void NearSphereScale_MaxScaleClampedCorrectly()
        {
            // Initial Arrange
            m_GameObject = new GameObject();
            NearInteractor nearInteractor = m_GameObject.AddComponent<NearInteractor>();
            for (int i = 3; i < 4; i++)
            {
                var inputs = m_NearScalingTestCases[i];
                // Act
                Vector3 result = nearInteractor.NearSphereScale(inputs.Item2, inputs.Item3, 
                    inputs.Item4, inputs.Item5, inputs.Item6);
                // Assert
                Assert.That(result, Is.EqualTo(inputs.Item1).Using(Vector3EqualityComparer.Instance),
                    inputs.Item7);
            }
        }
    }
}