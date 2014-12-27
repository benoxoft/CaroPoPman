using System;
using System.Data;
using System.Data.SQLite;

namespace CaroPoPman
{
	public class DB
	{
		private const string CONNECTION_STRING = "";

		private static DB _db = new DB ();

		private SQLiteConnection _conn = new SQLiteConnection(CONNECTION_STRING);

		public DB ()
		{

		}

		public IDbConnection CreateConnection() {
			return new SQLiteConnection (CONNECTION_STRING);
		}

		public IDbCommand CreateCommand(SQLiteConnection conn) {
			return new SQLiteCommand (conn);
		}

		public DataTable GetDataTable(string sql) {
			using (SQLiteConnection conn = CreateConnection()) {
				var cmd = CreateCommand (conn);
				cmd.CommandText = sql;
				var table = new DataTable ();
				var adapter = new SQLiteDataAdapter (cmd);
				adapter.Fill (table);
				return table;
			}
		}


		public DB Instance {
			get {
				return _db;
			}
		}
	}
}

