

// ModifyWordsPage.xaml.cs
using System;
using System.IO;
using Xamarin.Forms;

namespace XamarinWordle
{
	public partial class ModifyWordsPage : ContentPage
	{
		public ModifyWordsPage()
		{
			InitializeComponent();
		}

		private void ModifyButton_Clicked(object sender, EventArgs e)
		{
			string wordToModify = wordEntry.Text?.Trim();
			string newTranslation = translationEntry.Text?.Trim();

			if (string.IsNullOrEmpty(wordToModify) || string.IsNullOrEmpty(newTranslation))
			{
				DisplayAlert("Error", "Please enter both word and translation.", "OK");
				return;
			}

			try
			{
				// Read all lines from the words.txt file
				string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "words.txt");
				string[] lines = File.ReadAllLines(filePath);

				// Find and modify the line with the specified word
				for (int i = 0; i < lines.Length; i++)
				{
					string[] parts = lines[i].Split(':');
					if (parts.Length == 2 && parts[0].Trim() == wordToModify)
					{
						lines[i] = $"{wordToModify}:{newTranslation}";
						break;
					}
				}

				// Write the modified lines back to the file
				File.WriteAllLines(filePath, lines);

				DisplayAlert("Success", "Word modified successfully.", "OK");
			}
			catch (Exception ex)
			{
				DisplayAlert("Error", $"Error modifying word: {ex.Message}", "OK");
			}
		}
	}
}
