// -- FILE ------------------------------------------------------------------
// name       : TimePeriodDemoData.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;
using System.Globalization;
using System.Threading;
using Itenso.TimePeriod;

namespace Itenso.TimePeriodDemo
{

	// ------------------------------------------------------------------------
	internal class TimePeriodDemoData
	{

		// ----------------------------------------------------------------------
		public TimePeriodDemoData()
		{
			Reset();
		} // TimePeriodDemoData

		// ----------------------------------------------------------------------
		public TimeCalendarConfig CalendarConfig
		{
			get
			{
				return new TimeCalendarConfig
				       	{
				       		Culture = culture,
									YearBaseMonth = YearBaseMonth,
									YearWeekType = YearWeekType
				       	};
			}
		} // CalendarConfig

		// ----------------------------------------------------------------------
		public DateTime SetupDate { get; private set; }

		// ----------------------------------------------------------------------
		public int PeriodCount { get; private set; }

		// ----------------------------------------------------------------------
		public int Year { get; private set; }

		// ----------------------------------------------------------------------
		public YearHalfyear Halfyear { get; private set; }

		// ----------------------------------------------------------------------
		public YearQuarter Quarter { get; private set; }

		// ----------------------------------------------------------------------
		public YearMonth Month { get; private set; }

		// ----------------------------------------------------------------------
		public int Week { get; private set; }

		// ----------------------------------------------------------------------
		public int Day { get; private set; }

		// ----------------------------------------------------------------------
		public int Hour { get; private set; }

		// ----------------------------------------------------------------------
		public int Minute { get; private set; }

		// ----------------------------------------------------------------------
		private string CultureName { get; set; }

		// ----------------------------------------------------------------------
		private YearMonth YearBaseMonth { get; set; }

		// ----------------------------------------------------------------------
		private YearWeekType YearWeekType { get; set; }

		// ----------------------------------------------------------------------
		public bool QueryCulture()
		{
			string cultureName = ConsoleTool.QueryText( "Culture [enter=" + CultureInfo.CurrentCulture.Name + "]: ", CultureInfo.CurrentCulture.Name );
			if ( cultureName == null )
			{
				return false;
			}
			if ( UpdateCulture( cultureName ) == false )
			{
				return false;
			}
			return true;
		} // QueryCulture

		// ----------------------------------------------------------------------
		public bool QueryPeriodCount()
		{
			int? periodCount = ConsoleTool.QueryNumber( "Period count [enter=" + PeriodCount + "]: ", PeriodCount, 1, 10000 );
			if ( !periodCount.HasValue )
			{
				return false;
			}
			PeriodCount = periodCount.Value;
			return true;
		} // QueryPeriodCount

		// ----------------------------------------------------------------------
		public bool QueryYear()
		{
			int? year = ConsoleTool.QueryNumber( "Year [enter=" + Year + "]: ", Year, DateTime.MinValue.Year, DateTime.MaxValue.Year );
			if ( !year.HasValue )
			{
				return false;
			}
			Year = year.Value;
			return true;
		} // QueryYear

		// ----------------------------------------------------------------------
		public bool QueryYearBaseMonth()
		{
			int? yearStartMonth = ConsoleTool.QueryNumber( "Year start month (1..12) [enter=" + (int)YearBaseMonth + "/" + YearBaseMonth + "]: ",
				(int)YearBaseMonth, 1, TimeSpec.MonthsPerYear );
			if ( !yearStartMonth.HasValue )
			{
				return false;
			}
			YearBaseMonth = (YearMonth)yearStartMonth.Value;
			return true;
		} // QueryYearBaseMonth

		// ----------------------------------------------------------------------
		public bool QueryYearHalfyear()
		{
			int? halfyear = ConsoleTool.QueryNumber( "Halfyear (1..2) [enter=" + (int)Halfyear + "/" + Halfyear + "]: ",
				(int)Halfyear, 1, TimeSpec.HalfyearsPerYear );
			if ( !halfyear.HasValue )
			{
				return false;
			}
			Halfyear = (YearHalfyear)halfyear.Value;
			return true;
		} // QueryYearHalfyear

