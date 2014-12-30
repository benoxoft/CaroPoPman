using System;
using System.Data;

namespace CaroPoPman.Animaux
{
    public class AnimalData
    {

		private const string SELECT_ANIMAL = "SELECT * FROM Animaux WHERE IDAnimal = {0}";
		private const string SELECT_CLIENT_ID = "SELECT IDClient FROM Animaux WHERE IDAnimal = {0}";

		private const string INSERT_ANIMAL = 
			"INSERT INTO Animaux (Nom, PrixBainSeul, PrixToilettage, Notes, IDClient, IDRace)" +
			"VALUES (@Nom, @PrixBainSeul, @PrixToilettage, @Notes, @IDClient, @IDRace)";

		private const string UPDATE_ANIMAL = 
			"UPDATE Animaux SET" +
			"\tNom = @Nom, " +
			"\tPrixBainSeul = @PrixBainSeul, " +
			"\tPrixToilettage = @PrixToilettage, " +
			"\tNotes = @Notes" +
			"WHERE IDAnimal = @IDAnimal AND ";

		private const string DELETE_ANIMAL = "DELETE FROM Animaux WHERE IDAnimal = {0}";

		private static readonly AnimalData _instance = new AnimalData();

        private AnimalData() {}

		public long ObtenirClientID(long idAnimal) {
			string sql = string.Format(SELECT_CLIENT_ID, idAnimal.ToString());
			long idClient = (long)DB.Instance.ExecuteScalar(sql);
			return idClient;
		}

		public DataTable ObtenirAnimal(long animalID) {
			string sql = string.Format(SELECT_ANIMAL, animalID.ToString());
			var table = DB.Instance.GetDataTable(sql);
			if (table.Rows.Count == 0) {
				table.Rows.Add(CreateDefaultRow(table));
			}
			return table;
		}

		private DataRow CreateDefaultRow(DataTable table) {
			var row = table.NewRow();
			row["Nom"] = "";
			row["PrixBainSeul"] = 0;
			row["PrixToilettage"] = 0;
			row["Notes"] = "";
			row["IDClient"] = -1;
			row["IDRace"] = -1;
			return row;
		}

		public void UpdateAnimal(DataTable table) {
			var adapter = CreateDataAdapter();
			adapter.InsertCommand.Connection.Open();

			adapter.Update(table.DataSet);

			if (table.Rows[0]["IDAnimal"] == DBNull.Value) {
				var lastrowid = DB.Instance.GetLastInsertedID(adapter.InsertCommand.Connection);
				table.Rows[0]["IDAnimal"] = lastrowid;
			}
			adapter.InsertCommand.Connection.Close();
			adapter.InsertCommand.Connection.Dispose();
		}

		public void DeleteAnimal(DataTable table) {
			var idAnimal = table.Rows[0]["IDAnimal"];
			if (idAnimal != DBNull.Value) {
				string sql = string.Format(DELETE_ANIMAL, idAnimal.ToString());
				DB.Instance.ExecuteNonQuery(sql);
			}
			table.Rows[0].Delete();
			table.Rows.Add(CreateDefaultRow(table));
			table.AcceptChanges();
		}

		#region ADO.net objects

		private IDbDataAdapter CreateDataAdapter() {
			var adapter = DB.Instance.CreateDataAdapter();
			adapter.InsertCommand = CreateInsertCommand();
			adapter.UpdateCommand = CreateUpdateCommand();
			return adapter;
		}

		private IDbCommand CreateInsertCommand() {
			var cmd = DB.Instance.CreateCommand(INSERT_ANIMAL);
			cmd.Parameters.Add(CreateNomParam(cmd));
			cmd.Parameters.Add(CreatePrixBainSeul(cmd));
			cmd.Parameters.Add(CreatePrixToilettage(cmd));
			cmd.Parameters.Add(CreateNotesParam(cmd));
			cmd.Parameters.Add(CreateIDClientParam(cmd));
			cmd.Parameters.Add(CreateIDRaceParam(cmd));
			return cmd;
		}

		private IDbCommand CreateUpdateCommand() {
			var cmd = DB.Instance.CreateCommand(UPDATE_ANIMAL);
			cmd.Parameters.Add(CreateIDAnimalParam(cmd));
			cmd.Parameters.Add(CreateNomParam(cmd));
			cmd.Parameters.Add(CreatePrixBainSeul(cmd));
			cmd.Parameters.Add(CreatePrixToilettage(cmd));
			cmd.Parameters.Add(CreateNotesParam(cmd));
			//cmd.Parameters.Add(CreateIDClientParam(cmd));
			//cmd.Parameters.Add(CreateIDRaceParam(cmd));
			return cmd;
		}

		private IDbDataParameter CreateIDAnimalParam(IDbCommand cmd) {
			var param = cmd.CreateParameter();
			param.DbType = DbType.Int64;
			param.SourceColumn = "IDAnimal";
			param.ParameterName = "@IDAnimal";
			return param;
		}

		private IDbDataParameter CreateNomParam(IDbCommand cmd) {
			var param = cmd.CreateParameter();
			param.DbType = DbType.String;
			param.SourceColumn = "Nom";
			param.ParameterName = "@Nom";
			return param;
		}

		private IDataParameter CreatePrixBainSeul(IDbCommand cmd) {
			var param = cmd.CreateParameter();
			param.DbType = DbType.Decimal;
			param.SourceColumn = "PrixBainSeul";
			param.ParameterName = "@PrixBainSeul";
			return param;
		}

		private IDataParameter CreatePrixToilettage(IDbCommand cmd) {
			var param = cmd.CreateParameter();
			param.DbType = DbType.Decimal;
			param.SourceColumn = "PrixToilettage";
			param.ParameterName = "@PrixToilettage";
			return param;
		}

		private IDataParameter CreateNotesParam(IDbCommand cmd) {
			var param = cmd.CreateParameter();
			param.DbType = DbType.String;
			param.SourceColumn = "Notes";
			param.ParameterName = "@Notes";
			return param;
		}

		private IDataParameter CreateIDClientParam(IDbCommand cmd) {
			var param = cmd.CreateParameter();
			param.DbType = DbType.Int64;
			param.SourceColumn = "IDClient";
			param.ParameterName = "@IDClient";
			return param;
		}

		private IDataParameter CreateIDRaceParam(IDbCommand cmd) {
			var param = cmd.CreateParameter();
			param.DbType = DbType.Int64;
			param.SourceColumn = "IDRace";
			param.ParameterName = "@IDRace";
			return param;
		}

		#endregion

		public static AnimalData Instance {
			get { return _instance; }
		}
    }
}

