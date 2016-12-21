// -- FILE ------------------------------------------------------------------
// name       : CollectorDemoViewModel.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Input;
using Itenso.TimePeriod;

namespace Itenso.CalendarPeriodCollector
{

	// ------------------------------------------------------------------------
	public class CollectorViewModel : ViewModel, INotifyPropertyChanged, ICommandHandler
	{

		// ----------------------------------------------------------------------
		private enum ClearType
		{
			TimeRange,
			Filter,
			CollectingRange,
		} // enum ClearType

		// ----------------------------------------------------------------------
		private enum PeriodSelectType
		{
			Previous,
			Current,
			Next,
		} // enum PeriodSelectType

		// ----------------------------------------------------------------------
		private enum CollectType
		{
			Year,
			Month,
			Day,
			Hour,
		} // enum CollectType

		// ----------------------------------------------------------------------
		public static readonly DependencyProperty WorkingTimePeriodProperty = DependencyProperty.Register(
			"WorkingTimePeriod",
			typeof( TimePeriodMode ),
			typeof( CollectorViewModel ),
			new PropertyMetadata( WorkingTimePeriodModeChanged ) );

		// ----------------------------------------------------------------------
		public static readonly DependencyProperty WorkingPeriodStartDateProperty = DependencyProperty.Register(
			"WorkingPeriodStartDate",
			typeof( DateTime ),
			typeof( CollectorViewModel ),
			new PropertyMetadata( WorkingPeriodLimitsChanged ) );

		// ----------------------------------------------------------------------
		public static readonly DependencyProperty WorkingPeriodStartHourProperty = DependencyProperty.Register(
			"WorkingPeriodStartHour",
			typeof( int ),
			typeof( CollectorViewModel ),
			new PropertyMetadata( WorkingPeriodLimitsChanged ) );

		// ----------------------------------------------------------------------
		public static readonly DependencyProperty WorkingPeriodEndDateProperty = DependencyProperty.Register(
			"WorkingPeriodEnd",
			typeof( DateTime ),
			typeof( CollectorViewModel ),
			new PropertyMetadata( WorkingPeriodLimitsChanged ) );

		// ----------------------------------------------------------------------
		public static readonly DependencyProperty WorkingPeriodEndHourProperty = DependencyProperty.Register(
			"WorkingPeriodEndHour",
			typeof( int ),
			typeof( CollectorViewModel ),
			new PropertyMetadata( WorkingPeriodLimitsChanged ) );

		// ----------------------------------------------------------------------
		public static readonly DependencyProperty CollectMonthStartProperty = DependencyProperty.Register(
			"CollectMonthStart",
			typeof( YearMonth? ),
			typeof( CollectorViewModel ),
			null );

		// ----------------------------------------------------------------------
		public static readonly DependencyProperty CollectMonthEndProperty = DependencyProperty.Register(
			"CollectMonthEnd",
			typeof( YearMonth? ),
			typeof( CollectorViewModel ),
			null );

		// ----------------------------------------------------------------------
		public static readonly DependencyProperty CollectDayStartProperty = DependencyProperty.Register(
			"CollectDayStart",
			typeof( int? ),
			typeof( CollectorViewModel ),
			null );

		// ----------------------------------------------------------------------
		public static readonly DependencyProperty CollectDayEndProperty = DependencyProperty.Register(
			"CollectDayEnd",
			typeof( int? ),
			typeof( CollectorViewModel ),
			null );

		// ----------------------------------------------------------------------
		public static readonly DependencyProperty CollectHourStartProperty = DependencyProperty.Register(
			"CollectHourStart",
			typeof( int? ),
			typeof( CollectorViewModel ),
			null );

		// ----------------------------------------------------------------------
		public static readonly DependencyProperty CollectHourEndProperty = DependencyProperty.Register(
			"CollectHourEndProperty",
			typeof( int? ),
			typeof( CollectorViewModel ),
			null );

