using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using AppointmentApp.Models;
using AppointmentApp.Managers;
using AppointmentApp.Interfaces;
using AppointmentApp.Handlers;

namespace AppointmentApp
{
	public class DoctorPage : ContentPage, INetworkFetcherInterface
	{
		NetworkHandler _networkHandler;
		/// <summary>
		/// The doctor identifier.
		/// </summary>
		string _doctorId;


		/// <summary>
		/// Sets the doctor identifier.
		/// </summary>
		/// <value>The doctor identifier.</value>
		public string DoctorId  {
			set {
				_doctorId = value;
			}
		}

		/// <summary>
		/// The activity indicator.
		/// </summary>
		private StackLayout _activityIndicatorStack;

		/// <summary>
		/// The data stack.
		/// </summary>
		private StackLayout _dataStack;

		//***** UI related instance elements
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
	
			 _dataStack = new StackLayout {
				Spacing = 10,
				Padding = 20,
				Orientation = StackOrientation.Vertical,
				VerticalOptions = LayoutOptions.StartAndExpand,
				Children = { nameStackLayout ,openingHoursStackLayout,addressLayout,_map }
			};
					
			ActivityIndicator indicator = new ActivityIndicator (){
				VerticalOptions = LayoutOptions.FillAndExpand,
				HorizontalOptions = LayoutOptions.Center,
				Color = Color.Black,
				BackgroundColor = Color.White,
				IsRunning = true,
				IsVisible = true,

			};

			_activityIndicatorStack = new StackLayout {
				HeightRequest = 35,
				WidthRequest = 35,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Children = {indicator } 

			};
			_activityIndicatorStack.IsVisible = true;
			_dataStack.IsVisible = false;

			Content =  new StackLayout {
				Orientation = StackOrientation.Vertical,
				VerticalOptions = LayoutOptions.FillAndExpand,
				Children = { _dataStack,_activityIndicatorStack }
			};;
		}


		/// <summary>
		/// Raises the appearing event.
		/// </summary>
		protected override void OnAppearing () {
			base.OnAppearing ();

			if (DoctorsDataManager.Instance.isDoctorAlreadyLoaded (_doctorId)) {
				DoctorModel doctor = DoctorsDataManager.Instance.GetModelById (_doctorId);
				BindingContext = doctor;

				_dataStack.IsVisible = true;
				_activityIndicatorStack.IsVisible = false;
			} else {
				_dataStack.IsVisible = false;
				_activityIndicatorStack.IsVisible = true;

				Dictionary<string,string> networkParms = new Dictionary<string,string> ();
				networkParms.Add (Constants.Constants.PARAM_ID, _doctorId);
				_networkHandler = new NetworkHandler ("doctors/{id}", networkParms, this, 1);
				_networkHandler.GetJsonData (true, Constants.Constants.JSON_DOCTORS);

			}
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

				Geocoder gecoder = new Geocoder ();
				string address = doctor.Street + " "+ doctor.PostalCode+" "+doctor.City;
				var positions  = await gecoder.GetPositionsForAddressAsync(address);
				List<Position> positionsArr = positions.ToList();

				if ( positionsArr.Count > 0  ) {
					Position position = positionsArr[0]; // Latitude, Longitude
				
					var mapSpan = MapSpan.FromCenterAndRadius(position, Distance.FromMiles(0.3));
					var mapPin = new Pin {
						Type = PinType.Place,
						Position = position,
						Label = "custom pin",
						Address = "custom detail info"
					};
					_map.MoveToRegion(mapSpan);
					_map.Pins.Add(mapPin);
				} 
				
			} catch (Exception e ) {
				System.Diagnostics.Debug.WriteLine ("EXception in Doctor Page OnBindingContextChanged,, {0}", e.ToString ());
			}
		}

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

		#region INetworkInterface Delegates 

		/// <summary>
		/// Ons the success.
		/// </summary>
		/// <param name="responseContent">Response content.</param>
		/// <param name="pRequestTag">P request tag.</param>
		public void onSuccess(string jsonResponse, int pRequestTag) {
			DoctorsDataManager.Instance.LoadDoctorsWithJson (jsonResponse);

			Xamarin.Forms.Device.BeginInvokeOnMainThread (() => {
				_activityIndicatorStack.IsVisible = false;
				DoctorModel doctor = DoctorsDataManager.Instance.GetModelById(_doctorId);
				if (doctor != null ) {
					_dataStack.IsVisible = true;
					_activityIndicatorStack.IsVisible = false;
					BindingContext = doctor;
				} else {
					//Cound not find the doctor
					//Navigation.PopAsync(true);
					_dataStack.IsVisible = false;
					_activityIndicatorStack.IsVisible = false;
				}

			});
		}

		/// <summary>
		/// Ons the failure.
		/// </summary>
		/// <param name="error">Error.</param>
		/// <param name="pRequestTag">P request tag.</param>
		public void onFailure(string error, int pRequestTag) {
			Xamarin.Forms.Device.BeginInvokeOnMainThread (() => {
				System.Diagnostics.Debug.WriteLine ("SomeError occured");
				_dataStack.IsVisible = false;
				_activityIndicatorStack.IsVisible = false;
			});
		}

		#endregion
	}
}

