using System;
using System.Collections.Generic;
using AppointmentApp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

/// <summary>
/// Doctors data manager.
/// </summary>
namespace AppointmentApp.Managers
{
	/// <summary>
	/// Doctors data manager.
	/// </summary>
	public class DoctorsDataManager
	{
		/// <summary>
		/// The instance.
		/// </summary>
		private static DoctorsDataManager _instance = null;

		/// <summary>
		/// Gets the instance.
		/// </summary>
		/// <value>The instance.</value>
		public static DoctorsDataManager Instance {
			get {
				if (_instance == null) {
					_instance = new DoctorsDataManager ();
				}
				return _instance;
			}
		}

		/// <summary>
		/// The doctors.
		/// </summary>
		List<DoctorModel> _doctors = null;

		/// <summary>
		/// The identifier to model.
		/// </summary>
		Dictionary<string, DoctorModel> _idToDoctorModelMap;

		/// <summary>
		/// Gets the doctors.
		/// </summary>
		/// <value>The doctors.</value>
		public List<DoctorModel> Doctors {
			get {
				return _doctors;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="AppointmentApp.Managers.DoctorsDataManager"/> class.
		/// </summary>
		private DoctorsDataManager () {
			_idToDoctorModelMap = new Dictionary<string, DoctorModel> ();
		}


		/// <summary>
		/// Loads the doctors.
		/// </summary>
		public void LoadDoctorsWithJson (string json) {
			try { 
				_doctors = JArray.Parse (json).ToObject<List<DoctorModel>>();
				BuildDoctorsMap ();

			} catch (Exception e) {
				System.Diagnostics.Debug.WriteLine ("Exception in Loading Doctors = {0}", e.ToString ());
			}
		}

		/// <summary>
		/// Builds the doctors map.
		/// </summary>
		public void BuildDoctorsMap () {
			/// loop through each doctor and build map
			foreach (var doctor in _doctors) {
				if (!_idToDoctorModelMap.ContainsKey (doctor.Id)) {
					_idToDoctorModelMap.Add (doctor.Id, doctor);
				}
			}
		}

		/// <summary>
		/// Gets the model by identifier.
		/// </summary>
		/// <returns>The model by identifier.</returns>
		/// <param name="id">Identifier.</param>
		public DoctorModel GetModelById(string id ) {
			if(_idToDoctorModelMap.ContainsKey ( id )) {
				return _idToDoctorModelMap [id];
			}
			return null;
		}

		/// <summary>
		/// Ises the doctor already loaded.
		/// </summary>
		/// <returns><c>true</c>, if doctor already loaded was ised, <c>false</c> otherwise.</returns>
		/// <param name="doctorId">Doctor identifier.</param>
		public bool isDoctorAlreadyLoaded (string doctorId)  {
			return GetModelById (doctorId) != null;
		}

			
	}
}

