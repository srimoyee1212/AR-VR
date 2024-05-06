using NUnit.Framework;
using UnityEngine;
using System.IO;
using UnityEngine.TestTools.Utils;
using XRC.Utilities.TestUtilities.Locomotion;

namespace XRC.Assignments.Locomotion.Tests
{
    public class TeleportationFacingDiretionTest : XRCBaseTest
    {
        private (Vector3, Vector3, Vector2, string)[] m_DirectionTestCases =
            new (Vector3, Vector3, Vector2, string)[]
            {
                (Vector3.zero, Vector3.zero, Vector2.zero, "Testing Base Case"),
                (Vector3.forward, Vector3.forward, Vector2.zero, "Testing Hand Pointing Direction"),
                (new Vector3(0.70710678118f, 0f, -0.70710678118f), new Vector3(0.4082483f, 0.8164966f, -0.4082483f), Vector2.up, "Testing Hand Pointing Direction"),
                (Vector3.right, Vector3.forward, Vector2.right, "Testing Input Vector"),
                (Vector3.back, Vector3.forward, Vector2.down, "Testing Input Vector"),
                (new Vector3(-1f, 0f, 0f), new Vector3(0.4082483f, 0.8164966f, -0.4082483f),
                    new Vector2(0.7071068f, -0.7071068f), "Testing All"),
                (new Vector3(0.1240348f, 0f, -0.9922779f), new Vector3(0.3841106f, -0.5121475f, 0.7682213f),
                    new Vector2(0.5547002f, -0.8320503f), "Testing All"),
            };
        
        private TeleportationController m_TeleportationController =
            new GameObject().AddComponent<TeleportationController>();
        
        private void RunFacingDirectionTest(int min, int max)
        {
            for (int i = min; i < max; i++)
            {
                var inputs = m_DirectionTestCases[i];
                var result = m_TeleportationController.GetFacingDirection(inputs.Item2, inputs.Item3);
                Assert.That(result, Is.EqualTo(inputs.Item1).Using(Vector3EqualityComparer.Instance), inputs.Item4);
            }
        }
        
        [Test]
        public void BaseCaseIsCorrect()
        {
            RunFacingDirectionTest(0, 1);
        }
        
        [Test]
        public void HandPointingDirectionIsCorrect()
        {
            RunFacingDirectionTest(1, 3);
        }
        
        [Test]
        public void InputVectorIsCorrect()
        {
            RunFacingDirectionTest(3, 5);
        }
        
        [Test]
        public void BothInputsAreCorrect()
        {
            RunFacingDirectionTest(5, 7);
        }
    }
}
