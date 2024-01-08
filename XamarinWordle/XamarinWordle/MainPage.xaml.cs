

// MainPage.xaml.cs
using System;
using Xamarin.Forms;

namespace XamarinWordle
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

		private void StartGameButton_Clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new GamePage());
		}

		private void ModifyWordsButton_Clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new ModifyWordsPage());
		}
		private void AddWordButton_Clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new AddWordPage());
		}
	}
}