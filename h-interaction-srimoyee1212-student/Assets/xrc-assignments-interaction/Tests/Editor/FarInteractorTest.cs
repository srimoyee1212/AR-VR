using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools.Utils;
using XRC.Utilities.TestUtilities.Interaction;

namespace XRC.Assignments.Interaction.Tests
{
    public class FarInteractorTest : XRCBaseTest
    {
        private GameObject m_GameObject;
        
        // result position, interactable position, interactor position, interactor rotation, attach position, moveSpeed, deltaTime
        private (Vector3, Vector3, Vector3, Quaternion, Vector3, float, float, string)[]
            m_FarMoveTestCases = new[]
            {
                (Vector3.zero, Vector3.zero, Vector3.zero, Quaternion.identity, Vector3.zero, 0f, 1f, "Testing Base Case"),
                (new Vector3(5f, 5f, 6f), new Vector3(5f, 5f, 5f), Vector3.zero, Quaternion.identity, Vector3.zero, 1f, 1f, "Testing Basic Movement"),
                (new Vector3(5f, 5f, 8f), new Vector3(5f, 5f, 5f), Vector3.zero, Quaternion.identity, Vector3.zero, 3f, 1f, "Testing Basic Movement"),
                (new Vector3(6f, 5f, 5f), new Vector3(5f, 5f, 5f), Vector3.zero, Quaternion.Euler(0f, 90f, 0f), Vector3.zero, 1f, 1f, "Testing Interactor Pointing Direction"),
                (new Vector3(7f, 5f, 5f), new Vector3(5f, 5f, 5f), Vector3.zero, Quaternion.Euler(0f, 90f, 0f), new Vector3(2f, 6f, 4f), 2f, 1f, "Testing Attach Transform"),
                (new Vector3(3f, 5f, 5f), new Vector3(5f, 5f, 5f), Vector3.zero, Quaternion.Euler(0f, 90f, 0f), new Vector3(2f, 6f, 4f), -5f, 2f, "Testing Move Behind The Hand"),
            };
        
        // result rotation, result position, interactable rotation, attach rotation, attach position, angleSpeed, deltaTime
        private (Quaternion, Vector3, Quaternion, Quaternion, Vector3, float, float, string)[]
            m_FarRotateTestCases = new[]
            {
                (Quaternion.identity, Vector3.zero, Quaternion.identity, Quaternion.identity, Vector3.zero, 0f, 1f, "Testing Base Case"),
                (Quaternion.Euler(0f, 10f, 0f), Vector3.zero, Quaternion.identity, Quaternion.identity, Vector3.zero, 10f, 1f, "Testing Basic Rotation"),
                (Quaternion.Euler(0f, -10f, 0f), Vector3.zero, Quaternion.identity, Quaternion.identity, Vector3.zero, -10f, 1f, "Testing Angle Speed Direction"),
                (Quaternion.Euler(0f, 0f, -10f), Vector3.zero, Quaternion.identity, Quaternion.Euler(90f, 0f, 0f), Vector3.zero, -10f, 1f, "Testing Attach Transform Up"),
                (Quaternion.Euler(0f, 0f, -90f), new Vector3(-3f, 5f, 0f), Quaternion.identity, Quaternion.Euler(90f, 0f, 0f), new Vector3(1f, 4f, 2f), -90f, 1f, "Testing Attach Transform Position"),
            };
        
        [Test]
        public void FarManipulationMove_DirectionAndDistanceCorrect()
        {
            // Initial Arrange
            m_GameObject = new GameObject();
            FarInteractor farInteractor = m_GameObject.AddComponent<FarInteractor>();
            // Arrange
            for (int i = 0; i < 5; i++)
            {
                var inputs = m_FarMoveTestCases[i];
                // Arrange
                GameObject interactable = new GameObject();
                interactable.name = "Interactable GameObject";
                interactable.transform.position = inputs.Item2;
                GameObject interactor = new GameObject();
                interactor.name = "Interactor GameObject";
                interactor.transform.position = inputs.Item3;
                interactor.transform.rotation = inputs.Item4;
                GameObject attach = new GameObject();
                attach.name = "Attach GameObject";
                attach.transform.position = inputs.Item5;
                // Act
                farInteractor.FarManipulationMove(interactable.transform, interactor.transform, 
                    attach.transform, inputs.Item6, inputs.Item7);
                // Assert
                Assert.That(interactable.transform.position, Is.EqualTo(inputs.Item1).
                        Using(Vector3EqualityComparer.Instance), inputs.Item8);
            }
        }
        
