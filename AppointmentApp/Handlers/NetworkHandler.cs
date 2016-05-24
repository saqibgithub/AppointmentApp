using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;
using RestSharp;

using AppointmentApp.Interfaces;
using AppointmentApp.Constants;



namespace AppointmentApp.Handlers
{
	public class NetworkHandler
	{
		INetworkFetcherInterface _delegate;
		private int _requestTag;
		private string _methodName;
		private Dictionary<string,string> _params;
		private string _serverUrl;

		public void SetDelegate (INetworkFetcherInterface pDel) {
			_delegate = pDel;
		}

		public NetworkHandler (string pMethodName, Dictionary<string,string> pParams, INetworkFetcherInterface pDel, int pTag) {
			_delegate = pDel;
			_methodName = pMethodName;
			_params = pParams;
			_requestTag = pTag;
			_serverUrl = Constants.Constants.APP_SERVER_URL;
		}
			
		/// <summary>
		/// Get this instance.
		/// </summary>
		public async void GetAppointments (bool isSuccess) {
			await Task.Delay (5000);
			Task.Run (() => {   

				RestClient client = new RestClient (_serverUrl);
				if (_params == null) {
					_params = new Dictionary<string, string> ();
				}


				client.Timeout = 15;
				var request = new RestRequest (_methodName,Method.GET);

				foreach (KeyValuePair<string,string> lParams in _params) {
					request.AddUrlSegment (lParams.Key, lParams.Value);
				}

				EventWaitHandle Wait = new AutoResetEvent (false);

				client.ExecuteAsync (request, response =>  {
					try {
						
						if (!isSuccess && ( response == null || response.Content == null || response.Content.CompareTo ("") == 0)) {
							_delegate?.onFailure ("Some Error Occured",_requestTag);
						} else {
							_delegate.onSuccess(Constants.Constants.JSON_APPOINTMENTS , _requestTag);
						}
					} catch (Exception e) {
						System.Diagnostics.Debug.WriteLine("Exception in network response handling Exception = {0}",e.ToString());
						_delegate.onFailure ("Some Error Occured",_requestTag);
					} 

				});
				Wait.WaitOne ();

			});
		}

	}
}

