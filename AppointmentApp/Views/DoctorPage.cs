using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using AppointmentApp.Models;

namespace AppointmentApp
{
	public class DoctorPage : ContentPage
	{
		Label _name;
		ListView _openingHoursList;

		Label _streetAddress;
		Label _postalCodeAddress;

		Map _map;

		public DoctorPage ()
		{
			Title = "Doctor";
			//******* DATE LABEL
			Label nameLabel = new Label {
				Text = "Name:",
				FontAttributes = FontAttributes.Bold,
			};
			_name = new Label {};
			StackLayout nameStackLayout = new StackLayout {
				Spacing = 5,
				Orientation = StackOrientation.Vertical,
				VerticalOptions = LayoutOptions.StartAndExpand,
				Children = { nameLabel, _name  }
			};
					
			//******** Opening Hours List
			Label openinghoursLabel = new Label {
				Text = "Opening Hours:",
				FontAttributes = FontAttributes.Bold,
			};
			_openingHoursList = new ListView () { RowHeight = 30 };
			_openingHoursList.ItemTemplate = new DataTemplate (typeof(TextCell));
			StackLayout openingHoursStackLayout = new StackLayout {
				Spacing = 5,
				Orientation = StackOrientation.Vertical,
				VerticalOptions = LayoutOptions.StartAndExpand,
				Children = { openinghoursLabel, _openingHoursList  }
			};


			//******* Address Label
			Label addressLabel = new Label {
				Text = "Address:",
				FontAttributes = FontAttributes.Bold,
			};
			_streetAddress = new Label { };
			_postalCodeAddress = new Label { };
			StackLayout innerAddresStackLayout = new StackLayout {
				Orientation = StackOrientation.Vertical,
				VerticalOptions = LayoutOptions.StartAndExpand,
				Children = { _streetAddress, _postalCodeAddress }
			};
			StackLayout addressLayout = new StackLayout {
				Spacing = 5,
				Orientation = StackOrientation.Vertical,
				VerticalOptions = LayoutOptions.StartAndExpand,
				Children = { addressLabel, innerAddresStackLayout  }
			};


			_map = new Map () {
				IsShowingUser = true,
				HeightRequest = 100,
				WidthRequest = 960,
				VerticalOptions = LayoutOptions.FillAndExpand
			};
	
			StackLayout finalStack = new StackLayout {
				Spacing = 10,
				Padding = 20,
				Orientation = StackOrientation.Vertical,
				VerticalOptions = LayoutOptions.StartAndExpand,
				Children = { nameStackLayout ,openingHoursStackLayout,addressLayout,_map }
			};

			Content = finalStack;
		}

		/// <summary>
		/// Override this method to execute an action when the BindingContext changes.
		/// </summary>
		/// <remarks></remarks>
		protected async override void OnBindingContextChanged () {
			base.OnBindingContextChanged ();

			try {
				DoctorModel doctor = (DoctorModel) BindingContext;

				_name.Text = doctor.Name;
				_openingHoursList.ItemsSource = doctor.OpeningHours;

				_streetAddress.Text = doctor.Street;
				_postalCodeAddress.Text = doctor.PostalCode + " " + doctor.City;

				string address = doctor.Street + " "+ doctor.PostalCode+" "+doctor.City;

				Geocoder gecoder = new Geocoder ();
				var positions  = await gecoder.GetPositionsForAddressAsync(address);

				List<Position> positionsArr = positions.ToList();

				if ( positionsArr.Count > 0 ) {
					Position position = positionsArr[0]; // Latitude, Longitude
				
					var mapSpan = MapSpan.FromCenterAndRadius(position, Distance.FromMiles(0.3));

					var pin = new Pin {
						Type = PinType.Place,
						Position = position,
						Label = "custom pin",
						Address = "custom detail info"
					};
					_map.MoveToRegion(mapSpan);
					_map.Pins.Add(pin);
				}
				
			} catch (Exception e ) {
				System.Diagnostics.Debug.WriteLine ("EXception in Doctor Page OnBindingContextChanged,, {0}", e.ToString ());
			}
		}

		/// <summary>
		/// Raises the appearing event.
		/// </summary>
//		protected override void OnAppearing () {
//			base.OnAppearing ();
//
//
//		
//		}

		/// <summary>
		/// Application developers can override this method to provide behavior when the back button is pressed.
		/// </summary>
		/// <returns>To be added.</returns>
		/// <remarks>To be added.</remarks>
		protected override bool OnBackButtonPressed () {
			base.OnBackButtonPressed ();
			Navigation.PopAsync (true);
			return true;
		}
	}
}

