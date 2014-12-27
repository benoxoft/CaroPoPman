using System;
using System.Data;

namespace CaroPoPman.Animaux
{
	public class Visite
	{
		private readonly DataRow _row;

		public Visite (DataRow row)
		{
			_row = row;
		}

		public DateTime DateVisite { 
			get { return _row ["DateVisite"]; }
		}

		public string Description { 
			get { return _row["Description"] }
		}

		public string Commentaire {
			get { return _row ["Commentaire"]; }
		}

	}
}

