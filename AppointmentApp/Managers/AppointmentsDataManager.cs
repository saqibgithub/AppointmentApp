using System;
using System.Collections.Generic;
using AppointmentApp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AppointmentApp.Managers
{
	public class AppointmentsDataManager
	{
		/// <summary>
		/// Initializes the <see cref="AppointmentApp.AppointmentsDataManager"/> class.
		/// </summary>
		private static AppointmentsDataManager _instance = null;

		/// <summary>
		/// Gets the instance.
		/// </summary>
		/// <value>The instance.</value>
		public static AppointmentsDataManager Instance {
			get {
				if (_instance == null) {
					_instance = new AppointmentsDataManager ();
				}
				return _instance;
			}
		}

		/// <summary>
		/// The appointments.
		/// </summary>
		private List<AppointmentModel> _appointments = null;

		/// <summary>
		/// Gets the appointments.
		/// </summary>
		/// <value>The appointments.</value>
		public List<AppointmentModel> Appointments {
			get {
				return _appointments;
			}
		}



		/// <summary>
		/// Initializes a new instance of the <see cref="AppointmentApp.AppointmentsDataManager"/> class.
		/// </summary> 
		private AppointmentsDataManager () {
			_appointments = new List<AppointmentModel> ();
		}

		/// <summary>
		/// Loads the appointments.
		/// </summary>
		public void LoadAppointments () {

			try { 
				_appointments = JArray.Parse (Constants.Constants.JSON_APPOINTMENTS).ToObject<List<AppointmentModel>> ();
			} catch (Exception e) {
				System.Diagnostics.Debug.WriteLine ("Exception in parsing appointment models = {0}",e.ToString());
			}


		}

	}
}

