// -- FILE ------------------------------------------------------------------
// name       : ICommandHandler.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System.Windows.Input;

namespace Itenso.CalendarPeriodCollector
{

	// ------------------------------------------------------------------------
	public interface ICommandHandler
	{

		// ----------------------------------------------------------------------
		bool IsAvailable( ICommand command, object parameter, object context );

		// ----------------------------------------------------------------------
		void Start( ICommand command, object parameter, object context );

	} // class ICommandHandler

} // namespace Itenso.CalendarPeriodCollector
// -- EOF -------------------------------------------------------------------