		// ----------------------------------------------------------------------
		public static readonly DependencyProperty CollectorStatusProperty = DependencyProperty.Register(
			"CollectorStatus",
			typeof( string ),
			typeof( CollectorViewModel ),
			null );

		// ----------------------------------------------------------------------
		public static readonly DependencyProperty LastCollectionDateProperty = DependencyProperty.Register(
			"LastCollectionDate",
			typeof( DateTime? ),
			typeof( CollectorViewModel ),
			null );

		// ----------------------------------------------------------------------
		public static readonly DependencyProperty CopyToClipboardProperty = DependencyProperty.Register(
			"CopyToClipboard",
			typeof( bool ),
			typeof( CollectorViewModel ),
			null );

		// ----------------------------------------------------------------------
		public CollectorViewModel()
		{
			// commands
			clearPeriodLimitsCommand = new GenericCommand( this, ClearType.TimeRange );
			clearPeriodFilterCommand = new GenericCommand( this, ClearType.Filter );
			clearCollectPeriodCommand = new GenericCommand( this, ClearType.CollectingRange );
			previousPeriodCommand = new GenericCommand( this, PeriodSelectType.Previous );
			currentPeriodCommand = new GenericCommand( this, PeriodSelectType.Current );
			nextPeriodCommand = new GenericCommand( this, PeriodSelectType.Next );
			collectYearsCommand = new GenericCommand( this, CollectType.Year );
			collectMonthsCommand = new GenericCommand( this, CollectType.Month );
			collectDaysCommand = new GenericCommand( this, CollectType.Day );
			collectHoursCommand = new GenericCommand( this, CollectType.Hour );

			// lookups
			for ( int day = 1; day <= TimeSpec.MaxDaysPerMonth; day++ )
			{
				monthDays.Add( day );
			}
			for ( int hour = 0; hour < TimeSpec.HoursPerDay; hour++ )
			{
				dayHours.Add( hour );
			}

			// filter
			yearFilter = new CheckableItemCollection<int>();
			int currentYear = DateTime.Now.Year;
			for ( int year = currentYear - 5; year < currentYear + 15; year++ )
			{
				yearFilter.AddValue( year );
			}
			yearMonthFilter = new CheckableItemCollection<YearMonth>( new EnumValueList<YearMonth>() );
			dayFilter = new CheckableItemCollection<int>();
			for ( int day = 1; day <= TimeSpec.MaxDaysPerMonth; day++ )
			{
				dayFilter.AddValue( day );
			}
			dayOfWeekFilter = new CheckableItemCollection<DayOfWeek>( new EnumValueList<DayOfWeek>() );

			// working period
			WorkingTimePeriod = TimePeriodMode.Year;

			// status
			ResetStatus();
		} // CollectorDemoViewModel

		// ----------------------------------------------------------------------
		public string Version
		{
			get { return "v" + TimePeriod.VersionInfo.AssemblyVersion; }
		} // Version

		#region Commands

		// ----------------------------------------------------------------------
		public ICommand ClearPeriodLimitsCommand
		{
			get { return clearPeriodLimitsCommand; }
		} // ClearPeriodLimitsCommand

		// ----------------------------------------------------------------------
		public ICommand ClearPeriodFilterCommand
		{
			get { return clearPeriodFilterCommand; }
		} // ClearPeriodFilterCommand

		// ----------------------------------------------------------------------
		public ICommand PreviousPeriodCommand
		{
			get { return previousPeriodCommand; }
		} // PreviousPeriodCommand

		// ----------------------------------------------------------------------
		public ICommand CurrentPeriodCommand
		{
			get { return currentPeriodCommand; }
		} // CurrentPeriodCommand

		// ----------------------------------------------------------------------
		public ICommand NextPeriodCommand
		{
			get { return nextPeriodCommand; }
		} // NextPeriodCommand

		// ----------------------------------------------------------------------
		public ICommand ClearCollectPeriodCommand
		{
			get { return clearCollectPeriodCommand; }
		} // ClearCollectPeriodCommand

