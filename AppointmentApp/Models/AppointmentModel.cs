using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AppointmentApp.Models
{
	/// <summary>
	/// Appointment model.
	/// </summary>
	public class AppointmentModel
	{
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>The identifier.</value>
		[JsonProperty("id",Required = Required.Always)]
		public string Id {set; get; }

		//public string dateStr { set; get; }
		/// <summary>
		/// Gets or sets the doctor identifier.
		/// </summary>
		/// <value>The doctor identifier.</value>
		[JsonProperty("doctor_id",Required = Required.Always)]
		public string DoctorId {set; get; }

		/// <summary>
		/// Gets or sets the date.
		/// </summary>
		/// <value>The date.</value
		[JsonProperty("date",Required = Required.Always)]
		public DateTime Date { set; get; }

		/// <summary>
		/// Gets or sets the notes.
		/// </summary>
		/// <value>The notes.</value>
		[JsonProperty("notes",Required = Required.AllowNull , NullValueHandling = NullValueHandling.Ignore)]
		public List<string> Notes {set; get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="AppointmentApp.Models.AppointmentModel"/> class.
		/// </summary>
		public AppointmentModel () {
		}
	}
}

