// -- FILE ------------------------------------------------------------------
// name       : EnumValueList.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Itenso.CalendarPeriodCollector
{

	// ------------------------------------------------------------------------
	public class EnumValueList<T> : IEnumerable<T>
	{

		// ----------------------------------------------------------------------
		public EnumValueList()
		{
			IEnumerable<T> enumValues = GetEnumValues();
			foreach ( T enumValue in enumValues )
			{
				enumItems.Add( enumValue );
			}
		} // EnumValueList

		// ----------------------------------------------------------------------
		protected Type EnumType
		{
			get { return typeof( T ); }
		} // EnumType

		// ----------------------------------------------------------------------
		public IEnumerator<T> GetEnumerator()
		{
			return enumItems.GetEnumerator();
		} // GetEnumerator

		// ----------------------------------------------------------------------
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		} // GetEnumerator

		// ----------------------------------------------------------------------
		// no Enum.GetValues() in Silverlight
		private IEnumerable<T> GetEnumValues()
		{
#if SILVERLIGHT
			List<T> enumValue = new List<T>();

			Type enumType = EnumType;
			foreach ( FieldInfo fieldInfo in enumType.GetFields( BindingFlags.Static | BindingFlags.Public ) )
			{
				enumValue.Add( (T)Enum.Parse( enumType, fieldInfo.Name, false ) );
			}
			return enumValue;
#else
			return Enum.GetValues( enumType );
#endif
		} // GetEnumValues

		// ----------------------------------------------------------------------
		// members
		private readonly List<T> enumItems = new List<T>();

	} // class EnumValueList

} // namespace Itenso.CalendarPeriodCollector
// -- EOF -------------------------------------------------------------------