		// ----------------------------------------------------------------------
		public ICommand CollectYearsCommand
		{
			get { return collectYearsCommand; }
		} // CollectYearsCommand

		// ----------------------------------------------------------------------
		public ICommand CollectMonthsCommand
		{
			get { return collectMonthsCommand; }
		} // CollectMonthsCommand

		// ----------------------------------------------------------------------
		public ICommand CollectDaysCommand
		{
			get { return collectDaysCommand; }
		} // CollectDaysCommand

		// ----------------------------------------------------------------------
		public ICommand CollectHoursCommand
		{
			get { return collectHoursCommand; }
		} // CollectHoursCommand

		#endregion

		// ----------------------------------------------------------------------
		public CheckableItemCollection<int> YearFilter
		{
			get { return yearFilter; }
		} // YearFilter

		// ----------------------------------------------------------------------
		public CheckableItemCollection<YearMonth> YearMonthFilter
		{
			get { return yearMonthFilter; }
		} // YearMonthFilter

		// ----------------------------------------------------------------------
		public CheckableItemCollection<int> DayFilter
		{
			get { return dayFilter; }
		} // DayFilter

		// ----------------------------------------------------------------------
		public CheckableItemCollection<DayOfWeek> DayOfWeekFilter
		{
			get { return dayOfWeekFilter; }
		} // DayOfWeekFilter

		// ----------------------------------------------------------------------
		public IEnumerable<TimePeriodMode> TimePeriods
		{
			get { return timePeriods; }
		} // TimePeriods

		// ----------------------------------------------------------------------
		public IEnumerable<YearMonth> YearMonths
		{
			get { return yearMonths; }
		} // YearMonths

		// ----------------------------------------------------------------------
		public IEnumerable<int> MonthDays
		{
			get { return monthDays; }
		} // MonthDays

		// ----------------------------------------------------------------------
		public IEnumerable<int> DayHours
		{
			get { return dayHours; }
		} // DayHours

		// ----------------------------------------------------------------------
		public TimePeriodMode WorkingTimePeriod
		{
			get { return (TimePeriodMode)GetValue( WorkingTimePeriodProperty ); }
			set { SetValue( WorkingTimePeriodProperty, value ); }
		} // WorkingTimePeriod

		// ----------------------------------------------------------------------
		public DateTime WorkingPeriodStartDate
		{
			get { return (DateTime)GetValue( WorkingPeriodStartDateProperty ); }
			set { SetValue( WorkingPeriodStartDateProperty, value ); }
		} // WorkingPeriodStartDate

		// ----------------------------------------------------------------------
		public int WorkingPeriodStartHour
		{
			get { return (int)GetValue( WorkingPeriodStartHourProperty ); }
			set { SetValue( WorkingPeriodStartHourProperty, value ); }
		} // WorkingPeriodStartHour

		// ----------------------------------------------------------------------
		public DateTime WorkingPeriodEndDate
		{
			get { return (DateTime)GetValue( WorkingPeriodEndDateProperty ); }
			set { SetValue( WorkingPeriodEndDateProperty, value ); }
		} // WorkingPeriodEndDate

		// ----------------------------------------------------------------------
		public int WorkingPeriodEndHour
		{
			get { return (int)GetValue( WorkingPeriodEndHourProperty ); }
			set { SetValue( WorkingPeriodEndHourProperty, value ); }
		} // WorkingPeriodEndHour

		// ----------------------------------------------------------------------
		public YearMonth? CollectMonthStart
		{
			get { return (YearMonth?)GetValue( CollectMonthStartProperty ); }
			set { SetValue( CollectMonthStartProperty, value ); }
		} // CollectMonthStart

		// ----------------------------------------------------------------------
		public YearMonth? CollectMonthEnd
		{
			get { return (YearMonth?)GetValue( CollectMonthEndProperty ); }
			set { SetValue( CollectMonthEndProperty, value ); }
		} // CollectMonthEnd

		// ----------------------------------------------------------------------
		public int? CollectDayStart
		{
			get { return (int?)GetValue( CollectDayStartProperty ); }
			set { SetValue( CollectDayStartProperty, value ); }
		} // CollectDayStart

