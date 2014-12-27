using System;
using System.Data;

namespace CaroPoPman.Animaux
{
	public class Client
	{

		private readonly DataTable _table;

		public Client (DataTable table)

		{
			_table = table;

		}
			
		public string Nom { 
			get { return _table.Rows [0] ["Nom"].ToString(); }
			set { _table.Rows [0] ["Nom"] = value; }
		}

		public string Prenom { 
			get { return _table.Rows [0] ["Prenom"].ToString();	}
			set { _table.Rows [0] ["Prenom"] = value; }
		}

		public string TelephoneMaison{ 
			get { return _table.Rows [0] ["TelephoneMaison"].ToString(); }
			set { _table.Rows [0] ["TelephoneMaison"] = value; }
		}

		public string TelephoneCellulaire{ 
			get { return _table.Rows [0] ["TelephoneCellulaire"].ToString(); }
			set { _table.Rows [0] ["TelephoneCellulaire"] = value; }
		}

		public string AutreTelephone{ 
			get { return _table.Rows [0] ["AutreTelephone"].ToString(); }
			set { _table.Rows [0] ["AutreTelephone"] = value; }
		}

	}
}

