// AddWordPage.xaml.cs
using System;
using System.IO;
using Xamarin.Forms;

namespace XamarinWordle
{
	public partial class AddWordPage : ContentPage
	{
		public AddWordPage()
		{
			InitializeComponent();
		}

		private void AddWordButton_Clicked(object sender, EventArgs e)
		{
			string newWord = newWordEntry.Text?.Trim();
			string newTranslation = newTranslationEntry.Text?.Trim();

			if (string.IsNullOrEmpty(newWord) || string.IsNullOrEmpty(newTranslation))
			{
				DisplayAlert("Error", "Please enter both word and translation.", "OK");
				return;
			}

			try
			{
				// Copy the words.txt file from the assets to the app's internal storage
				string assetsFilePath = "words.txt";
				string writableFilePath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "words_copy.txt");

				using (var assetStream = Android.App.Application.Context.Assets.Open(assetsFilePath))
				using (var writableStream = new FileStream(writableFilePath, FileMode.Create, FileAccess.Write))
				{
					assetStream.CopyTo(writableStream);
				}

				// Append the new word and translation to the copied file
				File.AppendAllText(writableFilePath, $"{newWord}:{newTranslation}{Environment.NewLine}");


				// Display the contents of both the source and destination files
				string sourceContent = File.ReadAllText(assetsFilePath);
				string destinationContent = File.ReadAllText(writableFilePath);

				Console.WriteLine($"Source Content (words.txt): {sourceContent}");
				Console.WriteLine($"Destination Content (words_copy.txt): {destinationContent}");

				DisplayAlert("Success", "Word added successfully.", "OK");
			}
			catch (Exception ex)
			{
				DisplayAlert("Error", $"Error adding word: {ex.Message}", "OK");
			}
		}

	}
}
