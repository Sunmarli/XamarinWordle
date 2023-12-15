using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using System.Xml;
using XamarinWordle.Models;
using Xamarin.Forms;

namespace XamarinWordle
{
	public class MainPageViewModel : INotifyPropertyChanged
	{
		private static readonly Color DarkGray = Color.FromHex("#3C373D");
		private static readonly Color Success = Color.FromHex("#92D65B");
		private static readonly Color LightGray = Color.FromHex("#514E51");

		public static readonly string[] _words = { "Title", "Bison", "Angel", "Tonal", "Eagle", "Entry" };

		public string _correctAnswer;

		private int currentGuess = 0;

		private ObservableCollection<Guess> _userGuesses;
		public ObservableCollection<Guess> UserGuesses
		{
			get { return _userGuesses; }
			set
			{
				if (_userGuesses != value)
				{
					_userGuesses = value;
					OnPropertyChanged();
				}
			}
		}

		public ICommand UpdateUserInput { get; }

		private string _userInput;
		public string UserInput
		{
			get { return _userInput; }
			set
			{
				if (_userInput != value)
				{
					_userInput = value;
					OnPropertyChanged();
				}
			}
		}

		public MainPageViewModel()
		{
			Random rand = new Random();
			_correctAnswer = _words[rand.Next(0, _words.Length)].ToUpper();

			UpdateUserInput = new Command(OnUpdateUserInput);

			UserGuesses = new ObservableCollection<Guess>();

			UserInput = "";

			SetupGameBoard();

		}

		private void SetupGameBoard()
		{
			Tile baseTile = new Tile()
			{
				Color = Color.LightGray,
				isValid = false,
			};

			for (int i = 0; i < 5; i++)
			{
				var baseGuess = new Guess();
				baseGuess.Row = i;
				baseGuess.Tiles.Add(baseTile);
				baseGuess.Tiles.Add(baseTile);
				baseGuess.Tiles.Add(baseTile);
				baseGuess.Tiles.Add(baseTile);
				baseGuess.Tiles.Add(baseTile);

				UserGuesses.Add(baseGuess);
			}
		}

		private void OnUpdateUserInput(object sender)
		{
			var btn = sender as Xamarin.Forms.Button;

			if (btn.Text == "Enter" && UserInput.Length == 5)
			{
				SubmitGuess();
			}
			else if (btn.Text == "-")
			{
				if (UserInput.Length > 0)
				{
					UserInput = UserInput.Remove(UserInput.Length - 1, 1);
				}
			}
			else
			{
				if (UserInput.Length < 5)
				{
					UserInput = UserInput + btn.Text;
				}
			}
		}

		private void SubmitGuess()
		{
			var newGuess = new Guess();

			for (var i = 0; i < UserInput.Length; i++)
			{
				var tile = new Tile()
				{
					Color = ValidateCharacter(UserInput[i], i) ? Success : DarkGray,
					isValid = ValidateCharacter(UserInput[i], i) ? true : false,
					DisplayCharacter = UserInput[i],
				};
				newGuess.Tiles.Add(tile);
			}

			UserGuesses.RemoveAt(currentGuess);
			UserGuesses.Insert(currentGuess, newGuess);

			UserInput = "";

			currentGuess++;

			if (currentGuess >= 5)
			{
				//Game over...
			}
		}

		private bool ValidateCharacter(char c, int i)
		{
			Console.WriteLine(UserInput[i] + " - " + _correctAnswer[i]);
			if (UserInput[i] == _correctAnswer[i])
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName] string name = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}
	}
}
