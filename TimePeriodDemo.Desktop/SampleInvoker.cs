// -- FILE ------------------------------------------------------------------
// name       : SampleInvoker.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.03.24
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;
using System.Reflection;

namespace Itenso.TimePeriodDemo
{

	// ------------------------------------------------------------------------
	public static class SampleInvoker
	{

		// ----------------------------------------------------------------------
		public static void Start( Type type, string group = null )
		{
			if ( type == null )
			{
				throw new ArgumentNullException( "type" );
			}
			if ( !type.IsClass )
			{
				throw new InvalidOperationException();
			}

			object instance = Activator.CreateInstance( type );

			MethodInfo[] methodInfos = type.GetMethods( BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly );
			foreach ( MethodInfo methodInfo in methodInfos )
			{
				object[] attributes = methodInfo.GetCustomAttributes( typeof( SampleAttribute ), true );

				if ( attributes.Length == 0 )
				{
					continue;
				}

				bool isMatchingMethod = true;
				if ( !string.IsNullOrEmpty( group ) )
				{
					foreach ( SampleAttribute sampleAttribute in attributes )
					{
						if ( string.Equals( group, sampleAttribute.Group ) )
						{
							break;
						}
						isMatchingMethod = false; // not matching
					}
				}

				if ( !isMatchingMethod )
				{
					continue;
				}

				string name = methodInfo.Name;
				if ( !string.IsNullOrEmpty( group ) )
				{
					name += " [" + group + "]";
				}
				Console.WriteLine( "{0} {1} {2}", new string( '-', 20 ), name, new string( '-', 20 ) );
				methodInfo.Invoke( instance, null );
			}
		} // Start

	} // class SampleInvoker

} // namespace Itenso.TimePeriodDemo
// -- EOF -------------------------------------------------------------------
