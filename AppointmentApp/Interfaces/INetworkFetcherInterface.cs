using System;
using Newtonsoft.Json.Linq;

namespace AppointmentApp.Interfaces
{
	public interface INetworkFetcherInterface
	{
		void onSuccess(string jsonResponse, int pRequestTag);
		void onFailure(string error, int pRequestTag);
	}
}

