using System;
using System.Data;

namespace CaroPoPman.Animaux
{
	public class Client
	{

		private readonly Datatable _table;

		public Client (DataTable table)

		{
			_table = table;

		}
			
		public string Nom { 
			get {
				return _table [0] ["Nom"];
			}
			set {
				_table [0] ["Nom"] = value;
			}
		}

		public string Prenom { 
			get {
				return _table [0] ["Prenom"];
			}
			set {
				_table [0] ["Prenom"];
			}
		}

		public string TelephoneMaison{ 
			get {
				return _table [0] ["TelephoneMaison"];
			}
			set {
				_table [0] ["TelephoneMaison"] = value;
			}
		}

		public string TelephoneCellulaire{ 
			get {
				return _table [0] ["TelephoneCellulaire"];
			}
			set {
				_table [0] ["TelephoneCellulaire"] = value;
			}
		}

		public string AutreTelephone{ 
			get {
				return _table [0] ["AutreTelephone"];
			}
			set {
				_table [0] ["AutreTelephone"] = value;
			}
		}

	}
}

