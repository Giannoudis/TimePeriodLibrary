// -- FILE ------------------------------------------------------------------
// name       : PlayTimeRange.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2012.03.07
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;
using Itenso.TimePeriod;

namespace Itenso.TimePeriodDemo.Player
{

	// ------------------------------------------------------------------------
	public class PlayTimeRange : TimeRange, IPlayTimeRange
	{

		// ----------------------------------------------------------------------
		public PlayTimeRange() :
			this( TimeSpec.MinPeriodDate, TimeSpec.MaxPeriodDate )
		{
		} // PlayTimeRange

		// ----------------------------------------------------------------------
		public PlayTimeRange( DateTime start, DateTime end, PlayDirection direction = PlayDirection.Forward ) :
			base( start, end )
		{
			this.direction = direction;
		} // PlayTimeRange

		// ----------------------------------------------------------------------
		public PlayTimeRange( IPlayTimeRange copy ) :
			base( copy )
		{
			direction = copy.Direction;
		} // PlayTimeRange

		// ----------------------------------------------------------------------
		public PlayDirection Direction
		{
			get { return direction; }
			set { direction = value; }
		} // Direction

		// ----------------------------------------------------------------------
		public DateTime PlayStart
		{
			get { return Direction == PlayDirection.Forward ? Start : End; }
			set
			{
				if ( Direction == PlayDirection.Forward )
				{
					Start = value;
				}
				else
				{
					End = value;
				}
			}
		} // PlayStart

		// ----------------------------------------------------------------------
		public DateTime PlayEnd
		{
			get { return Direction == PlayDirection.Forward ? End : Start; }
			set
			{
				if ( Direction == PlayDirection.Forward )
				{
					End = value;
				}
				else
				{
					Start = value;
				}
			}
		} // PlayEnd

		// ----------------------------------------------------------------------
		public void RevertDirection()
		{
			direction = direction == PlayDirection.Forward ? PlayDirection.Backward : PlayDirection.Forward;
		} // RevertDirection

		// ----------------------------------------------------------------------
		public override void Setup( DateTime newStart, DateTime newEnd )
		{
			base.Setup( newStart, newEnd );
			Direction = newStart <= newEnd ? PlayDirection.Forward : PlayDirection.Backward;
		} // Setup
		 
		// ----------------------------------------------------------------------
		public override bool IsSamePeriod( ITimePeriod test )
		{
			IPlayTimeRange playTimeRange = (IPlayTimeRange)test;
			return base.IsSamePeriod( playTimeRange ) && direction == playTimeRange.Direction;
		} // IsSamePeriod

		// ----------------------------------------------------------------------
		protected override string Format( ITimeFormatter formatter )
		{
			return string.Format( "{0}{1}", base.Format( formatter ), Direction == PlayDirection.Forward ? ">" : "<" );
		} // Format

		// ----------------------------------------------------------------------
		protected override bool IsEqual( object obj )
		{
			return base.IsEqual( obj ) && HasSameData( obj as PlayTimeRange );
		} // IsEqual

		// ----------------------------------------------------------------------
		private bool HasSameData( PlayTimeRange comp )
		{
			return direction == comp.direction;
		} // HasSameData

		// ----------------------------------------------------------------------
		protected override int ComputeHashCode()
		{
			return HashTool.ComputeHashCode( base.ComputeHashCode(), direction );
		} // ComputeHashCode

		// ----------------------------------------------------------------------
		// members
		private PlayDirection direction;

	} // class PlayTimeRange

} // namespace Itenso.TimePeriodDemo.Player
// -- EOF -------------------------------------------------------------------
