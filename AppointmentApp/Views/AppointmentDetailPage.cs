using System;
using Xamarin.Forms;
using AppointmentApp.Models;
using AppointmentApp.Managers;

namespace AppointmentApp.Views
{
	public class AppointmentDetailPage : ContentPage
	{
		Label _dateTime;
		Label _doctorName;
		ListView _notesList;

		public AppointmentDetailPage ()
		{
			Title = "Detail";
			//** DATE LABEL
			Label dateTimeLabel = new Label {
				Text = "Date/Time:",
				FontAttributes = FontAttributes.Bold,
			};
			_dateTime = new Label {};
			StackLayout dateTimeStackLayout = new StackLayout {
				Spacing = 5,
				Orientation = StackOrientation.Vertical,
				VerticalOptions = LayoutOptions.StartAndExpand,
				Children = { dateTimeLabel, _dateTime  }
			};

			//** Doctors Label
			Label doctorLabel = new Label {
				Text = "Doctor:",
				FontAttributes = FontAttributes.Bold,
			};
			_doctorName = new Label { };
			StackLayout doctorStackLayout = new StackLayout {
				Spacing = 5,
				Orientation = StackOrientation.Vertical,
				VerticalOptions = LayoutOptions.StartAndExpand,
				Children = { doctorLabel, _doctorName  }
			};

			//** Opening Hours List
			Label notesLabel = new Label {
				Text = "Notes",
				FontAttributes = FontAttributes.Bold,
			};
			_notesList = new ListView () { RowHeight = 30 };
			_notesList.ItemTemplate = new DataTemplate (typeof(TextCell));
			StackLayout notesStackLayout = new StackLayout {
				Spacing = 5,
				Orientation = StackOrientation.Vertical,
				VerticalOptions = LayoutOptions.StartAndExpand,
				Children = { notesLabel, _notesList  }
			};

			///
			StackLayout finalStack = new StackLayout {
				Spacing = 10,
				Padding = 20,
				Orientation = StackOrientation.Vertical,
				VerticalOptions = LayoutOptions.StartAndExpand,
				Children = { dateTimeStackLayout, doctorStackLayout ,notesStackLayout }
			};

			Content = finalStack;
		
		}

		/// <summary>
		/// Override this method to execute an action when the BindingContext changes.
		/// </summary>
		/// <remarks></remarks>
		protected override void OnBindingContextChanged () {
			base.OnBindingContextChanged ();
			try {
				AppointmentModel appointment = (AppointmentModel)BindingContext;

				_dateTime.Text = appointment.Date.ToString("g");
				DoctorModel doctor =  DoctorsDataManager.Instance.GetModelById(appointment.DoctorId);
				if ( doctor != null ) {
					_doctorName.Text = doctor.Name;
				}

				_notesList.ItemsSource = appointment.Notes;


			} catch ( Exception e ) {
				System.Diagnostics.Debug.WriteLine ("Exception in AppointmentDetailPage::OnBindingContextChanged, {0}", e.ToString ());
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
	}
}

