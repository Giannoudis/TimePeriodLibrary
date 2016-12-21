// -- FILE ------------------------------------------------------------------
// name       : TimeZoneDurationProvider.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2013.11.03
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;

namespace Itenso.TimePeriod
{

	// ------------------------------------------------------------------------
	public class TimeZoneDurationProvider : DurationProvider
	{

		// ----------------------------------------------------------------------
		public TimeZoneDurationProvider( TimeZoneInfo timeZone = null,
			Func<TimeZoneInfo, DateTime, TimeSpan[], DateTime> ambigiousMoment = null,
			Func<TimeZoneInfo, DateTime, DateTime> invalidMoment = null )
		{
			if ( timeZone == null )
			{
				timeZone = TimeZoneInfo.Local;
			}

			this.timeZone = timeZone;
			this.ambigiousMoment = ambigiousMoment;
			this.invalidMoment = invalidMoment;
		} // TimeZoneDurationProvider

		// ----------------------------------------------------------------------
		public TimeZoneInfo TimeZone
		{
			get { return timeZone; }
		} // TimeZone

		// ----------------------------------------------------------------------
		public Func<TimeZoneInfo, DateTime, TimeSpan[], DateTime> AmbigiousMoment
		{
			get { return ambigiousMoment; }
		} // AmbigiousMoment

		// ----------------------------------------------------------------------
		public Func<TimeZoneInfo, DateTime, DateTime> InvalidMoment
		{
			get { return invalidMoment; }
		} // InvalidMoment

		// ----------------------------------------------------------------------
		public override TimeSpan GetDuration( DateTime start, DateTime end )
		{
			if ( timeZone.SupportsDaylightSavingTime )
			{
				// start
				if ( timeZone.IsAmbiguousTime( start ) )
				{
					start = OnAmbiguousMoment( start );
				}
				else if ( timeZone.IsInvalidTime( start ) )
				{
					start = OnInvalidMoment( start );
				}

				// end
				if ( timeZone.IsAmbiguousTime( end ) )
				{
					end = OnAmbiguousMoment( end );
				}
				else if ( timeZone.IsInvalidTime( end ) )
				{
					end = OnInvalidMoment( end );
				}
			}

			return base.GetDuration( start, end );
		} // GetDuration

		// ----------------------------------------------------------------------
		protected virtual DateTime OnAmbiguousMoment( DateTime moment )
		{
			if ( ambigiousMoment == null )
			{
				throw new AmbiguousMomentException( moment );
			}
			return ambigiousMoment( timeZone, moment, timeZone.GetAmbiguousTimeOffsets( moment ) );
		} // OnAmbiguousMoment

		// ----------------------------------------------------------------------
		protected virtual DateTime OnInvalidMoment( DateTime moment )
		{
			if ( invalidMoment == null )
			{
				throw new InvalidMomentException( moment );
			}
			return invalidMoment( timeZone, moment );
		} // OnInvalidMoment

		// ----------------------------------------------------------------------
		// members
		private readonly TimeZoneInfo timeZone;
		private readonly Func<TimeZoneInfo, DateTime, TimeSpan[], DateTime> ambigiousMoment;
		private readonly Func<TimeZoneInfo, DateTime, DateTime> invalidMoment;

	} // class TimeDurationProvider

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
