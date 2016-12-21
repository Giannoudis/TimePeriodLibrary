// -- FILE ------------------------------------------------------------------
// name       : TimePeriodObservableCollection.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using Itenso.TimePeriod;

namespace Itenso.CalendarPeriodCollector
{

	// ------------------------------------------------------------------------
	public class TimePeriodObservableCollection : ObservableCollection<ITimePeriod>
	{

		// ----------------------------------------------------------------------
		public void CopyToClipboard()
		{
			StringBuilder sb = new StringBuilder();

			sb.Append( "Period" );

			sb.Append( clipboardDelimiter );
			sb.Append( "Start" );
			sb.Append( clipboardDelimiter );
			sb.Append( "StartDate" );
			sb.Append( clipboardDelimiter );
			sb.Append( "StartTime" );

			sb.Append( clipboardDelimiter );
			sb.Append( "End" );
			sb.Append( clipboardDelimiter );
			sb.Append( "EndDate" );
			sb.Append( clipboardDelimiter );
			sb.Append( "EndTime" );

			sb.Append( clipboardDelimiter );
			sb.Append( "Duration" );
			sb.Append( clipboardDelimiter );
			sb.Append( "DurationDays" );
			sb.Append( clipboardDelimiter );
			sb.Append( "DurationHours" );
			sb.Append( clipboardDelimiter );
			sb.Append( "DurationMinutes" );
			sb.Append( clipboardDelimiter );
			sb.Append( "DurationTotalDays" );
			sb.Append( clipboardDelimiter );
			sb.Append( "DurationTotalHours" );
			sb.Append( clipboardDelimiter );
			sb.Append( "DurationTotalMinutes" );

			if ( Count == 0 )
			{
				return;
			}
			sb.Append( Environment.NewLine );

			foreach ( ITimePeriod collectedPeriod in this )
			{
				sb.Append( '"' );
				sb.Append( collectedPeriod );
				sb.Append( '"' );
				sb.Append( clipboardDelimiter );

				sb.Append( collectedPeriod.Start );
				sb.Append( clipboardDelimiter );
				sb.Append( collectedPeriod.Start.ToShortDateString() );
				sb.Append( clipboardDelimiter );
				sb.Append( collectedPeriod.Start.ToShortTimeString() );
				sb.Append( clipboardDelimiter );

				sb.Append( collectedPeriod.End );
				sb.Append( clipboardDelimiter );
				sb.Append( collectedPeriod.End.ToShortDateString() );
				sb.Append( clipboardDelimiter );
				sb.Append( collectedPeriod.End.ToShortTimeString() );
				sb.Append( clipboardDelimiter );

				TimeSpan duration = collectedPeriod.Duration;
				sb.Append( duration.ToString( "d\\.hh\\:mm" ) );
				sb.Append( clipboardDelimiter );
				sb.Append( duration.Days );
				sb.Append( clipboardDelimiter );
				sb.Append( duration.Hours );
				sb.Append( clipboardDelimiter );
				sb.Append( duration.Minutes );
				sb.Append( clipboardDelimiter );
				sb.Append( duration.TotalDays.ToString( "0.####" ) );
				sb.Append( clipboardDelimiter );
				sb.Append( duration.TotalHours.ToString( "0.####" ) );
				sb.Append( clipboardDelimiter );
				sb.Append( duration.TotalMinutes.ToString( "0.####" ) );

				sb.Append( Environment.NewLine );
			}

			Clipboard.SetText( sb.ToString() );
		} // UpdateClipboard

		// ----------------------------------------------------------------------
		// members
		private const string clipboardDelimiter = "\t";

	} // class TimePeriodObservableCollection

} // namespace Itenso.CalendarPeriodCollector
// -- EOF -------------------------------------------------------------------
