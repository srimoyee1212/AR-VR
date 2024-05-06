using System;
using System.Collections.Generic;
using NUnit.Framework.Internal;

namespace XRC.Utilities.TestUtilities
{
    [Serializable]
    public struct XRCTestResult
    {
        public string Test;
        public string Result;
        public string Message;
    }

    [Serializable]
    public class XRCTestSuiteResult
    {
        public int passCount
        {
            get => Passed;
            set => Passed = value;
        }

        public string Class;
        public string ID;
        public int Total;
        public int Passed;
        public int Failed;
        public int Skipped;
        public int Inconclusive;
        public List<XRCTestResult> Tests;
        
        

        public XRCTestSuiteResult()
        {
            Tests = new List<XRCTestResult>(); 
        }
    }
}