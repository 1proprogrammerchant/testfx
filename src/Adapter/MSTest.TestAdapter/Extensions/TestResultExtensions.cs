﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.VisualStudio.TestPlatform.MSTest.TestAdapter.ObjectModel;

using UTF = Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Microsoft.VisualStudio.TestPlatform.MSTest.TestAdapter.Extensions;
public static class TestResultExtensions
{
    /// <summary>
    /// Converts the test framework's TestResult objects array to a serializable UnitTestResult objects array.
    /// </summary>
    /// <param name="testResults">The test framework's TestResult object array.</param>
    /// <returns>The serializable UnitTestResult object array.</returns>
    public static UnitTestResult[] ToUnitTestResults(this UTF.TestResult[] testResults)
    {
        UnitTestResult[] unitTestResults = new UnitTestResult[testResults.Length];

        for (int i = 0; i < testResults.Length; ++i)
        {
            UnitTestOutcome outcome = testResults[i].Outcome.ToUnitTestOutcome();

            UnitTestResult unitTestResult = testResults[i].TestFailureException != null
                ? new UnitTestResult(
                    new TestFailedException(
                        outcome,
                        testResults[i].TestFailureException!.TryGetMessage(),
                        testResults[i].TestFailureException is TestFailedException testException ? testException.StackTraceInformation : testResults[i].TestFailureException!.TryGetStackTraceInformation()))
                : new UnitTestResult { Outcome = outcome };
            unitTestResult.StandardOut = testResults[i].LogOutput;
            unitTestResult.StandardError = testResults[i].LogError;
            unitTestResult.DebugTrace = testResults[i].DebugTrace;
            unitTestResult.TestContextMessages = testResults[i].TestContextMessages;
            unitTestResult.Duration = testResults[i].Duration;
            unitTestResult.DisplayName = testResults[i].DisplayName;
            unitTestResult.DatarowIndex = testResults[i].DatarowIndex;
            unitTestResult.ResultFiles = testResults[i].ResultFiles;
            unitTestResult.ExecutionId = testResults[i].ExecutionId;
            unitTestResult.ParentExecId = testResults[i].ParentExecId;
            unitTestResult.InnerResultsCount = testResults[i].InnerResultsCount;
            unitTestResults[i] = unitTestResult;
        }

        return unitTestResults;
    }
}
