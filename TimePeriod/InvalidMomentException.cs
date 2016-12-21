// -- FILE ------------------------------------------------------------------
// name       : InvalidMomentException.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2013.11.03
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;
#if (!SILVERLIGHT &&!PCL)
using System.Runtime.Serialization;
#endif

namespace Itenso.TimePeriod
{

	// ------------------------------------------------------------------------
#if (!SILVERLIGHT &&!PCL)
	[Serializable]
#endif
	public class InvalidMomentException : Exception
	{

		// ----------------------------------------------------------------------
		public InvalidMomentException( DateTime moment )
		{
			this.moment = moment;
		} // InvalidMomentException

		// ----------------------------------------------------------------------
		public InvalidMomentException( DateTime moment, string message ) :
			base( message )
		{
			this.moment = moment;
		} // InvalidMomentException

		// ----------------------------------------------------------------------
		public InvalidMomentException( DateTime moment, Exception cause ) :
			base( cause.Message, cause )
		{
			this.moment = moment;
		} // InvalidMomentException

		// ----------------------------------------------------------------------
		public InvalidMomentException( DateTime moment, string message, Exception cause ) :
			base( message, cause )
		{
			this.moment = moment;
		} // InvalidMomentException

		// ----------------------------------------------------------------------
#if (!SILVERLIGHT &&!PCL)
		private InvalidMomentException( SerializationInfo info, StreamingContext context ) :
			base( info, context )
		{
			moment = (DateTime)info.GetValue( "moment", typeof( DateTime ) );
		} // InvalidMomentException

		// ----------------------------------------------------------------------
		public override void GetObjectData( SerializationInfo info, StreamingContext context )
		{
			info.AddValue( "faultInfo", moment );
			base.GetObjectData( info, context );
		} // GetObjectData
#endif

		// ----------------------------------------------------------------------
		public DateTime Moment
		{
			get { return moment; }
		} // Moment

		// ----------------------------------------------------------------------
		// members
		private readonly DateTime moment;

	} // class InvalidMomentException

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
