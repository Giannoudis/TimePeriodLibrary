// -- FILE ------------------------------------------------------------------
// name       : TimeLineMoment.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.03.31
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;

namespace Itenso.TimePeriod
{

	// ------------------------------------------------------------------------
	public class TimeLineMoment : ITimeLineMoment
	{

		// ----------------------------------------------------------------------
		public TimeLineMoment( DateTime moment )
		{
			this.moment = moment;
		} // TimeLineMoment

		// ----------------------------------------------------------------------
		public DateTime Moment
		{
			get { return moment; }
		} // Moment

		// ----------------------------------------------------------------------
		public int BalanceCount
		{
			get { return startCount - endCount; }
		} // BalanceCount

		// ----------------------------------------------------------------------
		public int StartCount
		{
			get { return startCount; }
		} // StartCount

		// ----------------------------------------------------------------------
		public int EndCount
		{
			get { return endCount; }
		} // EndCount

		// ----------------------------------------------------------------------
		public bool IsEmpty
		{
			get { return startCount == 0 && endCount == 0; }
		} // IsEmpty

		// ----------------------------------------------------------------------
		public void AddStart()
		{
			startCount++;
		} // AddStart

		// ----------------------------------------------------------------------
		public void RemoveStart()
		{
			if ( startCount == 0 )
			{
				throw new InvalidOperationException();
			}
			startCount--;
		} // RemoveStart

		// ----------------------------------------------------------------------
		public void AddEnd()
		{
			endCount++;
		} // AddEnd

		// ----------------------------------------------------------------------
		public void RemoveEnd()
		{
			if ( endCount == 0 )
			{
				throw new InvalidOperationException();
			}
			endCount--;
		} // RemoveEnd

		// ----------------------------------------------------------------------
		public override string ToString()
		{
			return string.Format( "{0} [{1}/{2}]", Moment, StartCount, EndCount );
		} // ToString

		// ----------------------------------------------------------------------
		// members
		private readonly DateTime moment;
		private int startCount;
		private int endCount;

	} // class TimeLineMoment

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
