// -- FILE ------------------------------------------------------------------
// name       : SchoolTestData.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;
using Itenso.TimePeriod;

namespace Itenso.TimePeriodTests
{

	// ------------------------------------------------------------------------
	public class Lesson : TimeBlock
	{

		// ----------------------------------------------------------------------
		public static TimeSpan LessonDuration = TimePeriod.Duration.Minutes( 50 );

		// ----------------------------------------------------------------------
		public Lesson( DateTime moment ) :
			base( moment, LessonDuration )
		{
		} // Lesson

	} // class Lesson

	// ------------------------------------------------------------------------
	public class ShortBreak : TimeBlock
	{

		// ----------------------------------------------------------------------
		public static TimeSpan ShortBreakDuration = TimePeriod.Duration.Minutes( 5 );

		// ----------------------------------------------------------------------
		public ShortBreak( DateTime moment ) :
			base( moment, ShortBreakDuration )
		{
		} // ShortBreak

	} // class ShortBreak

	// ------------------------------------------------------------------------
	public class LargeBreak : TimeBlock
	{

		// ----------------------------------------------------------------------
		public static TimeSpan LargeBreakDuration = TimePeriod.Duration.Minutes( 15 );

		// ----------------------------------------------------------------------
		public LargeBreak( DateTime moment ) :
			base( moment, LargeBreakDuration )
		{
		} // LargeBreak

	} // class LargeBreak


	// ------------------------------------------------------------------------
	public class SchoolDay : TimePeriodChain
	{

		// ----------------------------------------------------------------------
		public SchoolDay( IClock clock = null ) :
			this( GetDefaultStartDate( clock ) )
		{
		} // SchoolDay

		// ----------------------------------------------------------------------
		public SchoolDay( DateTime moment )
		{
			Lesson1 = new Lesson( moment );
			Break1 = new ShortBreak( moment );
			Lesson2 = new Lesson( moment );
			Break2 = new LargeBreak( moment );
			Lesson3 = new Lesson( moment );
			Break3 = new ShortBreak( moment );
			Lesson4 = new Lesson( moment );

			base.Add( Lesson1 );
			base.Add( Break1 );
			base.Add( Lesson2 );
			base.Add( Break2 );
			base.Add( Lesson3 );
			base.Add( Break3 );
			base.Add( Lesson4 );
		} // SchoolDay

		// ----------------------------------------------------------------------
		public Lesson Lesson1 { get; private set; }

		// ----------------------------------------------------------------------
		public ShortBreak Break1 { get; private set; }

		// ----------------------------------------------------------------------
		public Lesson Lesson2 { get; private set; }

		// ----------------------------------------------------------------------
		public LargeBreak Break2 { get; private set; }

		// ----------------------------------------------------------------------
		public Lesson Lesson3 { get; private set; }

		// ----------------------------------------------------------------------
		public ShortBreak Break3 { get; private set; }

		// ----------------------------------------------------------------------
		public Lesson Lesson4 { get; private set; }

		// ----------------------------------------------------------------------
		private static DateTime GetDefaultStartDate( IClock clock )
		{
			if ( clock == null )
			{
				clock = ClockProxy.Clock;
			}
			DateTime now = clock.Now;
			return new DateTime( now.Year, now.Month, now.Day, 8, 0, 0 );
		} // GetDefaultStartDate

	} // class SchoolDay

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
