// -- FILE ------------------------------------------------------------------
// name       : CollectCommand.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;
using System.Windows.Input;

namespace Itenso.CalendarPeriodCollector
{

	// ------------------------------------------------------------------------
	public class GenericCommand : ICommand
	{

		// ----------------------------------------------------------------------
		public event EventHandler CanExecuteChanged;

		// ----------------------------------------------------------------------
		public GenericCommand( ICommandHandler commandHandler ) :
			this( commandHandler, null )
		{
		} // Command

		// ----------------------------------------------------------------------
		public GenericCommand( ICommandHandler commandHandler, object context )
		{
			if ( commandHandler == null )
			{
				throw new ArgumentNullException( "commandHandler" );
			}
			this.commandHandler = commandHandler;
			this.context = context;
		} // Command

		// ----------------------------------------------------------------------
		public ICommandHandler CommandHandler
		{
			get { return commandHandler; }
		} // CommandHandler

		// ----------------------------------------------------------------------
		public object Context
		{
			get { return context; }
		} // Context

		// ----------------------------------------------------------------------
		public virtual bool CanExecute( object parameter )
		{
			return canExecute;
		} // CanExecute

		// ----------------------------------------------------------------------
		public virtual void Execute( object parameter )
		{
			commandHandler.Start( this, parameter, context );	
		} // Execute

		// ----------------------------------------------------------------------
		public void UpdateCanExecute( object parameter = null )
		{
			bool isAvailable = commandHandler.IsAvailable( this, parameter, context );
			if ( isAvailable != canExecute )
			{
				canExecute = isAvailable;
				OnCanExecuteChanged();
			}
		} // UpdateCanExecute

		// ----------------------------------------------------------------------
		protected virtual void OnCanExecuteChanged()
		{
			EventHandler handler = CanExecuteChanged;
			if ( handler != null )
			{
				handler( this, EventArgs.Empty );
			}
		} // OnCanExecuteChanged

		// ----------------------------------------------------------------------
		// members
		private readonly ICommandHandler commandHandler;
		private readonly object context;
		private bool canExecute = true;

	} // class CollectCommand

} // namespace Itenso.CalendarPeriodCollector
// -- EOF -------------------------------------------------------------------
