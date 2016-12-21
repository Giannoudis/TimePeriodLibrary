// -- FILE ------------------------------------------------------------------
// name       : CultureTestData.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace Itenso.TimePeriodTests
{

	// ------------------------------------------------------------------------
	public class CultureTestData : IEnumerable<CultureInfo>
	{

		// ----------------------------------------------------------------------
		public CultureTestData()
		{
			Cultures = new List<CultureInfo>
					{
						new CultureInfo( "ar-DZ" ),
						new CultureInfo( "ca-ES" ),
						new CultureInfo( "zh-HK" ),
						new CultureInfo( "cs-CZ" ),
						new CultureInfo( "da-DK" ),
						new CultureInfo( "de-DE" ),
						new CultureInfo( "de-CH" ),
						new CultureInfo( "el-GR" ),
						new CultureInfo( "en-US" ),
						new CultureInfo( "es-ES" ),
						new CultureInfo( "fi-FI" )
					};
		} // CultureTestData

		// ----------------------------------------------------------------------
		public IEnumerator<CultureInfo> GetEnumerator()
		{
			return Cultures.GetEnumerator();
		} // GetEnumerator

		// ----------------------------------------------------------------------
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		} // IEnumerable.GetEnumerator

		// ----------------------------------------------------------------------
		public IEnumerable<CultureInfo> Cultures { get; private set; }

	} // class CultureTestData

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
