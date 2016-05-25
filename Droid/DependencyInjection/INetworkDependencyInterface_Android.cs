using System;
using Xamarin.Forms;
using Android.Net;
using Android.App;
using AppointmentApp.DependencyInterfaces;
using AppointmentApp.Droid.DependencyInterfaces;

[assembly: Dependency(typeof (INetworkDependencyInterface_Android))]
namespace AppointmentApp.Droid.DependencyInterfaces
{
	public class INetworkDependencyInterface_Android : INetworkDependencyInterface
	{
		public INetworkDependencyInterface_Android ()
		{
		}

		public bool isConnected() {
			var context = Forms.Context;
			var connectivityManager = (ConnectivityManager)(((Activity)context).GetSystemService("connectivity"));


			var activeConnection = connectivityManager.ActiveNetworkInfo;
			if ((activeConnection != null) && activeConnection.IsConnected) {
				return true;
			} else {
				return false;
			}
		}
	}
}

