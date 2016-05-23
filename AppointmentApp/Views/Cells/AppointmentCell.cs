using System;
using Xamarin.Forms;
using AppointmentApp.Models;
using AppointmentApp.Managers;

namespace AppointmentApp.Views.Cells
{
	/// <summary>
	/// Appointment cell.
	/// </summary>
	public class AppointmentCell : ViewCell
	{
		/// <summary>
		/// The date label.
		/// </summary>
		Label _dateLabel;

		/// <summary>
		/// The time label.
		/// </summary>
		Label _timeLabel;

		/// <summary>
		/// The name of the doctor.
		/// </summary>
		Label _doctorName;

		/// <summary>
		/// Initializes a new instance of the <see cref="AppointmentApp.Views.Cells.AppointmentCell"/> class.
		/// </summary>
		public AppointmentCell ()
		{
			//*** DATE Label
			_dateLabel = new Label {
				VerticalTextAlignment = TextAlignment.Center,
				HorizontalOptions = LayoutOptions.StartAndExpand
			};
			_dateLabel.Text = "date";

			//*** DIVIDER LABEL
			Label divider1 = new Label {
				VerticalTextAlignment = TextAlignment.Center,
				HorizontalOptions = LayoutOptions.StartAndExpand
			};
			divider1.Text = "|";

			//*** TIME LABEL
			_timeLabel = new Label {
				VerticalTextAlignment = TextAlignment.Center,
				HorizontalOptions = LayoutOptions.StartAndExpand,
			};
			_timeLabel.Text = "time";

			//*** DIVIDER LABEL
			Label divider2 = new Label {
				VerticalTextAlignment = TextAlignment.Center,
				HorizontalOptions = LayoutOptions.StartAndExpand
			};
			divider2.Text = "|";

			//*** DOCTORNAME LABEL
			_doctorName = new Label {
				VerticalTextAlignment = TextAlignment.Center,
				HorizontalOptions = LayoutOptions.End
			};
			_doctorName.Text = "doctname";

			TapGestureRecognizer tapGesture = new TapGestureRecognizer();
			tapGesture.Tapped += DoctorNameTapped;
			_doctorName.GestureRecognizers.Add (tapGesture);

			var layout = new StackLayout {
				Padding = new Thickness (20, 0, 20, 0),
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.StartAndExpand,
				Children = { _dateLabel, divider1,_timeLabel,divider2,_doctorName }
			};

			View = layout;
		}

		/// <summary>
		/// Override this method to execute an action when the BindingContext changes.
		/// </summary>
		/// <remarks></remarks>
		protected override void OnBindingContextChanged() {
			base.OnBindingContextChanged ();

			try {
				AppointmentModel appointmentModel = (AppointmentModel) BindingContext;

				_dateLabel.Text = appointmentModel.Date.ToString("d");
				_timeLabel.Text = appointmentModel.Date.ToString("t");

				DoctorModel doctorModel = DoctorsDataManager.Instance.GetModelById(appointmentModel.DoctorId);
				if(doctorModel != null ) {
					_doctorName.Text = doctorModel.Name;
				}
			} catch (Exception e ) {
				System.Diagnostics.Debug.WriteLine (" Exception in OnBindingContextChanged : {0}",e.ToString());
			}
		}

		/// <summary>
		/// Doctors the name tapped.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		private void DoctorNameTapped (object sender, EventArgs arg) {
			try {
				AppointmentModel appointment = (AppointmentModel)BindingContext;
				MessagingCenter.Send<AppointmentCell,AppointmentModel>(this,Constants.Constants.MESSEGE_CENTER_OPEN_DOCTOR_PAGE, appointment);

			} catch (Exception e ) {
				System.Diagnostics.Debug.WriteLine (" Exception occured in DoctorNameTapped, with exception stack trace {0}", e.ToString ());
			}
		}

	}
}

