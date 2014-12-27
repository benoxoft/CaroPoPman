using System;
using System.Collections.Generic
using System.Data;

namespace CaroPoPman.Animaux
{
	public class Animal
	{
		public event EventHandler AnimalUpdated;

		private readonly DataTable _table;
		private readonly Client _client;
		private readonly List<Visite> _visites;
		private readonly List<Particularite> _particulatires;

		public Animal (DataTable table, Client client, List<Particularite> particularites, List<Visite> visites)
		{
			_table = table;
			_client = client;
			_particulatires = particularites;
			_visites = visites;
		}

		public void NotifyObservers() {
			AnimalUpdated (this, EventArgs.Empty);
		}

		public string Nom { 
			get { return _table [0] ["Nom"].ToString();	}
			set { _table [0] ["Nom"] = value; }
		}

		public string Race { 
			get { return _table [0] ["Race"].ToString(); }
			set { _table [0] ["Race"] = value; }
		}

		public float PrixBainSeul { 
			get { return (float)_table [0] ["PrixBainSeul"]; }
			set { _table [0] ["PrixBainSeul"] = value; }
		}

		public float PrixToilettage { 
			get { return (float)_table [0] ["PrixToilettage"]; }
			set { _table [0] ["PrixToilettage"] = value; }
		}

		public string Notes { 
			get { return _table [0] ["Notes"].ToString(); }
			set { _table [0] ["Notes"] = value; }
		}

		public Client Client { 
			get { return _client }
		}

		public List<Particularite> Particularites {
			get { return _particularites; }
		}

		public List<Visite> Visites {
			get { return _visites; }
		}

	}
}

