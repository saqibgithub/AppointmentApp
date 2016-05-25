using System;
using System.Net;
using SystemConfiguration;
using Xamarin.Forms;

using AppointmentApp.DependencyInterfaces;
using AppointmentApp.iOS.DependencyInterfaces;
using AppointmentApp;

[assembly: Dependency(typeof (INetworkDependencyInterface_iOS))]
namespace AppointmentApp.iOS.DependencyInterfaces
{
	public class INetworkDependencyInterface_iOS : INetworkDependencyInterface
	{
		public INetworkDependencyInterface_iOS ()
		{
		}

		public bool isConnected() {

			NetworkStatus internetStatus = Reachability.InternetConnectionStatus ();
			return internetStatus != NetworkStatus.NotReachable;
		}


	}
}

