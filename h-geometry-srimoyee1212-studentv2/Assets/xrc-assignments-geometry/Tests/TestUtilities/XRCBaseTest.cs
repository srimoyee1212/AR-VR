using System.IO;
using NUnit.Framework;
using NUnit.Framework.Internal;
using UnityEngine;
using UnityEngine.TestTools;

namespace XRC.Utilities.TestUtilities
{
    public class XRCBaseTest : IPostBuildCleanup, IPrebuildSetup
    {
        private StreamWriter m_Writer;
        private int m_PassCount;
        private int m_FailCount;
        private string m_Filename;
        private string m_Summary;

        private XRCTestSuiteResult m_TestSuiteResult;
        private int m_InconlusiveCount;
        private int m_SkipCount;
        private int m_TotalCount;

        [OneTimeSetUp]
        public void InitCounter()
        {
            m_PassCount = 0;
            m_FailCount = 0;
            m_InconlusiveCount = 0;
            m_SkipCount = 0;
            m_TotalCount = 0;
            m_Filename = TestContext.CurrentContext.Test.Name;
            m_TestSuiteResult = new XRCTestSuiteResult();
        }

        [TearDown]
        public void TestStatistics()
        {
            m_PassCount += TestContext.CurrentContext.Result.PassCount;
            m_FailCount += TestContext.CurrentContext.Result.FailCount;
            m_InconlusiveCount += TestContext.CurrentContext.Result.InconclusiveCount;
            m_SkipCount += TestContext.CurrentContext.Result.SkipCount;

            /*m_TestSuiteResult.Class = TestContext.CurrentContext.Test.ClassName;
            m_TestSuiteResult.Failed = m_FailCount;
            m_TestSuiteResult.Passed = m_PassCount;
            m_TestSuiteResult.ID = TestContext.CurrentContext.Test.ID;*/
            var testResult = new XRCTestResult
            {
                Test = TestContext.CurrentContext.Test.Name,
                Result = TestContext.CurrentContext.Result.Outcome.ToString()
            };

            m_TestSuiteResult.Tests.Add(testResult);
        }

        [OneTimeTearDown]
        public void SaveResults()
        {
            m_TestSuiteResult.Class = TestContext.CurrentContext.Test.ClassName;
            m_TestSuiteResult.Failed = m_FailCount;
            m_TestSuiteResult.Passed = m_PassCount;
            m_TestSuiteResult.Skipped = m_SkipCount;
            m_TestSuiteResult.Inconclusive = m_InconlusiveCount;

            m_TotalCount = m_FailCount + m_PassCount + m_InconlusiveCount + m_SkipCount;
            
            m_TestSuiteResult.Total = m_TotalCount;
            m_TestSuiteResult.ID = TestContext.CurrentContext.Test.ID;
            WriteScoreToFile();
        }

        private void WriteScoreToFile()
        {
            string outputDirectory = "TestResults";
            Directory.CreateDirectory(outputDirectory);
            string path = outputDirectory + "/" + TestContext.CurrentContext.Test.ClassName + ".json";

            m_Writer = new StreamWriter(path, false);
            string json = JsonUtility.ToJson(m_TestSuiteResult, true);
            m_Writer.WriteLine(json);
            m_Writer.Close();
        }

        public void Cleanup()
        {
            //throw new System.NotImplementedException();
        }

        public void Setup()
        {
            /*System.IO.DirectoryInfo di = new DirectoryInfo("Results");

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete(); 
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true); 
            }*/
        }
    }
}