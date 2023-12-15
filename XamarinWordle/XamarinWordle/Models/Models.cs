using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinWordle.Models
{
	public class Tile
	{
		public char? DisplayCharacter { get; set; }
		public Color Color { get; set; }
		public bool isValid { get; set; }
	}

	public class Guess
	{
		public int Row { get; set; }
		public List<Tile> Tiles { get; set; } = new List<Tile>();
	}
}
