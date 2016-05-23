using System;
using Xamarin.Forms;
using AppointmentApp.Views;

namespace AppointmentApp
{
	public class App : Application
	{
		public App ()
		{
			// The root page of your application
//			MainPage = new NavigationPage( new ContentPage {
//				Content = new StackLayout {
//					VerticalOptions = LayoutOptions.Center,
//					Children = {
//						new Label {
//							XAlign = TextAlignment.Center,
//							Text = "Welcome to Xamarin Forms!"
//						}
//					}
//				}
//			});

			var navigationPage = new NavigationPage (new AppointmentsPage ());

			Resources = new ResourceDictionary ();
			Resources.Add ("barColor", Color.FromHex("ff880e"));

			navigationPage.BarBackgroundColor = (Color) App.Current.Resources ["barColor"];
			navigationPage.BarTextColor = Color.White;


			MainPage = navigationPage;
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

