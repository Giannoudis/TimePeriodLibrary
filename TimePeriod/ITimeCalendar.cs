// -- FILE ------------------------------------------------------------------
// name       : ITimeCalendar.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;
using System.Globalization;

namespace Itenso.TimePeriod
{

	// ------------------------------------------------------------------------
	public interface ITimeCalendar : ITimePeriodMapper
	{

		// ----------------------------------------------------------------------
		CultureInfo Culture { get; }

		// ----------------------------------------------------------------------
		YearType YearType { get; }

		// ----------------------------------------------------------------------
		TimeSpan StartOffset { get; }

		// ----------------------------------------------------------------------
		TimeSpan EndOffset { get; }

		// ----------------------------------------------------------------------
		YearMonth YearBaseMonth { get; }

		// ----------------------------------------------------------------------
		YearMonth FiscalYearBaseMonth { get; }

		// ----------------------------------------------------------------------
		DayOfWeek FiscalFirstDayOfYear { get; }

		// ----------------------------------------------------------------------
		FiscalYearAlignment FiscalYearAlignment { get; }

		// ----------------------------------------------------------------------
		FiscalQuarterGrouping FiscalQuarterGrouping { get; }

		// ----------------------------------------------------------------------
		DayOfWeek FirstDayOfWeek { get; }

		// ----------------------------------------------------------------------
		YearWeekType YearWeekType { get; }

		// ----------------------------------------------------------------------
		int GetYear( DateTime time );

		// ----------------------------------------------------------------------
		int GetMonth( DateTime time );

		// ----------------------------------------------------------------------
		int GetHour( DateTime time );

		// ----------------------------------------------------------------------
		int GetMinute( DateTime time );

		// ----------------------------------------------------------------------
		int GetDayOfMonth( DateTime time );

		// ----------------------------------------------------------------------
		DayOfWeek GetDayOfWeek( DateTime time );

		// ----------------------------------------------------------------------
		int GetDaysInMonth( int year, int month );

		// ----------------------------------------------------------------------
		int GetYear( int year, int month );

		// ----------------------------------------------------------------------
		string GetYearName( int year );

		// ----------------------------------------------------------------------
		string GetHalfyearName( YearHalfyear yearHalfyear );

		// ----------------------------------------------------------------------
		string GetHalfyearOfYearName( int year, YearHalfyear yearHalfyear );

		// ----------------------------------------------------------------------
		string GetQuarterName( YearQuarter yearQuarter );

		// ----------------------------------------------------------------------
		string GetQuarterOfYearName( int year, YearQuarter yearQuarter );

		// ----------------------------------------------------------------------
		string GetMonthName( int month );

		// ----------------------------------------------------------------------
		string GetMonthOfYearName( int year, int month );

		// ----------------------------------------------------------------------
		string GetWeekOfYearName( int year, int weekOfYear );

		// ----------------------------------------------------------------------
		string GetDayName( DayOfWeek dayOfWeek );

		// ----------------------------------------------------------------------
		int GetWeekOfYear( DateTime time );

		// ----------------------------------------------------------------------
		DateTime GetStartOfYearWeek( int year, int weekOfYear );

	} // class ITimeCalendar

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