		// ----------------------------------------------------------------------
		public int? CollectDayEnd
		{
			get { return (int?)GetValue( CollectDayEndProperty ); }
			set { SetValue( CollectDayEndProperty, value ); }
		} // CollectDayEnd

		// ----------------------------------------------------------------------
		public int? CollectHourStart
		{
			get { return (int?)GetValue( CollectHourStartProperty ); }
			set { SetValue( CollectHourStartProperty, value ); }
		} // CollectHourStart

		// ----------------------------------------------------------------------
		public int? CollectHourEnd
		{
			get { return (int?)GetValue( CollectHourEndProperty ); }
			set { SetValue( CollectHourEndProperty, value ); }
		} // CollectHourEnd

		// ----------------------------------------------------------------------
		public string CollectorStatus
		{
			get { return (string)GetValue( CollectorStatusProperty ); }
			set { SetValue( CollectorStatusProperty, value ); }
		} // CollectorStatus

		// ----------------------------------------------------------------------
		public DateTime? LastCollectionDate
		{
			get { return (DateTime?)GetValue( LastCollectionDateProperty ); }
			set { SetValue( LastCollectionDateProperty, value ); }
		} // LastCollectionDate

		// ----------------------------------------------------------------------
		public bool CopyToClipboard
		{
			get { return (bool)GetValue( CopyToClipboardProperty ); }
			set { SetValue( CopyToClipboardProperty, value ); }
		} // CopyToClipboard

		// ----------------------------------------------------------------------
		public ObservableCollection<ITimePeriod> CollectedPeriods
		{
			get { return collectedPeriods; }
		} // CollectedPeriods

		// ----------------------------------------------------------------------
		private CalendarTimeRange CurrentTimeRange
		{
			get
			{
				CalendarTimeRange timeRange = null;

				switch ( WorkingTimePeriod )
				{
					case TimePeriodMode.Custom:
						return WorkingTimeRange;
					case TimePeriodMode.Year:
						return new Year();
					case TimePeriodMode.Halfyear:
						return new Halfyear();
					case TimePeriodMode.Quarter:
						return new Quarter();
					case TimePeriodMode.Month:
						return new Month();
					case TimePeriodMode.Week:
						return new Week();
					case TimePeriodMode.Day:
						return new Day();
				}

				return timeRange;
			}
		} // CurrentTimeRange

		// ----------------------------------------------------------------------
		private CalendarTimeRange WorkingTimeRange
		{
			get
			{
				DateTime start = new DateTime(
					WorkingPeriodStartDate.Year,
					WorkingPeriodStartDate.Month,
					WorkingPeriodStartDate.Day,
					WorkingPeriodStartHour,
					0,
					0 );
				DateTime end = new DateTime(
					WorkingPeriodEndDate.Year,
					WorkingPeriodEndDate.Month,
					WorkingPeriodEndDate.Day,
					WorkingPeriodEndHour,
					0,
					0 ).AddHours( 1 );
				return end > start ? new CalendarTimeRange( start, end ) : null;
			}
		} // WorkingTimeRange

		// ----------------------------------------------------------------------
		bool ICommandHandler.IsAvailable( ICommand command, object parameter, object context )
		{
			// select period
			if ( context.GetType() == typeof( PeriodSelectType ) )
			{
				return WorkingTimePeriod != TimePeriodMode.Custom;
			}

			// collect
			if ( context.GetType() == typeof( CollectType ) )
			{
				return WorkingTimeRange != null;
			}

			return true;
		} // ICommandHandler.IsAvailable

		// ----------------------------------------------------------------------
		void ICommandHandler.Start( ICommand command, object parameter, object context )
		{
			if ( context == null )
			{
				return;
			}

			// clear
			if ( context.GetType() == typeof( ClearType ) )
			{
				Clear( (ClearType)context );
				return;
			}

			// select period
			if ( context.GetType() == typeof( PeriodSelectType ) )
			{
				SelectPeriod( (PeriodSelectType)context );
				return;
			}

			// collect
			if ( context.GetType() == typeof( CollectType ) )
			{
				Collect( (CollectType)context );
				return;
			}
		} // ICommandHandler.Start

