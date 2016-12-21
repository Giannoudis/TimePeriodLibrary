// -- FILE ------------------------------------------------------------------
// name       : Program.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using NUnit.Core;

namespace Itenso.TimePeriodTests
{

	// ------------------------------------------------------------------------
	public class TestResultReport
	{

		// ----------------------------------------------------------------------
		public TestResultReport( TestResult result )
		{
			if ( result == null )
			{
				throw new ArgumentNullException( "result" );
			}

			this.result = result;

			AppendResult( result );
		} // TestResultReport

		// ----------------------------------------------------------------------
		public TestResult Result
		{
			get { return result; }
		} // Result

		// ----------------------------------------------------------------------
		public IList<string> FailedTests
		{
			get { return failedTests; }
		} // FailedTests

		// ----------------------------------------------------------------------
		public IList<string> ErrorTests
		{
			get { return errorTests; }
		} // ErrorTests

		// ----------------------------------------------------------------------
		private void AppendResult( TestResult testResult )
		{
			if ( testResult.IsSuccess )
			{
				return;
			}

			if ( testResult.Results == null )
			{
				if ( testResult.IsFailure )
				{
					failedTests.Add( string.Format( "{0}: {1}", testResult.FullName, testResult.Message ) );
					if ( !string.IsNullOrEmpty( testResult.StackTrace ) )
					{
						failedTests.Add( testResult.StackTrace );
					}
				}
				if ( testResult.IsError )
				{
					errorTests.Add( string.Format( "{0}: {1}", testResult.FullName, testResult.Message ) );
					if ( !string.IsNullOrEmpty( testResult.StackTrace ) )
					{
						errorTests.Add( testResult.StackTrace );
					}
				}
			}
			else
			{
				foreach ( TestResult childResult in testResult.Results )
				{
					AppendResult( childResult );
				}
			}
		} // AppendResult

		// ----------------------------------------------------------------------
		// memebers
		private readonly IList<string> failedTests = new List<string>();
		private readonly IList<string> errorTests = new List<string>();
		private readonly TestResult result;

	} // class Program

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
