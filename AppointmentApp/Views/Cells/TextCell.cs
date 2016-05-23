using System;
using Xamarin.Forms;

namespace AppointmentApp
{
	public class TextCell : ViewCell
	{
		Label _textLabel;
		public TextCell ()
		{
			_textLabel = new Label {
				VerticalTextAlignment = TextAlignment.Center,
				HorizontalOptions = LayoutOptions.StartAndExpand
			};

			View = new StackLayout {
				Padding = new Thickness (20, 0, 20, 0),
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.StartAndExpand,
				Children = { _textLabel }
			};
				
		}

		/// <summary>
		/// Override this method to execute an action when the BindingContext changes.
		/// </summary>
		/// <remarks></remarks>
		protected override void OnBindingContextChanged () {
			base.OnBindingContextChanged (); 
			try {
				string textValue = (string)BindingContext;
				_textLabel.Text = textValue;
				
			} catch (Exception e) {
				System.Diagnostics.Debug.WriteLine ("Exception in OnBindingContextChanged in TextCell with exception: {0}", e.ToString ());
			}
		}
	}
}

