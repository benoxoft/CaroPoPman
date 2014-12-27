using CaroPoPman;
using System;
using System.Data;

namespace CaroPoPman.Animaux.Clients {

	public class ClientData {

		private const string SELECT_CLIENT = "SELECT * FROM Client WHERE IDClient = {0}";

		private static readonly ClientData _instance = new ClientData();

		private ClientData () {

		}

		public DataTable ObtenirClient(int idClient) {
			string sql = string.Format (SELECT_CLIENT, idClient.ToString ());
			DataTable table = DB.Instance.GetDataTable (sql);
			return table;
		}

		public static ClientData Instance {
			get { return _instance;	}
		}
	}
}