		// ----------------------------------------------------------------------
		private void Clear( ClearType clearType )
		{
			switch ( clearType )
			{
				case ClearType.TimeRange:
					WorkingTimePeriod = TimePeriodMode.Year;
					break;
				case ClearType.Filter:
					yearFilter.SetSelection( false );
					yearMonthFilter.SetSelection( false );
					dayFilter.SetSelection( false );
					dayOfWeekFilter.SetSelection( false );
					break;
				case ClearType.CollectingRange:
					CollectMonthStart = null;
					CollectMonthEnd = null;
					CollectDayStart = null;
					CollectDayEnd = null;
					CollectHourStart = null;
					CollectHourEnd = null;
					break;
			}
		} // Clear

		// ----------------------------------------------------------------------
		private void SelectPeriod( PeriodSelectType periodSelectType )
		{
			int offset = 0;
			switch ( periodSelectType )
			{
				case PeriodSelectType.Previous:
					offset = -1;
					break;
				case PeriodSelectType.Current:
					ResetWorkingPeriod();
					return;
				case PeriodSelectType.Next:
					offset = 1;
					break;
			}

			switch ( WorkingTimePeriod )
			{
				case TimePeriodMode.Year:
					Year year = new Year( WorkingPeriodStartDate );
					SetWorkingPeriod( year.AddYears( offset ) );
					break;
				case TimePeriodMode.Halfyear:
					Halfyear halfyear = new Halfyear( WorkingPeriodStartDate );
					SetWorkingPeriod( halfyear.AddHalfyears( offset ) );
					break;
				case TimePeriodMode.Quarter:
					Quarter quarter = new Quarter( WorkingPeriodStartDate );
					SetWorkingPeriod( quarter.AddQuarters( offset ) );
					break;
				case TimePeriodMode.Month:
					Month month = new Month( WorkingPeriodStartDate );
					SetWorkingPeriod( month.AddMonths( offset ) );
					break;
				case TimePeriodMode.Week:
					Week week = new Week( WorkingPeriodStartDate );
					SetWorkingPeriod( week.AddWeeks( offset ) );
					break;
				case TimePeriodMode.Day:
					Day day = new Day( WorkingPeriodStartDate );
					SetWorkingPeriod( day.AddDays( offset ) );
					break;
			}
		} // SelectPeriod

