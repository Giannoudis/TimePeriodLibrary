// -- FILE ------------------------------------------------------------------
// name       : CheckableItem.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;
using System.Windows;

namespace Itenso.CalendarPeriodCollector
{

	// ------------------------------------------------------------------------
	public class CheckableItem<T> : ViewModel
	{

		// ----------------------------------------------------------------------
		public event EventHandler IsSelectedChanged;

		// ----------------------------------------------------------------------
		public static readonly DependencyProperty ContentProperty = DependencyProperty.Register(
			"Content",
			typeof( T ),
			typeof( CheckableItem<T> ),
			null );

		// ----------------------------------------------------------------------
		public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register(
			"IsSelected",
			typeof( bool ),
			typeof( CheckableItem<T> ),
			new PropertyMetadata( IsSelectedPropertyChanged ) );

		// ----------------------------------------------------------------------
		public T Content
		{
			get { return (T)GetValue( ContentProperty ); }
			set { SetValue( ContentProperty, value ); }
		} // Content

		// ----------------------------------------------------------------------
		public bool IsSelected
		{
			get { return (bool)GetValue( IsSelectedProperty ); }
			set { SetValue( IsSelectedProperty, value ); }
		} // IsSelected

		// ----------------------------------------------------------------------
		protected void OnIsSelectedChanged()
		{
			EventHandler isSelectedChanged = IsSelectedChanged;
			if ( isSelectedChanged != null )
			{
				isSelectedChanged( this, EventArgs.Empty );
			}
		} // OnIsSelectedChanged

		// ----------------------------------------------------------------------
		private static void IsSelectedPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
		{
			CheckableItem<T> checkableItem = d as CheckableItem<T>;
			if ( checkableItem == null )
			{
				return;
			}

			checkableItem.OnIsSelectedChanged();
		} // IsSelectedPropertyChanged

	} // class CheckableItem

} // namespace Itenso.CalendarPeriodCollector
// -- EOF -------------------------------------------------------------------