        [Test]
        public void FarManipulationMove_EdgeCasesChecked()
        {
            // Initial Arrange
            m_GameObject = new GameObject();
            FarInteractor farInteractor = m_GameObject.AddComponent<FarInteractor>();
            // Arrange
            for (int i = 5; i < m_FarMoveTestCases.Length; i++)
            {
                var inputs = m_FarMoveTestCases[i];
                // Arrange
                GameObject interactable = new GameObject();
                interactable.name = "Interactable GameObject";
                interactable.transform.position = inputs.Item2;
                GameObject interactor = new GameObject();
                interactor.name = "Interactor GameObject";
                interactor.transform.position = inputs.Item3;
                interactor.transform.rotation = inputs.Item4;
                GameObject attach = new GameObject();
                attach.name = "Attach GameObject";
                attach.transform.position = inputs.Item5;
                // Act
                farInteractor.FarManipulationMove(interactable.transform, interactor.transform, 
                    attach.transform, inputs.Item6, inputs.Item7);
                // Assert
                Assert.That(interactable.transform.position, Is.EqualTo(inputs.Item1).
                    Using(Vector3EqualityComparer.Instance), inputs.Item8);
            }
        }

        [Test]
        public void FarManipulationRotate_AngleIsCorrect()
        {
            // Initial Arrange
            m_GameObject = new GameObject();
            FarInteractor farInteractor = m_GameObject.AddComponent<FarInteractor>();
            // Arrange
            for (int i = 0; i < 3; i++)
            {
                var inputs = m_FarRotateTestCases[i];
                // Arrange
                GameObject interactable = new GameObject();
                interactable.name = "Interactable GameObject";
                interactable.transform.rotation = inputs.Item3;
                GameObject attach = new GameObject();
                attach.name = "Attach GameObject";
                attach.transform.rotation = inputs.Item4;
                attach.transform.position = inputs.Item5;
                // Act
                farInteractor.FarManipulationRotate(interactable.transform, attach.transform, inputs.Item6,
                    inputs.Item7);
                // Assert
                Assert.That(interactable.transform.position, Is.EqualTo(inputs.Item2).
                    Using(Vector3EqualityComparer.Instance), inputs.Item8 + ", Error in position");
                Assert.That(interactable.transform.rotation, Is.EqualTo(inputs.Item1).
                    Using(QuaternionEqualityComparer.Instance), inputs.Item8 + ", Error in rotation");
            }
        }
        
        [Test]
        public void FarManipulationRotate_AxisIsCorrect()
        {
            // Initial Arrange
            m_GameObject = new GameObject();
            FarInteractor farInteractor = m_GameObject.AddComponent<FarInteractor>();
            // Arrange
            for (int i = 3; i < m_FarRotateTestCases.Length; i++)
            {
                var inputs = m_FarRotateTestCases[i];
                // Arrange
                GameObject interactable = new GameObject();
                interactable.name = "Interactable GameObject";
                interactable.transform.rotation = inputs.Item3;
                GameObject attach = new GameObject();
                attach.name = "Attach GameObject";
                attach.transform.rotation = inputs.Item4;
                attach.transform.position = inputs.Item5;
                // Act
                farInteractor.FarManipulationRotate(interactable.transform, attach.transform, inputs.Item6,
                    inputs.Item7);
                // Assert
                Assert.That(interactable.transform.position, Is.EqualTo(inputs.Item2).
                    Using(Vector3EqualityComparer.Instance), inputs.Item8 + ", Error in position");
                Assert.That(interactable.transform.rotation, Is.EqualTo(inputs.Item1).
                    Using(QuaternionEqualityComparer.Instance), inputs.Item8 + ", Error in rotation");
            }
        }
    }
}