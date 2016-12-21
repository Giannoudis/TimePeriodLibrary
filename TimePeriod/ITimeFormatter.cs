// -- FILE ------------------------------------------------------------------
// name       : ITimeFormatter.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.03.16
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;
using System.Globalization;

namespace Itenso.TimePeriod
{

	// ------------------------------------------------------------------------
	public interface ITimeFormatter
	{

		// ----------------------------------------------------------------------
		CultureInfo Culture { get; }

		// ----------------------------------------------------------------------
		string ListSeparator { get; }

		// ----------------------------------------------------------------------
		string ContextSeparator { get; }

		// ----------------------------------------------------------------------
		string StartEndSeparator { get; }

		// ----------------------------------------------------------------------
		string DurationSeparator { get; }

		// ----------------------------------------------------------------------
		string DurationItemSeparator { get; }

		// ----------------------------------------------------------------------
		string DurationLastItemSeparator { get; }

		// ----------------------------------------------------------------------
		string DurationValueSeparator { get; }

		// ----------------------------------------------------------------------
		string IntervalStartClosed { get; }

		// ----------------------------------------------------------------------
		string IntervalStartOpen { get; }

		// ----------------------------------------------------------------------
		string IntervalStartOpenIso { get; }

		// ----------------------------------------------------------------------
		string IntervalEndClosed { get; }

		// ----------------------------------------------------------------------
		string IntervalEndOpen { get; }

		// ----------------------------------------------------------------------
		string IntervalEndOpenIso { get; }

		// ----------------------------------------------------------------------
		string DateTimeFormat { get; }

		// ----------------------------------------------------------------------
		string ShortDateFormat { get; }

		// ----------------------------------------------------------------------
		string LongTimeFormat { get; }

		// ----------------------------------------------------------------------
		string ShortTimeFormat { get; }

		// ----------------------------------------------------------------------
		DurationFormatType DurationType { get; }

		// ----------------------------------------------------------------------
		bool UseDurationSeconds { get; }

		// ----------------------------------------------------------------------
		string GetCollection( int count );

		// ----------------------------------------------------------------------
		string GetCollectionPeriod( int count, DateTime start, DateTime end, TimeSpan duration );

		// ----------------------------------------------------------------------
		string GetDateTime( DateTime dateTime );

		// ----------------------------------------------------------------------
		string GetShortDate( DateTime dateTime );

		// ----------------------------------------------------------------------
		string GetLongTime( DateTime dateTime );

		// ----------------------------------------------------------------------
		string GetShortTime( DateTime dateTime );

		// ----------------------------------------------------------------------
		string GetPeriod( DateTime start, DateTime end );

		// ----------------------------------------------------------------------
		string GetDuration( TimeSpan timeSpan );

		// ----------------------------------------------------------------------
		string GetDuration( TimeSpan timeSpan, DurationFormatType durationFormatType );

		// ----------------------------------------------------------------------
		string GetDuration( int years, int months, int days, int hours, int minutes, int seconds );

		// ----------------------------------------------------------------------
		string GetPeriod( DateTime start, DateTime end, TimeSpan duration );

		// ----------------------------------------------------------------------
		string GetInterval( DateTime start, DateTime end, IntervalEdge startEdge, IntervalEdge endEdge, TimeSpan duration );

		// ----------------------------------------------------------------------
		string GetCalendarPeriod( string start, string end, TimeSpan duration );

		// ----------------------------------------------------------------------
		string GetCalendarPeriod( string context, string start, string end, TimeSpan duration );

		// ----------------------------------------------------------------------
		string GetCalendarPeriod( string startContext, string endContext, string start, string end, TimeSpan duration );

	} // interface ITimeFormatter

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
