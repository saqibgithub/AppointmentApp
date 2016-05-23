using System;
using Xamarin.Forms;

namespace AppointmentApp.Views
{
	public class LoadingPage : ContentPage
	{
		public LoadingPage ()
		{
			StackLayout stackLayout = new StackLayout {
				VerticalOptions = LayoutOptions.Center,
				Children = {
					new Label {
						XAlign = TextAlignment.Center,
						Text = "Loading..."
					},
					new ActivityIndicator {
						IsRunning = true,
						Color = Color.Black
					}
				}
			};

			Content = stackLayout;

		}
	}
}

