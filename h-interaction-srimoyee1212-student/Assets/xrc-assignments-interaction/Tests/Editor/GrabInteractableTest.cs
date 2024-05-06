using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools.Utils;
using XRC.Utilities.TestUtilities.Interaction;

namespace XRC.Assignments.Interaction.Tests
{
    public class GrabInteractableTest : XRCBaseTest
    {
        private GameObject m_GameObject;
        // result position, result rotation, this position, this rotation, parent position, parent rotation, attach position, attach rotation
        private (Vector3, Quaternion, Vector3, Quaternion, Vector3, Quaternion, Vector3, Quaternion, string)[]
            m_GrabingTestCases = new[]
            {
                (Vector3.zero, Quaternion.identity, Vector3.zero, Quaternion.identity, Vector3.zero,
                    Quaternion.identity, Vector3.zero, Quaternion.identity, "Testing Base Case"),
                (new Vector3(1f, 5f, 9f), Quaternion.identity, new Vector3(2f, 3f, 4f), Quaternion.identity, new Vector3(5f, 10f, 15f),
                    Quaternion.identity, new Vector3(6f, 8f, 10f), Quaternion.identity, "Testing Positions"),
                (Vector3.zero, Quaternion.Euler(0f, 0f, 90f), Vector3.zero, Quaternion.Euler(90f, 0f, 0f), Vector3.zero,
                    Quaternion.Euler(-90f, 0f, 90f), Vector3.zero, Quaternion.Euler(0f, 90f, 90f), "Testing Rotations"),
                (new Vector3(1f, 5f, 9f), Quaternion.Euler(0f, 0f, 90f), new Vector3(2f, 3f, 4f), Quaternion.Euler(90f, 0f, 0f), new Vector3(5f, 10f, 15f),
                    Quaternion.Euler(-90f, 0f, 90f), new Vector3(6f, 8f, 10f), Quaternion.Euler(0f, 90f, 90f), "Testing Transforms"),
                (new Vector3(-18f, -60f, 13f), new Quaternion(0.6932333f, 0.5517767f, -0.1982232f, -0.4191393f), new Vector3(-10f, -2f, 5f), Quaternion.Euler(90f, 120f, -90f), new Vector3(0f, -8f, 2f),
                    Quaternion.Euler(45f, 30f, 0f), new Vector3(8f, 50f, -6f), Quaternion.Euler(-60f, 90f, 45f), "Testing Transforms"),
            };
        
        private (GameObject, GameObject, GameObject) InitializeGrabingGameObjects(Vector3 position1, Quaternion rotation1, 
            Vector3 position2, Quaternion rotation2, Vector3 position3, Quaternion rotation3)
        {
            GameObject interactable = new GameObject();
            interactable.name = "Interactable GameObject";
            interactable.transform.position = position1;
            interactable.transform.rotation = rotation1;
            GameObject parent = new GameObject();
            parent.name = "Parent GameObject";
            parent.transform.position = position2;
            parent.transform.rotation = rotation2;
            GameObject attach = new GameObject();
            attach.name = "Attach GameObject";
            attach.transform.position = position3;
            attach.transform.rotation = rotation3;
            return (interactable, parent, attach);
        }
        
        [Test]
        public void Grab_ParentUpdatedCorrectly()
        {
            // Initial Arrange
            m_GameObject = new GameObject();
            m_GameObject.AddComponent<BoxCollider>();
            GrabInteractable grabInteractable = m_GameObject.AddComponent<GrabInteractable>();
            
            for (int i = 0; i < m_GrabingTestCases.Length; i++)
            {
                var inputs = m_GrabingTestCases[i];
                // Arrange
                (GameObject, GameObject, GameObject) objects = InitializeGrabingGameObjects(inputs.Item3, inputs.Item4, 
                    inputs.Item5, inputs.Item6, inputs.Item7, inputs.Item8);
                GameObject interactable = objects.Item1;
                GameObject parent = objects.Item2;
                // Act
                grabInteractable.Grab(interactable.transform, parent.transform);
                Transform resultParent = interactable.transform.parent;
                // Assert
                Assert.That(resultParent, Is.EqualTo(parent.transform), inputs.Item9 + ", Error in parent");
            }
        }
        
        [Test]
        public void GrabAttachTransform_TransformAlignedCorrectly()
        {
            // Initial Arrange
            m_GameObject = new GameObject();
            m_GameObject.AddComponent<BoxCollider>();
            GrabInteractable grabInteractable = m_GameObject.AddComponent<GrabInteractable>();
            
            for (int i = 0; i < m_GrabingTestCases.Length; i++)
            {
                var inputs = m_GrabingTestCases[i];
                // Arrange
                (GameObject, GameObject, GameObject) objects = InitializeGrabingGameObjects(inputs.Item3, inputs.Item4, 
                    inputs.Item5, inputs.Item6, inputs.Item7, inputs.Item8);
                GameObject interactable = objects.Item1;
                GameObject parent = objects.Item2;
                GameObject attach = objects.Item3;
                // Act
                grabInteractable.GrabAttachTransform(interactable.transform, parent.transform, attach.transform);
                Vector3 resultPosition = interactable.transform.position;
                Quaternion resultRotation = interactable.transform.rotation;
                // Assert
                Assert.That(resultPosition, Is.EqualTo(inputs.Item1).Using(Vector3EqualityComparer.Instance),
                    inputs.Item9 + ", Error in position");
                Assert.That(resultRotation, Is.EqualTo(inputs.Item2).Using(QuaternionEqualityComparer.Instance),
                    inputs.Item9 + ", Error in rotation");
            }
        }

        [Test]
        public void GrabAttachTransform_ParentUpdatedCorrectly()
        {
            // Initial Arrange
            m_GameObject = new GameObject();
            m_GameObject.AddComponent<BoxCollider>();
            GrabInteractable grabInteractable = m_GameObject.AddComponent<GrabInteractable>();
            
            for (int i = 0; i < m_GrabingTestCases.Length; i++)
            {
                var inputs = m_GrabingTestCases[i];
                // Arrange
                (GameObject, GameObject, GameObject) objects = InitializeGrabingGameObjects(inputs.Item3, inputs.Item4, 
                    inputs.Item5, inputs.Item6, inputs.Item7, inputs.Item8);
                GameObject interactable = objects.Item1;
                GameObject parent = objects.Item2;
                GameObject attach = objects.Item3;
                // Act
                grabInteractable.GrabAttachTransform(interactable.transform, parent.transform, attach.transform);
                Transform resultParent = interactable.transform.parent;
                // Assert
                Assert.That(resultParent, Is.EqualTo(parent.transform), inputs.Item9 + ", Error in parent");
            }
        }

        [Test] public void ReleaseResetParent_ParentResetCorrectly()
        {
            // Arrange
            m_GameObject = new GameObject();
            m_GameObject.AddComponent<BoxCollider>();
            GrabInteractable grabInteractable = m_GameObject.AddComponent<GrabInteractable>();
            
            GameObject interactable = new GameObject();
            interactable.name = "Interactable GameObject";
            GameObject currentParent = new GameObject();
            currentParent.name = "Current Parent GameObject";
            GameObject originalParent = new GameObject();
            originalParent.name = "Original Parent GameObject";
            interactable.transform.parent = currentParent.transform;
            // Act
            grabInteractable.ReleaseResetParent(interactable.transform, originalParent.transform);
            // Assert
            Assert.That(interactable.transform.parent, Is.EqualTo(originalParent.transform), "Testing Parent");
        }
    }
}
