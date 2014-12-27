using System;
using System.Data;
using Mono.Data.Sqlite;

namespace CaroPoPman
{
	public class DB
	{
		private const string CONNECTION_STRING = "Data Source=CaroPoPtest.sqlite;Version=3;";

		private static DB _db = new DB ();

		private SqliteConnection _conn = new SqliteConnection(CONNECTION_STRING);

		private DB () {}

		public SqliteConnection CreateConnection() {
			return _conn;
		}

		public IDbCommand CreateCommand(IDbConnection conn, string sql) {
			var cmd = new SqliteCommand ((SqliteConnection)conn);
			cmd.CommandType = CommandType.Text;
			cmd.CommandText = sql;
			return cmd;
		}

		public SqliteDataAdapter CreateDataAdapter(IDbCommand cmd) {
			return new SqliteDataAdapter((SqliteCommand)cmd);
		}

		public DataTable GetDataTable(string sql) {
			using (IDbConnection conn = CreateConnection()) {
				var cmd = CreateCommand (conn, sql);
				var table = new DataTable ();
				var adapter = CreateDataAdapter (cmd);
				adapter.Fill (table);
				return table;
			}
		}

		public long ExecuteNonQuery(string sql) {
			using (IDbConnection conn = CreateConnection()) {
				conn.Open();
				var cmd = CreateCommand(conn, sql);
				cmd.ExecuteNonQuery();
				return GetLastInsertedID(conn);
			}
		}

		public object ExecuteScalar(string sql) {
			using (IDbConnection conn = CreateConnection()) {
				var cmd = CreateCommand(conn, sql);
				conn.Open();
				var data = cmd.ExecuteScalar();
				return data;
			}
		}

		public long GetLastInsertedID(IDbConnection conn) {
			string sql = "SELECT last_insert_rowid()";
			var cmd = CreateCommand(conn, sql);
			return (long)cmd.ExecuteScalar();
		}

		public static DB Instance {
			get { return _db; }
		}
	}
}