		// ----------------------------------------------------------------------
		private void Collect( CollectType collectType )
		{
			// filter
			CalendarPeriodCollectorFilter filter = new CalendarPeriodCollectorFilter();
			foreach ( int year in yearFilter.SelectedItems )
			{
				filter.Years.Add( year );
			}
			foreach ( YearMonth yearMonth in yearMonthFilter.SelectedItems )
			{
				filter.Months.Add( yearMonth );
			}
			foreach ( int day in dayFilter.SelectedItems )
			{
				filter.Days.Add( day );
			}
			foreach ( DayOfWeek dayOfWeek in dayOfWeekFilter.SelectedItems )
			{
				filter.WeekDays.Add( dayOfWeek );
			}

			// period limits
			TimeRange workingTimeRange = WorkingTimeRange;
			if ( workingTimeRange == null )
			{
				CollectorStatus = "Invalid Working Time Range!";
				return;
			}

			// collect months
			YearMonth? collectMonthStart = CollectMonthStart;
			YearMonth? collectMonthEnd = CollectMonthEnd;
			if ( collectMonthStart.HasValue && collectMonthEnd.HasValue )
			{
				if ( collectMonthEnd.Value < collectMonthStart.Value )
				{
					CollectorStatus = "Invalid Collecting Periods Months!";
					return;
				}
				filter.CollectingMonths.Add( new MonthRange( collectMonthStart.Value, collectMonthEnd.Value ) );
			}

			// collect day
			int? collectDayStart = CollectDayStart;
			int? collectDayEnd = CollectDayEnd;
			if ( collectDayStart.HasValue && collectDayEnd.HasValue )
			{
				if ( collectDayEnd.Value < collectDayStart.Value )
				{
					CollectorStatus = "Invalid Collecting Periods Days!";
					return;
				}
				filter.CollectingDays.Add( new DayRange( collectDayStart.Value, collectDayEnd.Value ) );
			}

			// collect hour
			int? collectHourStart = CollectHourStart;
			int? collectHourEnd = CollectHourEnd;
			if ( collectHourStart.HasValue && collectHourEnd.HasValue )
			{
				if ( collectHourEnd.Value < collectHourStart.Value )
				{
					CollectorStatus = "Invalid Collecting Periods Hours!";
					return;
				}
				filter.CollectingHours.Add( new HourRange( collectHourStart.Value, collectHourEnd.Value ) );
			}

			StringBuilder status = new StringBuilder();

			// collector
			string collectContext = string.Empty;
			TimePeriod.CalendarPeriodCollector collector =
				new TimePeriod.CalendarPeriodCollector( filter, workingTimeRange );
			switch ( collectType )
			{
				case CollectType.Year:
					collectContext = "year";
					collector.CollectYears();
					break;
				case CollectType.Month:
					collectContext = "month";
					collector.CollectMonths();
					break;
				case CollectType.Day:
					collectContext = "day";
					collector.CollectDays();
					break;
				case CollectType.Hour:
					collectContext = "hour";
					collector.CollectHours();
					break;
			}

			collectedPeriods.Clear();
			if ( collector.Periods.Count == 0 )
			{
				status.Append( string.Format( "no {0} periods found", collectContext ) );
			}
			else if ( collector.Periods.Count > maxPeriodCount )
			{
				status.Append( string.Format( "{0} {1} periods found, a maximum of {2} periods can be displayed",
																			collector.Periods.Count, collectContext, maxPeriodCount ) );
			}
			else
			{
				foreach ( ITimePeriod timePeriod in collector.Periods )
				{
					collectedPeriods.Add( timePeriod );
				}
				status.Append( string.Format( "{0} {1} periods found", collectedPeriods.Count, collectContext ) );
			}

			CollectorStatus = status.ToString();
			LastCollectionDate = DateTime.Now;

			if ( CopyToClipboard )
			{
				collectedPeriods.CopyToClipboard();
			}
		} // Collect

		// ----------------------------------------------------------------------
		private void ResetStatus()
		{
			CollectorStatus = "Please choose working range, filter, collecting ranges and collect the periods";
		} // ResetStatus

		// ----------------------------------------------------------------------
		private void ResetWorkingPeriod()
		{
			SetWorkingPeriod( CurrentTimeRange );
		} // ResetWorkingPeriod

		// ----------------------------------------------------------------------
		private void SetWorkingPeriod( ITimePeriod timeRange )
		{
			if ( isWorkingPeriodUpdating )
			{
				return;
			}

			if ( timeRange == null )
			{
				WorkingTimePeriod = TimePeriodMode.Year;
				return;
			}

			isWorkingPeriodUpdating = true;
			WorkingPeriodStartDate = timeRange.Start;
			WorkingPeriodStartHour = timeRange.Start.Hour;
			WorkingPeriodEndDate = timeRange.End;
			WorkingPeriodEndHour = timeRange.End.Hour;
			isWorkingPeriodUpdating = false;
		} // SetWorkingPeriod

