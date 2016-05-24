using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppointmentApp.Views.Cells;
using AppointmentApp.Models;
using AppointmentApp.Managers;
using AppointmentApp.Constants;
using AppointmentApp.Handlers;
using AppointmentApp.Interfaces;
using Xamarin.Forms;
using Newtonsoft.Json.Linq;


namespace AppointmentApp.Views
{
	public class AppointmentsPage : ContentPage , INetworkFetcherInterface
	{

		NetworkHandler _networkHandler;
		/// <summary>
		/// The list view.
		/// </summary>
		private ListView _listView;

		/// <summary>
		/// The activity indicator.
		/// </summary>
		private StackLayout _activityIndicatorStack;
	


		/// <summary>
		/// Initializes a new instance of the <see cref="AppointmentApp.AppointmentsPage"/> class.
		/// </summary>
		public AppointmentsPage ()
		{
			NavigationPage.SetHasNavigationBar (this, true);

			Title = "Appointments";

			_listView = new ListView {
				RowHeight = 40
			};
			_listView.IsVisible = false;

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

			_listView.ItemTemplate  = new DataTemplate(typeof(AppointmentCell));
			_listView.ItemTapped += ListListener ;

			Content = new StackLayout {
				Orientation = StackOrientation.Vertical,
				VerticalOptions = LayoutOptions.FillAndExpand,
				BackgroundColor = Color.White,
				Children = { _activityIndicatorStack,_listView }
			};
		}

		/// <summary>
		/// List Cell Tap Event
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		public void ListListener(object sender, ItemTappedEventArgs e) {
			AppointmentModel appointment = (AppointmentModel)e.Item;
			var appointmentPage = new AppointmentDetailPage ();
			appointmentPage.BindingContext = appointment;
			Navigation.PushAsync (appointmentPage, true);
		}

		/// <summary>
		/// Raises the appearing event.
		/// </summary>
		protected override void OnAppearing() {
			base.OnAppearing ();

			//TODO: load them in loading view or from parsing
			//AppointmentsDataManager.Instance.LoadAppointments ();
			DoctorsDataManager.Instance.LoadDoctors ();
		
			MessagingCenter.Subscribe<AppointmentCell, AppointmentModel> (this, Constants.Constants.MESSEGE_CENTER_OPEN_DOCTOR_PAGE, (sender  ,appointmentModel) =>  {
				DoctorModel doctor = DoctorsDataManager.Instance.GetModelById(appointmentModel.DoctorId);
				var doctorPage = new DoctorPage();
				doctorPage.BindingContext = doctor;
				 Navigation.PushAsync(doctorPage,true);
			
			});


			/// IF appointments are not already loaded
			if (!AppointmentsDataManager.Instance.IsAppointmentsLoaded) {
				// Load
				_activityIndicatorStack.IsVisible = true;
				_listView.IsVisible = false;

				Dictionary<string,string> networkParms = new Dictionary<string,string> ();
				networkParms.Add (Constants.Constants.PARAM_ID, Constants.Constants.USER_ID);
				_networkHandler = new NetworkHandler ("users/{id}/appointments", networkParms, this, 1);
				_networkHandler.GetAppointments (true);
			}
		}

		/// <summary>
		/// Raises the disappearing event.
		/// </summary>
		protected override void OnDisappearing () {
			base.OnDisappearing ();
			MessagingCenter.Unsubscribe<AppointmentCell, AppointmentModel> (this, Constants.Constants.MESSEGE_CENTER_OPEN_DOCTOR_PAGE);
		}


		#region INetworkInterface Delegates 

		/// <summary>
		/// Ons the success.
		/// </summary>
		/// <param name="responseContent">Response content.</param>
		/// <param name="pRequestTag">P request tag.</param>
		public void onSuccess(string jsonResponse, int pRequestTag) {
			AppointmentsDataManager.Instance.LoadAppointmentsWithJson (jsonResponse);

			Xamarin.Forms.Device.BeginInvokeOnMainThread (() => {
				_activityIndicatorStack.IsVisible = false;
				_listView.IsVisible = true;
				_listView.ItemsSource = AppointmentsDataManager.Instance.Appointments;
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
				_activityIndicatorStack.IsVisible = false;
				_listView.IsVisible = true;
				_listView.ItemsSource = null;
			});
		}

		#endregion
	
	}
}

