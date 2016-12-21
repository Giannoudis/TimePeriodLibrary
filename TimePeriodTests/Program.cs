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
using System.Reflection;
using Itenso.TimePeriod;
using NUnit.Core;

namespace Itenso.TimePeriodTests
{

	// ------------------------------------------------------------------------
	class Program
	{

		// ----------------------------------------------------------------------
		static void Main( string[] args )
		{
			Assembly assembly = typeof( Program ).Assembly;
			if ( assembly == null )
			{
				throw new ApplicationException();
			}

			bool fullTest = false;
			bool waitAtEnd = false;
			bool waitBecauseOfError = false;
			bool testSucceed = true;
			bool testPerformance = false;
			if ( args != null && args.Length > 0 )
			{
				foreach ( string arg in args )
				{
					if ( "-full".Equals( arg ) )
					{
						fullTest = true;
					}
					if ( "-wait".Equals( arg ) )
					{
						waitAtEnd = true;
					}
					if ( "-perf".Equals( arg ) )
					{
						testPerformance = true;
					}
				}
			}

			if ( testPerformance )
			{
				TestPerformance();
				waitAtEnd = true;
			}
			else
			{
				try
				{
					Console.WriteLine( "===== Test::begin =====" );
					CoreExtensions.Host.InitializeService();
					string packageName = assembly.Location;
					testSucceed = fullTest ?
						StartFullTest( packageName ) :
						StartTest( packageName );
				}
				catch ( Exception e )
				{
					Console.WriteLine( e.Message );
					Console.WriteLine( e.StackTrace );
					waitBecauseOfError = true;
				}
				finally
				{
					Console.WriteLine( "===== Test::end =====" );
				}
			}

			if ( testSucceed && !waitAtEnd && !waitBecauseOfError )
			{
				return;
			}

			Console.WriteLine( "press any key to quit ..." );
			Console.ReadKey( true );
		} // Main

		// ----------------------------------------------------------------------
		private static bool StartTest( string packageName )
		{
			Console.WriteLine( ">>>>> Testing start " + ClockProxy.Clock.Now.ToShortDateString() + " <<<<<" );
			return TestPackage( packageName );
		} // StartTest

		// ----------------------------------------------------------------------
		private static bool StartFullTest( string packageName )
		{
			int errorCount = 0;
			foreach ( int year in GetTestYears() )
			{
				foreach ( DateTime testMoment in GetTestMoments( year ) )
				{
					ClockProxy.Clock = new StaticClock( testMoment );
					Console.WriteLine( ">>>>> Testing year " + year + " - " + testMoment.ToShortDateString() + " <<<<<" );
					if ( TestPackage( packageName ) == false )
					{
						errorCount++;
					}
				}
			}
			return errorCount == 0;
		} // StartFullTest

		// ----------------------------------------------------------------------
		private static void TestPerformance()
		{
			PerformanceTest performanceTest = new PerformanceTest();

			// gap
			performanceTest.GapCalculator2( 1000000 );
			performanceTest.GapCalculator2( 10000000 );
			performanceTest.GapCalculator4( 10000000 );
			performanceTest.GapCalculator8( 10000000 );
			performanceTest.GapCalculator16( 1000000 );
			performanceTest.GapCalculator32( 1000000 );

			// combine
			performanceTest.Combiner5( 1000000 );
			performanceTest.Combiner5( 10000000 );

			// intersection
			performanceTest.Intersector4( 1000000 );
			performanceTest.Intersector4( 10000000 );

			performanceTest.TimeGapCalculator( 500000 );

		} // TestPerformance

		// ----------------------------------------------------------------------
		private static bool TestPackage( string packageName )
		{
			bool success = true;

			SimpleTestRunner runner = new SimpleTestRunner();
			TestPackage package = new TestPackage( packageName );
			if ( runner.Load( package ) )
			{
				TestResult result = runner.Run( new NullListener() );

				if ( result.IsSuccess )
				{
					Console.WriteLine( "tests finished successfully" );
				}
				else
				{
					success = false;
					TestResultReport testReport = new TestResultReport( result );
					foreach ( string failedTest in testReport.FailedTests )
					{
						Console.WriteLine( "failed test: {0}", failedTest );
					}
					foreach ( string errorTests in testReport.ErrorTests )
					{
						Console.WriteLine( "error test: {0}", errorTests );
					}
				}
			}
			return success;
		} // TestPackage

		// ----------------------------------------------------------------------
		// returns a leap and a non-leap year
		private static IEnumerable<int> GetTestYears()
		{
			List<int> testYears = new List<int>();

			// current year
			int year = DateTime.Now.Year;
			testYears.Add( year );

			// find next leap/non-leap year
			bool isLeapYaer = DateTime.IsLeapYear( year );
			while ( DateTime.IsLeapYear( year ) == isLeapYaer )
			{
				year++;
			}
			testYears.Add( year );

			return testYears;
		} // GetTestYears

		// ----------------------------------------------------------------------
		// returns some specific test dates
		private static IEnumerable<DateTime> GetTestMoments( int year )
		{
			List<DateTime> testMoments = new List<DateTime> { new DateTime( year, 1, 1 ) };

			if ( DateTime.IsLeapYear( year ) )
			{
				testMoments.Add( new DateTime( year, 2, 29 ) ); // additionay leap year day
			}
			testMoments.Add( new DateTime( year, 3, 31 ) ); // last day of 1st year quarter
			testMoments.Add( new DateTime( year, 4, 1 ) ); // first day of 2nd year quarter
			testMoments.Add( new DateTime( year, 6, 30 ) ); // last day of 2nd year quarter
			testMoments.Add( new DateTime( year, 7, 1 ) ); // first day of 3th year quarter
			testMoments.Add( new DateTime( year, 9, 30 ) ); // last day of 3th year quarter
			testMoments.Add( new DateTime( year, 10, 1 ) ); // first day of 4th year quarter
			testMoments.Add( new DateTime( year, 12, 31 ) ); // last day of year

			return testMoments;
		} // GetTestMoments

	} // class Program

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
