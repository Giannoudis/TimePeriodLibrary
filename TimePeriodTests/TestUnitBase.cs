// -- FILE ------------------------------------------------------------------
// name       : TestUnitBase.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;
using System.IO;
using System.Reflection;
using NUnit.Framework;

namespace Itenso.TimePeriodTests
{

	// ------------------------------------------------------------------------
	public abstract class TestUnitBase
	{

		// ----------------------------------------------------------------------
		[SetUp]
		public void Initialize()
		{
		} // Initialize

		// ----------------------------------------------------------------------
		[TearDown]
		public void Cleanup()
		{
		} // Cleanup

		// ----------------------------------------------------------------------
		public static void AssertEqualLines( string msgPrefix, string expected, string actual )
		{
			if ( !expected.Equals( actual ) )
			{
				AssertEqualLines( msgPrefix, new StringReader( expected ), new StringReader( actual ) );
			}
		} // AssertEqualLines

		// ----------------------------------------------------------------------
		public static void AssertEqualLines( string msgPrefix, TextReader expectedReader, TextReader actualReader )
		{
			int lineNumber = 1;
			string expectedLine = expectedReader.ReadLine();
			string actualLine = actualReader.ReadLine();
			string msg = msgPrefix + " differs from expected at line ";
			while ( expectedLine != null && actualLine != null )
			{
				if ( !expectedLine.Equals( actualLine ) )
				{
					Assert.AreEqual( expectedLine, actualLine, msg + lineNumber );
				}
				lineNumber++;
				expectedLine = expectedReader.ReadLine();
				actualLine = actualReader.ReadLine();
			}
			if ( expectedLine != null || actualLine != null )
			{
				Assert.AreEqual( expectedLine, actualLine, msg + lineNumber );
			}
		} // AssertEqualLines

		// ----------------------------------------------------------------------
		protected void IterateResourceTestCases( string kind, string ext, bool expectFailures )
		{
			int count = 0;
			string testCaseName = BuildTestResourceName( kind, count, false, ext );
			Stream testResStream = GetTestResource( testCaseName );
			while ( testResStream != null )
			{
				count++;
				DoTest( kind, testResStream, testCaseName );
				testCaseName = BuildTestResourceName( kind, count, false, ext );
				testResStream = GetTestResource( testCaseName );
			}
			Assert.AreNotEqual( 0, count, "no valid test cases found: " + testCaseName );

			if ( !expectFailures )
			{
				return;
			}

			count = 0;
			testCaseName = BuildTestResourceName( kind, count, true, ext );
			testResStream = GetTestResource( testCaseName );
			while ( testResStream != null )
			{
				count++;
				bool assertionException = false;
				try
				{
					DoTest( kind, testResStream, testCaseName );
					assertionException = true;
					Assert.Fail( "invalid test case not recognized as invalid: " + testCaseName );
				}
				catch ( Exception )
				{
					// expected, but if it is an assertion exception we need to re-throw it
					if ( assertionException )
					{
						throw;
					}
				}
				testCaseName = BuildTestResourceName( kind, count, true, ext );
				testResStream = GetTestResource( testCaseName );
			}
			Assert.AreNotEqual( 0, count, "no invalid test cases found: " + testCaseName );
		} // IterateResourceTestCases

		// ----------------------------------------------------------------------
		protected virtual void DoTest( string kind, Stream testRes, string testCaseName )
		{
			Assert.Fail( GetType().FullName + " must override method DoTest() when calling IterateTestCases()" );
		} // DoTest

		// ----------------------------------------------------------------------
		protected string BuildTestResourceName( string kind, int pos, bool fail, string ext )
		{
			return GetType().Name + kind + "_" + ( fail ? "fail_" : "" ) + pos + "." + ext;
		} // BuildTestResourceName

		// ----------------------------------------------------------------------
		protected Stream GetTestResource( string filename )
		{
			return GetResourceAsStream( GetType().Name + "/" + filename );
		} // GetTestResource

		// ----------------------------------------------------------------------
		/// <summary>
		/// Opens a stream to the given embedded resource in the test assembly. Fails if the
		/// given test resource cannot be located in the assembly.
		/// </summary>
		/// <param name="name">the name of the embedded resource without assembly prefix</param>
		/// <returns>an open stream to the desired embedded resource</returns>
		protected Stream GetResourceAsStream( string name )
		{
			Type type = GetType();
			Assembly assembly = type.Assembly;
			string namespaceName = type.Namespace;
			string fullName = namespaceName + "." + name.Replace( '\\', '.' ).Replace( '/', '.' );
			return assembly.GetManifestResourceStream( fullName );
		} // GetResourceAsStream

	} // class TestUnitBase

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
