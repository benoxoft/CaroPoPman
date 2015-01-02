using System;
using System.Data;
using Mono.Data.Sqlite;

namespace CaroPoPman
{
	public class DB
	{
		private const string CONNECTION_STRING = "Data Source=CaroPoPtest.sqlite;Version=3;";

		private static DB _db = new DB ();

		//private SqliteConnection _conn = new SqliteConnection(CONNECTION_STRING);

		private DB () {}

		public SqliteConnection Connection() {
			//return _conn;
			return new SqliteConnection(CONNECTION_STRING);
		}

		public IDbCommand CreateCommand(string sql) {
			return CreateCommand(Connection(), sql);
		}

		public IDbCommand CreateCommand(IDbConnection conn, string sql) {
			var cmd = new SqliteCommand ((SqliteConnection)conn);
			cmd.CommandType = CommandType.Text;
			cmd.CommandText = sql;
			return cmd;
		}

		public IDbDataAdapter CreateDataAdapter() {
			var a = new SqliteDataAdapter();
			return a;

		}
			
		public SqliteDataAdapter CreateDataAdapter(IDbCommand cmd) {
			return new SqliteDataAdapter((SqliteCommand)cmd);
		}

		public DataTable GetDataTable(string sql) {
			using (IDbConnection conn = Connection()) {
				var cmd = CreateCommand (conn, sql);
				var ds = new DataSet();
				var adapter = CreateDataAdapter (cmd);
				adapter.Fill (ds);
				cmd.Dispose();
				return ds.Tables[0];
			}
		}

		public long ExecuteNonQuery(string sql) {
			using (IDbConnection conn = Connection()) {
				conn.Open();
				var cmd = CreateCommand(conn, sql);
				cmd.ExecuteNonQuery();
				cmd.Dispose();
				return GetLastInsertedID(conn);
			}
		}

		public object ExecuteScalar(string sql) {
			using (IDbConnection conn = Connection()) {
				var cmd = CreateCommand(conn, sql);
				conn.Open();
				var data = cmd.ExecuteScalar();
				cmd.Dispose();
				return data;
			}
		}

		public long GetLastInsertedID() {
			return GetLastInsertedID(Connection());
		}

		public long GetLastInsertedID(IDbConnection conn) {
			string sql = "SELECT last_insert_rowid()";
			var cmd = CreateCommand(conn, sql);
			var data = (long)cmd.ExecuteScalar();
			cmd.Dispose();
			return data;
		}

		public static DB Instance {
			get { return _db; }
		}
	}
}

