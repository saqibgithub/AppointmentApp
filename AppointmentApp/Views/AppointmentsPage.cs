using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppointmentApp.Views.Cells;
using AppointmentApp.Models;
using AppointmentApp.Managers;
using AppointmentApp.Constants;
using Xamarin.Forms;


namespace AppointmentApp.Views
{
	public class AppointmentsPage : ContentPage
	{
		/// <summary>
		/// The list view.
		/// </summary>
		private ListView _listView;

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

			_listView.ItemTemplate  = new DataTemplate(typeof(AppointmentCell));
			_listView.ItemTapped += ListListener ;

			Content = new StackLayout {
				Orientation = StackOrientation.Vertical,
				VerticalOptions = LayoutOptions.FillAndExpand,
				BackgroundColor = Color.White,
				Children = { _listView }
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
			AppointmentsDataManager.Instance.LoadAppointments ();
			DoctorsDataManager.Instance.LoadDoctors ();

			_listView.ItemsSource = AppointmentsDataManager.Instance.Appointments;

			MessagingCenter.Subscribe<AppointmentCell, AppointmentModel> (this, Constants.Constants.MESSEGE_CENTER_OPEN_DOCTOR_PAGE, (sender  ,appointmentModel) =>  {
				DoctorModel doctor = DoctorsDataManager.Instance.GetModelById(appointmentModel.DoctorId);
				var doctorPage = new DoctorPage();
				doctorPage.BindingContext = doctor;
				 Navigation.PushAsync(doctorPage,true);
			
			});
		}

		/// <summary>
		/// Raises the disappearing event.
		/// </summary>
		protected override void OnDisappearing () {
			base.OnDisappearing ();
			MessagingCenter.Unsubscribe<AppointmentCell, AppointmentModel> (this, Constants.Constants.MESSEGE_CENTER_OPEN_DOCTOR_PAGE);
		}
	
	}
}

