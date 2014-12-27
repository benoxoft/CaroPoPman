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
			get { return (DateTime) _row ["DateVisite"]; }
		}

		public string Description { 
			get { return _row ["Description"].ToString(); }
		}

		public string Commentaire {
			get { return _row ["Commentaire"].ToString(); }
		}

	}
}

