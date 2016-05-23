using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AppointmentApp.Models
{
	/// <summary>
	/// Doctor model.
	/// </summary>
	public class DoctorModel
	{
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>The identifier.</value>
		[JsonProperty("id",Required = Required.Always)]
		public string Id {set; get; }

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		[JsonProperty("name",Required = Required.Always)]
		public string Name {set; get; }

		/// <summary>
		/// Gets or sets the opening hours.
		/// </summary>
		/// <value>The opening hours.</value>
		[JsonProperty("opening_hours",Required = Required.Always)]
		public List<string> OpeningHours {set; get; }

		/// <summary>
		/// Gets or sets the street.
		/// </summary>
		/// <value>The street.</value>
		[JsonProperty("street",Required = Required.Always)]
		public string Street {set; get; }

		/// <summary>
		/// Gets or sets the postal code.
		/// </summary>
		/// <value>The postal code.</value>
		[JsonProperty("postal_code",Required = Required.Always)]
		public string PostalCode {set; get; }

		/// <summary>
		/// Gets or sets the city.
		/// </summary>
		/// <value>The city.</value>
		[JsonProperty("city",Required = Required.Always)]
		public string City {set; get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="AppointmentApp.Models.DoctorModel"/> class.
		/// </summary>
		public DoctorModel () {
		}
	}
}

