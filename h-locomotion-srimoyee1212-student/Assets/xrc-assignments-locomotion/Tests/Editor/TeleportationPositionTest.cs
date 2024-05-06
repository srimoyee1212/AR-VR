using NUnit.Framework;
using UnityEngine;
using System.IO;
using UnityEngine.TestTools.Utils;
using XRC.Utilities.TestUtilities.Locomotion;

namespace XRC.Assignments.Locomotion.Tests
{
    public class TeleportationPositionTest : XRCBaseTest
    {
        private (Vector3, Vector3, Vector3, Vector3, string)[] m_TeleportationTestCases =
            new (Vector3, Vector3, Vector3, Vector3, string)[]
            {
                (Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, "Testing Base Case"),
                (new Vector3(0f, 20f, -30f), new Vector3(0f, 20f, -30f), Vector3.zero, Vector3.zero,
                    "Testing Target Position"),
                (new Vector3(5f, -10f, 30f), new Vector3(5f, -10f, 30f), Vector3.zero, Vector3.zero,
                    "Testing Target Position"),
                (new Vector3(-5f, 0f, 30f), Vector3.zero, Vector3.zero, new Vector3(5f, 10f, -30f),
                    "Testing Starting Camera Position"),
                (new Vector3(-0.5f, 0f, -80f), Vector3.zero, Vector3.zero, new Vector3(0.5f, -10f, 80f),
                    "Testing Starting Camera Position"),
                (new Vector3(-2f, 0f, 10f), Vector3.zero, new Vector3(-2f, 6f, 10f), Vector3.zero,
                    "Testing Starting Rig Position"),
                (new Vector3(-7f, 0f, 40f), Vector3.zero, new Vector3(-2f, 6f, 10f), new Vector3(5f, 10f, -30f),
                    "Testing Starting Rig Position"),
                (new Vector3(1f, -10f, 55f), new Vector3(8f, -10f, 15f), new Vector3(-2f, 6f, 10f),
                    new Vector3(5f, 10f, -30f), "Testing All"),
                (new Vector3(5.5f, 50f, -45f), new Vector3(4f, 50f, -15f), new Vector3(2f, 3f, -10f),
                    new Vector3(0.5f, -10f, 20f), "Testing All"),
                (new Vector3(-5f, 0f, -6.5f), new Vector3(6f, 0f, 2.5f), new Vector3(-8f, -6f, 0f),
                    new Vector3(3f, 6f, 9f), "Testing All"),
            };
        
        private TeleportationController m_TeleportationController =
            new GameObject().AddComponent<TeleportationController>();
        
        private void RunTeleportationTest(int min, int max)
        {
            for (int i = min; i < max; i++)
            {
                var inputs = m_TeleportationTestCases[i];
                var result = m_TeleportationController.GetTeleportedPosition(inputs.Item2, inputs.Item3, inputs.Item4);
                Assert.That(result, Is.EqualTo(inputs.Item1).Using(Vector3EqualityComparer.Instance), inputs.Item5);
            }
        }
        
        [Test]
        public void BaseCaseIsCorrect()
        {
            RunTeleportationTest(0, 1);
        }
        
        [Test]
        public void TargetPositionIsCorrect()
        {
            RunTeleportationTest(1, 3);
        }
        
        [Test]
        public void StartingCameraPositionIsCorrect()
        {
            RunTeleportationTest(3, 5);
        }
        
        [Test]
        public void StartingRigPositionIsCorrect()
        {
            RunTeleportationTest(5, 7);
        }
        [Test]
        public void AllInputsAreCorrect()
        {
            RunTeleportationTest(7, 10);
        }
    }
}
