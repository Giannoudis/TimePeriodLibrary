// -- FILE ------------------------------------------------------------------
// name       : VersionInfo.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;
using System.Reflection;

// Version information for an assembly consists of the following four values:
//      Major Version
//      Minor Version
//      Build Number
//      Revision
[assembly: AssemblyVersion("2.0.0.0")]

// ReSharper disable CheckNamespace
namespace Itenso.TimePeriod
// ReSharper restore CheckNamespace
{

	// ------------------------------------------------------------------------
	public sealed class VersionInfo
	{

		/// <value>Provides easy access to the assemblies version as a string.</value>
		public static readonly string AssemblyVersion = VersionOf( typeof( VersionInfo ) );

		// ----------------------------------------------------------------------
		private static string VersionOf( Type type )
		{
			if ( type == null )
			{
				return null;
			}
			Assembly assembly = type.Assembly;
#if (SILVERLIGHT || PCL)
			AssemblyName assemblyName = new AssemblyName( assembly.FullName );
#else
			AssemblyName assemblyName = assembly.GetName();
#endif
			Version assemblyVersion = assemblyName.Version;
			return assemblyVersion == null ? null : assemblyVersion.ToString();
		} // VersionOf

	} // class VersionInfo

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
