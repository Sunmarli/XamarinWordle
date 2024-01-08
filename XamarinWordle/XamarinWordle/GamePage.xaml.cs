// MainPage.xaml.cs
using System;
using System.IO;
using Xamarin.Forms;

namespace XamarinWordle
{
	public partial class GamePage : CarouselPage
	{
		private StreamReader streamReader;
		private int currentIndex = 0;

		public GamePage()
		{
			InitializeComponent();

			try
			{
				// Open the StreamReader with the words.txt file from the Assets folder
				streamReader = new StreamReader(Android.App.Application.Context.Assets.Open("words.txt"));

				// Create pages for each word and translation
				while (!streamReader.EndOfStream)
				{
					string line = streamReader.ReadLine();
					string[] parts = line.Split(':');
					if (parts.Length == 2)
					{
						string word = parts[0].Trim();
						string translation = parts[1].Trim();

						ContentPage page = new ContentPage();
						StackLayout layout = new StackLayout();

						Label wordLabel = new Label { Text = $"Word: {word}", HorizontalOptions = LayoutOptions.CenterAndExpand, VerticalOptions = LayoutOptions.CenterAndExpand };
						Label translationLabel = new Label { Text = $"Translation: {translation}", HorizontalOptions = LayoutOptions.CenterAndExpand, VerticalOptions = LayoutOptions.CenterAndExpand, IsVisible = false };

						// Add a tap gesture recognizer to the word label
						wordLabel.GestureRecognizers.Add(new TapGestureRecognizer
						{
							Command = new Command(() =>
							{
								wordLabel.IsVisible = false;
								translationLabel.IsVisible = true;
							})
						});

						layout.Children.Add(wordLabel);
						layout.Children.Add(translationLabel);
						page.Content = layout;

						Children.Add(page);
					}
				}

				// Display the first page
				DisplayWordAndTranslation(currentIndex);
			}
			catch (Exception ex)
			{
				// Handle exceptions (e.g., file not found, format issues, etc.)
				DisplayAlert("Error", $"Error opening words.txt: {ex.Message}", "OK");
			}
		}

		private void DisplayWordAndTranslation(int index)
		{
			// Set the current page
			CurrentPage = Children[index];
		}

		// Handle swipe events to navigate between pages
		protected override void OnCurrentPageChanged()
		{
			base.OnCurrentPageChanged();

			// Get the index of the current page
			currentIndex = Children.IndexOf(CurrentPage);
		}
	}
}

