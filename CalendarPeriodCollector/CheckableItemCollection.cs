// -- FILE ------------------------------------------------------------------
// name       : CheckableItemCollection.cs
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

namespace Itenso.CalendarPeriodCollector
{

	// ------------------------------------------------------------------------
	public class CheckableItemCollection<T> : ObservableCollection<CheckableItem<T>>
	{

		// ----------------------------------------------------------------------
		public CheckableItemCollection()
		{
		} // CheckableItemCollection

		// ----------------------------------------------------------------------
		public CheckableItemCollection( IEnumerable<T> values )
		{
			AddValues( values );
		} // CheckableItemCollection

		// ----------------------------------------------------------------------
		public void AddValue( T value )
		{
			Add( new CheckableItem<T>
			{
				Content = value
			} );
		} // AddValue

		// ----------------------------------------------------------------------
		public void AddValues( IEnumerable<T> values )
		{
			foreach ( T value in values )
			{
				AddValue( value );
			}
		} // AddValues

		// ----------------------------------------------------------------------
		public IList<T> SelectedItems
		{
			get { return selectedItems; }
		} // SelectedItems

		// ----------------------------------------------------------------------
		public string SelectedValues
		{
			get { return selectedValues; }
			private set
			{
				if ( string.Equals( selectedValues, value ) )
				{
					return;
				}
				selectedValues = value;
				OnPropertyChanged( new PropertyChangedEventArgs( "SelectedValues" ) );
			}
		} // SelectedValues

		// ----------------------------------------------------------------------
		public void SetSelection( bool isSelected )
		{
			foreach ( CheckableItem<T> checkableItem in this )
			{
				checkableItem.IsSelected = isSelected;
			}
		} // SetSelection

		// ----------------------------------------------------------------------
		public bool Contains( T item )
		{
			foreach ( CheckableItem<T> checkableItem in this )
			{
				if ( checkableItem.Content.Equals( item ) )
				{
					return true;
				}
			}
			return false;
		} // Contains

		// ----------------------------------------------------------------------
		public bool IsSelected( object item )
		{
			foreach ( CheckableItem<T> checkableItem in this )
			{
				if ( checkableItem.Content.Equals( item ) )
				{
					return checkableItem.IsSelected;
				}
			}
			throw new InvalidOperationException();
		} // IsSelected

		// ----------------------------------------------------------------------
		protected override void InsertItem( int index, CheckableItem<T> item )
		{
			base.InsertItem( index, item );
			item.IsSelectedChanged += IsItemSelectedChanged;
		} // IsSelected

		// ----------------------------------------------------------------------
		protected override void RemoveItem( int index )
		{
			CheckableItem<T> item = this[ index ];
			if ( item != null )
			{
				item.IsSelectedChanged -= IsItemSelectedChanged;
			}
			base.RemoveItem( index );
		} // IsSelected

		// ----------------------------------------------------------------------
		private void UpdateSelectedItems()
		{
			StringBuilder sb = new StringBuilder();
			selectedItems.Clear();
			foreach ( CheckableItem<T> checkableItem in this )
			{
				if ( checkableItem.IsSelected )
				{
					selectedItems.Add( checkableItem.Content );
					if ( sb.Length > 0 )
					{
						sb.Append( ", " );
					}
					sb.Append( checkableItem.Content );
				}
			}
			SelectedValues = sb.ToString();
		} // UpdateSelectedItems

		// ----------------------------------------------------------------------
		private void IsItemSelectedChanged( object sender, EventArgs e )
		{
			UpdateSelectedItems();
		} // IsSelected

		// ----------------------------------------------------------------------
		private readonly List<T> selectedItems = new List<T>();
		private string selectedValues;

	} // class CheckableItemCollection

} // namespace Itenso.CalendarPeriodCollector
// -- EOF -------------------------------------------------------------------
