using NUnit.Framework;
using UnityEngine;
using System.IO;
using UnityEngine.TestTools.Utils;
using XRC.Utilities.TestUtilities.Locomotion;

namespace XRC.Assignments.Locomotion.Tests
{
    public class ContinuousMovementTest : XRCBaseTest
    {
        private (Vector3, Vector3, Vector3, float, float, string)[] m_MovementTestCases =
            new (Vector3, Vector3, Vector3, float, float, string)[]
            {
                (Vector3.zero, Vector3.zero, Vector3.forward, 0f, 1f, "Testing Base Case"),
                (new Vector3(9f, -10f, -50f), new Vector3(10f, -10f, -50f), Vector3.left, 1f, 1f,
                    "Testing Position and Direction"),
                (new Vector3(-3.552786f, 10, 50.89443f), new Vector3(-4f, 10f, 50f),
                    new Vector3(0.4472136f, 0f, 0.8944272f).normalized, 1f, 1f, "Testing Position and Direction"),
                (new Vector3(10f, -10f, -47.5f), new Vector3(10f, -10f, -50f), Vector3.forward, 2.5f, 1f,
                    "Testing Speed"),
                (new Vector3(10f, -10f, -52.5f), new Vector3(10f, -10f, -50f), Vector3.forward, -2.5f, 1f,
                    "Testing Speed"),
                (new Vector3(10f, -10f, -49.9f), new Vector3(10f, -10f, -50f), Vector3.forward, 1f, 0.1f,
                    "Testing DeltaTime"),
                (new Vector3(10f, -10f, -49f), new Vector3(10f, -10f, -50f), Vector3.forward, 2f, 0.5f,
                    "Testing All")
            };
        
        private ContinuousMovementController m_ContinuousMovementController =
            new GameObject().AddComponent<ContinuousMovementController>();

        private StreamWriter m_Writer;

        public void RunContinuousMovementTest(int min, int max)
        {
            for (int i = min; i < max; i++)
            {
                var inputs = m_MovementTestCases[i];
                var result = m_ContinuousMovementController.MovedPosition(inputs.Item2, inputs.Item3, inputs.Item4, inputs.Item5);
                Assert.That(result, Is.EqualTo(inputs.Item1).Using(Vector3EqualityComparer.Instance), inputs.Item6);
            }
        }
        
        [Test]
        public void BaseCaseIsCorrect()
        {
            RunContinuousMovementTest(0, 1);
        }
        
        [Test]
        public void StartingPositionAndDirectionAreCorrect()
        {
            RunContinuousMovementTest(1, 3);
        }
        
        [Test]
        public void SpeedIsCorrect()
        {
            RunContinuousMovementTest(3, 5);
        }
        
        [Test]
        public void DeltaTimeIsCorrect()
        {
            RunContinuousMovementTest(5, 6);
        }
        
        [Test]
        public void AllInputsAreCorrect()
        {
            RunContinuousMovementTest(6, 7);
        }
    }
}
