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
                        new( "ar-DZ" ),
                        new( "ca-ES" ),
                        new( "zh-HK" ),
                        new( "cs-CZ" ),
                        new( "da-DK" ),
                        new( "de-DE" ),
                        new( "de-CH" ),
                        new( "el-GR" ),
                        new( "en-US" ),
                        new( "es-ES" ),
                        new( "fi-FI" )
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
        private IEnumerable<CultureInfo> Cultures { get; }

    } // class CultureTestData

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
