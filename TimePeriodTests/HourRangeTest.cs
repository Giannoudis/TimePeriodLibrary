// -- FILE ------------------------------------------------------------------
// name       : HourRangeTest.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.08.26
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using Itenso.TimePeriod;
using Xunit;

namespace Itenso.TimePeriodTests
{

    // ------------------------------------------------------------------------

    public sealed class HourRangeTest : TestUnitBase
    {

        // ----------------------------------------------------------------------
        [Trait("Category", "HourRange")]
        [Fact]
        public void ConstructorHourTest()
        {
            var hourRange = new HourRange(1);

            Assert.Equal(hourRange.Start, new Time(1));
            Assert.Equal(hourRange.End, new Time(1));
        } // ConstructorHourTest

        // ----------------------------------------------------------------------
        [Trait("Category", "HourRange")]
        [Fact]
        public void ConstructorTimeTest()
        {
            var hourRange = new HourRange(new Time(1, 30), new Time(2, 45));

            Assert.Equal(hourRange.Start, new Time(1, 30));
            Assert.Equal(hourRange.End, new Time(2, 45));
        } // ConstructorTimeTest

        // ----------------------------------------------------------------------
        [Trait("Category", "HourRange")]
        [Fact]
        public void ConstructorTimeSortTest()
        {
            var hourRange = new HourRange(new Time(2, 45), new Time(1, 30));

            Assert.Equal(hourRange.Start, new Time(1, 30));
            Assert.Equal(hourRange.End, new Time(2, 45));
        } // ConstructorTimeSortTest

        // ----------------------------------------------------------------------
        [Trait("Category", "HourRange")]
        [Fact]
        public void IsMomentTest()
        {
            var hourRange = new HourRange(new Time(2, 45, 33, 876), new Time(2, 45, 33, 876));

            Assert.True(hourRange.IsMoment);
        } // IsMomentTest

        // ----------------------------------------------------------------------
        [Trait("Category", "HourRange")]
        [Fact]
        public void IsNotMomentTest()
        {
            Assert.True(new HourRange(new Time(), new Time()).IsMoment);
            Assert.True(new HourRange(new Time(24), new Time(24)).IsMoment);
            Assert.True(new HourRange(new Time(0, 24), new Time(0, 24)).IsMoment);
            Assert.False(new HourRange(new Time(2, 45, 33, 876), new Time(2, 45, 33, 877)).IsMoment);
        } // IsNotMomentTest

    } // class HourRangeTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