		// ----------------------------------------------------------------------
		private void UpdateWorkingPeriodMode()
		{
			if ( isWorkingPeriodUpdating )
			{
				return;
			}

			TimePeriodMode workingTimePeriod = TimePeriodMode.Custom;
			TimeRange workingTimeRange = WorkingTimeRange;
			if ( workingTimeRange != null )
			{
				if ( workingTimeRange.IsSamePeriod( new Year() ) )
				{
					workingTimePeriod = TimePeriodMode.Year;
				}
				else if ( workingTimeRange.IsSamePeriod( new Halfyear() ) )
				{
					workingTimePeriod = TimePeriodMode.Halfyear;
				}
				else if ( workingTimeRange.IsSamePeriod( new Quarter() ) )
				{
					workingTimePeriod = TimePeriodMode.Quarter;
				}
				else if ( workingTimeRange.IsSamePeriod( new Month() ) )
				{
					workingTimePeriod = TimePeriodMode.Month;
				}
				else if ( workingTimeRange.IsSamePeriod( new Week() ) )
				{
					workingTimePeriod = TimePeriodMode.Week;
				}
				else if ( workingTimeRange.IsSamePeriod( new Day() ) )
				{
					workingTimePeriod = TimePeriodMode.Day;
				}
			}

			isWorkingPeriodUpdating = true;
			WorkingTimePeriod = workingTimePeriod;
			isWorkingPeriodUpdating = false;
		} // UpdateWorkingPeriodMode

		// ----------------------------------------------------------------------
		private void UpdateCommands()
		{
			clearPeriodLimitsCommand.UpdateCanExecute();
			clearPeriodFilterCommand.UpdateCanExecute();
			clearCollectPeriodCommand.UpdateCanExecute();
			previousPeriodCommand.UpdateCanExecute();
			currentPeriodCommand.UpdateCanExecute();
			nextPeriodCommand.UpdateCanExecute();
			collectYearsCommand.UpdateCanExecute();
			collectMonthsCommand.UpdateCanExecute();
			collectDaysCommand.UpdateCanExecute();
			collectHoursCommand.UpdateCanExecute();
		} // UpdateCommands

		// ----------------------------------------------------------------------
		private static void WorkingTimePeriodModeChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
		{
			CollectorViewModel viewModel = d as CollectorViewModel;
			if ( viewModel == null )
			{
				return;
			}

			viewModel.ResetWorkingPeriod();
			viewModel.UpdateCommands();
		} // WorkingTimePeriodModeChanged

		// ----------------------------------------------------------------------
		private static void WorkingPeriodLimitsChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
		{
			CollectorViewModel viewModel = d as CollectorViewModel;
			if ( viewModel == null )
			{
				return;
			}

			viewModel.UpdateWorkingPeriodMode();
			viewModel.UpdateCommands();
		} // WorkingPeriodLimitsChanged

		// ----------------------------------------------------------------------
		// members
		private readonly GenericCommand clearPeriodLimitsCommand;
		private readonly GenericCommand clearPeriodFilterCommand;
		private readonly GenericCommand clearCollectPeriodCommand;
		private readonly GenericCommand previousPeriodCommand;
		private readonly GenericCommand currentPeriodCommand;
		private readonly GenericCommand nextPeriodCommand;
		private readonly GenericCommand collectYearsCommand;
		private readonly GenericCommand collectMonthsCommand;
		private readonly GenericCommand collectDaysCommand;
		private readonly GenericCommand collectHoursCommand;
		private readonly EnumValueList<TimePeriodMode> timePeriods = new EnumValueList<TimePeriodMode>();
		private readonly EnumValueList<YearMonth> yearMonths = new EnumValueList<YearMonth>();
		private readonly List<int> monthDays = new List<int>();
		private readonly List<int> dayHours = new List<int>();
		private readonly TimePeriodObservableCollection collectedPeriods = new TimePeriodObservableCollection();
		private readonly CheckableItemCollection<int> yearFilter;
		private readonly CheckableItemCollection<YearMonth> yearMonthFilter;
		private readonly CheckableItemCollection<int> dayFilter;
		private readonly CheckableItemCollection<DayOfWeek> dayOfWeekFilter;
		private bool isWorkingPeriodUpdating;

		private const int maxPeriodCount = 10000;

	} // class CollectorDemoViewModel

} // namespace Itenso.CalendarPeriodCollector
// -- EOF -------------------------------------------------------------------