		// ----------------------------------------------------------------------
		public bool QueryYearQuarter()
		{
			int? yearQuarter = ConsoleTool.QueryNumber( "Quarter (1..4) [enter=" + (int)Quarter + "/" + Quarter + "]: ",
				(int)Quarter, 1, TimeSpec.QuartersPerYear );
			if ( !yearQuarter.HasValue )
			{
				return false;
			}
			Quarter = (YearQuarter)yearQuarter.Value;
			return true;
		} // QueryYearQuarter

		// ----------------------------------------------------------------------
		public bool QueryYearMonth()
		{
			int? yearMonth = ConsoleTool.QueryNumber( "Month (1..12) [enter=" + (int)Month + "/" + Month + "]: ",
				(int)Month, 1, TimeSpec.QuartersPerYear );
			if ( !yearMonth.HasValue )
			{
				return false;
			}
			Month = (YearMonth)yearMonth.Value;
			return true;
		} // QueryYearMonth

		// ----------------------------------------------------------------------
		public bool QueryWeek()
		{
			int? week = ConsoleTool.QueryNumber( "Week (1..53) [enter=" + Week + "]: ", Week, 1, 53 );
			if ( !week.HasValue )
			{
				return false;
			}
			Week = week.Value;
			return true;
		} // QueryWeek

		// ----------------------------------------------------------------------
		public bool QueryYearWeekType()
		{
			int? weekType = ConsoleTool.QueryNumber( "Week type (0..1) [enter=" + (int)YearWeekType + "/" + YearWeekType + "]: ",
				(int)YearWeekType, 0, 2 );
			if ( !weekType.HasValue )
			{
				return false;
			}
			YearWeekType = (YearWeekType)weekType.Value;
			return true;
		} // QueryYearWeekType

		// ----------------------------------------------------------------------
		public bool QueryDay()
		{
			int? day = ConsoleTool.QueryNumber( "Day (1..31) [enter=" + Day + "]: ", Day, 1, 31 );
			if ( !day.HasValue )
			{
				return false;
			}
			Day = day.Value;
			return true;
		} // QueryDay

		// ----------------------------------------------------------------------
		public bool QueryHour()
		{
			int? hour = ConsoleTool.QueryNumber( "Hour (0..23) [enter=" + Hour + "]: ", Hour, 0, TimeSpec.HoursPerDay - 1 );
			if ( !hour.HasValue )
			{
				return false;
			}
			Hour = hour.Value;
			return true;
		} // QueryHour

		// ----------------------------------------------------------------------
		public bool QueryMinute()
		{
			int? minute = ConsoleTool.QueryNumber( "Minute (0..59) [enter=" + Minute + "]: ", Minute, 0, TimeSpec.MinutesPerHour - 1 );
			if ( !minute.HasValue )
			{
				return false;
			}
			Minute = minute.Value;
			return true;
		} // QueryMinute

		// ----------------------------------------------------------------------
		public void Reset()
		{
			Reset( SetupDate );
		} // Reset

		// ----------------------------------------------------------------------
		public void Reset( DateTime dateTime )
		{
			culture = CultureInfo.CurrentCulture;
			CultureName = culture.Name;
			YearBaseMonth = TimeSpec.CalendarYearStartMonth;
			int year;
			int weekOfYear;
			TimeTool.GetWeekOfYear( SetupDate, culture, YearWeekType, out year, out weekOfYear );

			PeriodCount = 1;
			SetupDate = ClockProxy.Clock.Now;
			Year = SetupDate.Year;
			Halfyear = TimeTool.GetHalfyearOfMonth( YearBaseMonth );
			Quarter = TimeTool.GetQuarterOfMonth( YearBaseMonth );
			Month = (YearMonth)SetupDate.Month;

			Week = weekOfYear;
			Day = SetupDate.Day;
			Hour = SetupDate.Hour;
			Minute = SetupDate.Minute;
		} // Reset

		// ----------------------------------------------------------------------
		private bool UpdateCulture( string cultureName )
		{
			try
			{
				culture = new CultureInfo( cultureName );
			}
			catch ( Exception e )
			{
				ConsoleTool.WriteLine( e.Message );
				return false;
			}
			CultureName = cultureName;
			return true;
		} // UpdateCulture

		// ----------------------------------------------------------------------
		// members
		private CultureInfo culture;

	} // class TimePeriodDemoData

} // namespace Itenso.TimePeriodDemo
// -- EOF -------------------------------------------------------------------
