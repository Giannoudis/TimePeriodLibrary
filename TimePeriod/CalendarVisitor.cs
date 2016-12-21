// -- FILE ------------------------------------------------------------------
// name       : CalendarVisitor.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.03.21
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;

namespace Itenso.TimePeriod
{

	// ------------------------------------------------------------------------
	public abstract class CalendarVisitor<TFilter, TContext>
		where TFilter : class, ICalendarVisitorFilter
		where TContext : class, ICalendarVisitorContext
	{

		// ------------------------------------------------------------------------
		protected CalendarVisitor( TFilter filter, ITimePeriod limits,
			SeekDirection seekDirection = SeekDirection.Forward, ITimeCalendar calendar = null )
		{
			if ( filter == null )
			{
				throw new ArgumentNullException( "filter" );
			}
			if ( limits == null )
			{
				throw new ArgumentNullException( "limits" );
			}

			this.filter = filter;
			this.limits = limits;
			this.seekDirection = seekDirection;
			this.calendar = calendar;
		} // CalendarVisitor

		// ----------------------------------------------------------------------
		public TFilter Filter
		{
			get { return filter; }
		} // Filter

		// ----------------------------------------------------------------------
		public ITimePeriod Limits
		{
			get { return limits; }
		} // Limits

		// ----------------------------------------------------------------------
		public SeekDirection SeekDirection
		{
			get { return seekDirection; }
		} // SeekDirection

		// ----------------------------------------------------------------------
		public ITimeCalendar Calendar
		{
			get { return calendar; }
		} // Calendar

		// ----------------------------------------------------------------------
		protected void StartPeriodVisit( TContext context = null )
		{
			StartPeriodVisit( limits, context );
		} // StartPeriodVisit

		// ----------------------------------------------------------------------
		protected void StartPeriodVisit( ITimePeriod period, TContext context = null )
		{
			if ( period == null )
			{
				throw new ArgumentNullException( "period" );
			}
			if ( period.IsMoment )
			{
				return;
			}

			OnVisitStart();

			Years years = calendar != null ?
				new Years( period.Start.Year, period.End.Year - period.Start.Year + 1, calendar ) :
				new Years( period.Start.Year, period.End.Year - period.Start.Year + 1 );
			if ( OnVisitYears( years, context ) && EnterYears( years, context ) )
			{
				ITimePeriodCollection yearsToVisit = years.GetYears();
				if ( seekDirection == SeekDirection.Backward )
				{
					yearsToVisit.SortByEnd();
				}
				foreach ( Year year in yearsToVisit )
				{
					if ( !year.OverlapsWith( period ) || OnVisitYear( year, context ) == false )
					{
						continue;
					}

					// months
					if ( EnterMonths( year, context ) == false )
					{
						continue;
					}
					ITimePeriodCollection monthsToVisit = year.GetMonths();
					if ( seekDirection == SeekDirection.Backward )
					{
						monthsToVisit.SortByEnd();
					}
					foreach ( Month month in monthsToVisit )
					{
						if ( !month.OverlapsWith( period ) || OnVisitMonth( month, context ) == false )
						{
							continue;
						}

						// days
						if ( EnterDays( month, context ) == false )
						{
							continue;
						}
						ITimePeriodCollection daysToVisit = month.GetDays();
						if ( seekDirection == SeekDirection.Backward )
						{
							daysToVisit.SortByEnd();
						}
						foreach ( Day day in daysToVisit )
						{
							if ( !day.OverlapsWith( period ) || OnVisitDay( day, context ) == false )
							{
								continue;
							}

							// hours
							if ( EnterHours( day, context ) == false )
							{
								continue;
							}
							ITimePeriodCollection hoursToVisit = day.GetHours();
							if ( seekDirection == SeekDirection.Backward )
							{
								hoursToVisit.SortByEnd();
							}
							foreach ( Hour hour in hoursToVisit )
							{
								if ( !hour.OverlapsWith( period ) || OnVisitHour( hour, context ) == false )
								{
// ReSharper disable RedundantJumpStatement
									continue;
// ReSharper restore RedundantJumpStatement
								}
							}
						}
					}
				}
			}

			OnVisitEnd();
		} // StartPeriodVisit

		// ----------------------------------------------------------------------
		protected Year StartYearVisit( Year year, TContext context = null, SeekDirection? visitDirection = null )
		{
			if ( year == null )
			{
				throw new ArgumentNullException( "year" );
			}

			if ( visitDirection == null )
			{
				visitDirection = SeekDirection;
			}

			OnVisitStart();

			// iteration limits
			Year lastVisited = null;
			DateTime minStart = DateTime.MinValue;
			DateTime maxEnd = DateTime.MaxValue.AddYears( -1 );
			while ( year.Start > minStart && year.End < maxEnd )
			{
				if ( OnVisitYear( year, context ) == false )
				{
					lastVisited = year;
					break;
				}
				switch ( visitDirection )
				{
					case SeekDirection.Forward:
						year = year.GetNextYear();
						break;
					case SeekDirection.Backward:
						year = year.GetPreviousYear();
						break;
				}
			}

			OnVisitEnd();

			return lastVisited;
		} // StartYearVisit

		// ----------------------------------------------------------------------
		protected Month StartMonthVisit( Month month, TContext context = null, SeekDirection? visitDirection = null )
		{
			if ( month == null )
			{
				throw new ArgumentNullException( "month" );
			}

			if ( visitDirection == null )
			{
				visitDirection = SeekDirection;
			}

			OnVisitStart();

			// iteration limits
			Month lastVisited = null;
			DateTime minStart = DateTime.MinValue;
			DateTime maxEnd = DateTime.MaxValue.AddMonths( -1 );
			while ( month.Start > minStart && month.End < maxEnd )
			{
				if ( OnVisitMonth( month, context ) == false )
				{
					lastVisited = month;
					break;
				}
				switch ( visitDirection )
				{
					case SeekDirection.Forward:
						month = month.GetNextMonth();
						break;
					case SeekDirection.Backward:
						month = month.GetPreviousMonth();
						break;
				}
			}

			OnVisitEnd();

			return lastVisited;
		} // StartMonthVisit

		// ----------------------------------------------------------------------
		protected Day StartDayVisit( Day day, TContext context = null, SeekDirection? visitDirection = null )
		{
			if ( day == null )
			{
				throw new ArgumentNullException( "day" );
			}

			if ( visitDirection == null )
			{
				visitDirection = SeekDirection;
			}

			OnVisitStart();

			// iteration limits
			Day lastVisited = null;
			DateTime minStart = DateTime.MinValue;
			DateTime maxEnd = DateTime.MaxValue.AddDays( -1 );
			while ( day.Start > minStart && day.End < maxEnd )
			{
				if ( OnVisitDay( day, context ) == false )
				{
					lastVisited = day;
					break;
				}
				switch ( visitDirection )
				{
					case SeekDirection.Forward:
						day = day.GetNextDay();
						break;
					case SeekDirection.Backward:
						day = day.GetPreviousDay();
						break;
				}
			}

			OnVisitEnd();

			return lastVisited;
		} // StartDayVisit

		// ----------------------------------------------------------------------
		protected Hour StartHourVisit( Hour hour, TContext context = null, SeekDirection? visitDirection = null )
		{
			if ( hour == null )
			{
				throw new ArgumentNullException( "hour" );
			}

			if ( visitDirection == null )
			{
				visitDirection = SeekDirection;
			}

			OnVisitStart();

			// iteration limits
			Hour lastVisited = null;
			DateTime minStart = DateTime.MinValue;
			DateTime maxEnd = DateTime.MaxValue.AddHours( -1 );
			while ( hour.Start > minStart && hour.End < maxEnd )
			{
				if ( OnVisitHour( hour, context ) == false )
				{
					lastVisited = hour;
					break;
				}
				switch ( visitDirection )
				{
					case SeekDirection.Forward:
						hour = hour.GetNextHour();
						break;
					case SeekDirection.Backward:
						hour = hour.GetPreviousHour();
						break;
				}
			}

			OnVisitEnd();

			return lastVisited;
		} // StartHourVisit

		// ----------------------------------------------------------------------
		protected virtual void OnVisitStart()
		{
		} // OnVisitStart

		// ----------------------------------------------------------------------
		protected virtual bool CheckLimits( ITimePeriod test )
		{
			return limits.HasInside( test );
		} // CheckLimits

		// ----------------------------------------------------------------------
		protected virtual bool CheckExcludePeriods( ITimePeriod test )
		{
			if ( filter.ExcludePeriods.Count == 0 )
			{
				return true;
			}
			return filter.ExcludePeriods.OverlapPeriods( test ).Count == 0;
		} // CheckExcludePeriods

		// ----------------------------------------------------------------------
		protected virtual bool EnterYears( Years years, TContext context )
		{
			return true;
		} // EnterYears

		// ----------------------------------------------------------------------
		protected virtual bool EnterMonths( Year year, TContext context )
		{
			return true;
		} // EnterMonths

		// ----------------------------------------------------------------------
		protected virtual bool EnterDays( Month month, TContext context )
		{
			return true;
		} // EnterDays

		// ----------------------------------------------------------------------
		protected virtual bool EnterHours( Day day, TContext context )
		{
			return true;
		} // EnterHours

		// ----------------------------------------------------------------------
		protected virtual bool OnVisitYears( Years years, TContext context )
		{
			return true;
		} // OnVisitYears

		// ----------------------------------------------------------------------
		protected virtual bool OnVisitYear( Year year, TContext context )
		{
			return true;
		} // OnVisitYear

		// ----------------------------------------------------------------------
		protected virtual bool OnVisitMonth( Month month, TContext context )
		{
			return true;
		} // OnVisitMonth

		// ----------------------------------------------------------------------
		protected virtual bool OnVisitDay( Day day, TContext context )
		{
			return true;
		} // OnVisitDay

		// ----------------------------------------------------------------------
		protected virtual bool OnVisitHour( Hour hour, TContext context )
		{
			return true;
		} // OnVisitHour

		// ----------------------------------------------------------------------
		protected virtual bool IsMatchingYear( Year year, TContext context )
		{
			// year filter
			if ( filter.Years.Count > 0 && !filter.Years.Contains( year.YearValue ) )
			{
				return false;
			}

			return CheckExcludePeriods( year );
		} // IsMatchingYear

		// ----------------------------------------------------------------------
		protected virtual bool IsMatchingMonth( Month month, TContext context )
		{
			// year filter
			if ( filter.Years.Count > 0 && !filter.Years.Contains( month.Year ) )
			{
				return false;
			}

			// month filter
			if ( filter.Months.Count > 0 && !filter.Months.Contains( month.YearMonth ) )
			{
				return false;
			}

			return CheckExcludePeriods( month );
		} // IsMatchingMonth

		// ----------------------------------------------------------------------
		protected virtual bool IsMatchingDay( Day day, TContext context )
		{
			// year filter
			if ( filter.Years.Count > 0 && !filter.Years.Contains( day.Year ) )
			{
				return false;
			}

			// month filter
			if ( filter.Months.Count > 0 && !filter.Months.Contains( (YearMonth)day.Month ) )
			{
				return false;
			}

			// day filter
			if ( filter.Days.Count > 0 && !filter.Days.Contains( day.DayValue ) )
			{
				return false;
			}

			// weekday filter
			if ( filter.WeekDays.Count > 0 && !filter.WeekDays.Contains( day.DayOfWeek ) )
			{
				return false;
			}

			return CheckExcludePeriods( day );
		} // IsMatchingDay

		// ----------------------------------------------------------------------
		protected virtual bool IsMatchingHour( Hour hour, TContext context )
		{
			// year filter
			if ( filter.Years.Count > 0 && !filter.Years.Contains( hour.Year ) )
			{
				return false;
			}

			// month filter
			if ( filter.Months.Count > 0 && !filter.Months.Contains( (YearMonth)hour.Month ) )
			{
				return false;
			}

			// day filter
			if ( filter.Days.Count > 0 && !filter.Days.Contains( hour.Day ) )
			{
				return false;
			}

			// weekday filter
			if ( filter.WeekDays.Count > 0 && !filter.WeekDays.Contains( hour.Start.DayOfWeek ) )
			{
				return false;
			}

			// hour filter
			if ( filter.Hours.Count > 0 && !filter.Hours.Contains( hour.HourValue ) )
			{
				return false;
			}

			return CheckExcludePeriods( hour );
		} // IsMatchingHour

		// ----------------------------------------------------------------------
		protected virtual void OnVisitEnd()
		{
		} // OnVisitEnd

		// ----------------------------------------------------------------------
		// members
		private readonly TFilter filter;
		private readonly ITimePeriod limits;
		private readonly SeekDirection seekDirection;
		private readonly ITimeCalendar calendar;

	} // class CalendarVisitor

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
