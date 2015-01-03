using System;
using System.Data;

namespace CaroPoPman.Animaux.Races
{
    public class RaceData
    {

		#region SQL queries 

		private const string SELECT_RACES = "SELECT * FROM Race";
		private const string INSERT_RACES = "INSERT INTO Race (Description) VALUES (@Description)";
		private const string UPDATE_RACES = "UPDATE Race SET Description = @Description WHERE IDRace = @IDRace";
		private const string DELETE_RACES = "DELETE FROM Race WHERE IDRace = @IDRace";

		#endregion

		private static readonly RaceData _instance = new RaceData();

		private RaceData() {}

		public DataTable ObtenirRaces() {
			var sql = SELECT_RACES;
			var table = DB.Instance.GetDataTable(sql);
			return table;
		}

		public void UpdateRaces(DataTable table) {
			var adapter = CreateDataAdapter();
			adapter.DeleteCommand.Connection.Open();
			adapter.InsertCommand.Connection.Open();
			adapter.UpdateCommand.Connection.Open();

			try {
				adapter.Update(table.DataSet);
			} finally {
				adapter.DeleteCommand.Connection.Close();
				adapter.InsertCommand.Connection.Close();
				adapter.UpdateCommand.Connection.Close();
				adapter.DeleteCommand.Connection.Dispose();
				adapter.InsertCommand.Connection.Dispose();
				adapter.UpdateCommand.Connection.Dispose();
			}

			var indexes = ObtenirRaces();
			foreach (DataRow row in table.Rows) {
				if (row["IDRace"] != DBNull.Value) {
					continue;
				}
				foreach (DataRow baserow in indexes.Rows) {
					if (row["Description"].ToString() == baserow["Description"].ToString()) {
						row["IDRace"] = baserow["IDRace"];
					}
				}
				table.AcceptChanges();
			}

		}

		#region ADO.net objects

		public IDbDataAdapter CreateDataAdapter() {
			var adapter = DB.Instance.CreateDataAdapter();
			adapter.DeleteCommand = CreateDeleteCommand();
			adapter.InsertCommand = CreateInsertCommand();
			adapter.UpdateCommand = CreateUpdateCommand();
			return adapter;
		}

		public IDbCommand CreateInsertCommand() {
			var cmd = DB.Instance.CreateCommand(INSERT_RACES);
			cmd.Parameters.Add(CreateDescriptionParam(cmd));
			return cmd;
		}

		public IDbCommand CreateUpdateCommand() {
			var cmd = DB.Instance.CreateCommand(UPDATE_RACES);
			cmd.Parameters.Add(CreateIDRaceParam(cmd));
			cmd.Parameters.Add(CreateDescriptionParam(cmd));
			return cmd;
		}

		public IDbCommand CreateDeleteCommand() {
			var cmd = DB.Instance.CreateCommand(DELETE_RACES);
			cmd.Parameters.Add(CreateIDRaceParam(cmd));
			return cmd;
		}

		public IDbDataParameter CreateIDRaceParam(IDbCommand cmd) {
			var param = cmd.CreateParameter();
			param.DbType = DbType.Int64;
			param.SourceColumn = "IDRace";
			param.ParameterName = "@IDRace";
			return param;
		}

		public IDbDataParameter CreateDescriptionParam(IDbCommand cmd) {
			var param = cmd.CreateParameter();
			param.DbType = DbType.String;
			param.SourceColumn = "Description";
			param.ParameterName = "@Description";
			return param;
		}

		#endregion

		public static RaceData Instance {
			get { return _instance; }
		}
    }
}

