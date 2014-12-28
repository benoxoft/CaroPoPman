using CaroPoPman;
using System;
using System.Data;

namespace CaroPoPman.Animaux.Clients {

	public class ClientData {

		private const string SELECT_CLIENT = "SELECT * FROM Clients WHERE IDClient = {0}";

		private const string UPDATE_CLIENT = 
			"UPDATE Clients SET \n" + 
			"\tNom = @Nom, \n" + 
			"\tPrenom = @Prenom, \n" + 
			"\tTelephoneMaison = @TelephoneMaison, \n" + 
			"\tTelephoneCellulaire = @TelephoneCellulaire, \n" + 
			"\tAutreTelephone = @AutreTelephone \n" + 
			"WHERE IDClient = @IDClient";

		private const string INSERT_CLIENT = 
			"INSERT INTO Clients (Nom, Prenom, TelephoneMaison, TelephoneCellulaire, AutreTelephone)\n" + 
			"VALUES (@Nom, @Prenom, @TelephoneMaison, @TelephoneCellulaire, @AutreTelephone)";

		private const string DELETE_CLIENT = "DELETE FROM Clients WHERE IDClient = {0}";

		private static readonly ClientData _instance = new ClientData();

		private readonly IDbDataAdapter _adapter;
		private IDbConnection _insertConn;

		private ClientData () {
			_adapter = CreateDataAdapter();
		}

		public DataTable ObtenirClient(long idClient) {
			string sql = string.Format (SELECT_CLIENT, idClient.ToString ());
			DataTable table = DB.Instance.GetDataTable (sql);
			if (table.Rows.Count == 0) {
				table.Rows.Add(CreateDefaultRow(table));
			}
			return table;
		}

		private DataRow CreateDefaultRow(DataTable table) {
			var row = table.NewRow();
			row["Nom"] = "";
			row["Prenom"] = "";
			row["TelephoneMaison"] = "";
			row["TelephoneCellulaire"] = "";
			row["AutreTelephone"] = "";
			return row;
		}

		public void UpdateClientTable(DataTable table) {
			if (table.Rows[0]["IDClient"] == DBNull.Value) {
				_insertConn.Open();
			}

			_adapter.Update(table.DataSet);

			if (table.Rows[0]["IDClient"] == DBNull.Value) {
				var lastrowid = DB.Instance.GetLastInsertedID(_insertConn);
				table.Rows[0]["IDClient"] = lastrowid;
			}
		}

		public void DeleteClient(DataTable table) {
			string sql = string.Format(DELETE_CLIENT, table.Rows[0]["IDClient"].ToString());
			DB.Instance.ExecuteNonQuery(sql);
		}

		private IDbDataAdapter CreateDataAdapter() {
			var a = DB.Instance.CreateDataAdapter();
			a.UpdateCommand = CreateUpdateCommand();
			a.InsertCommand = CreateInsertCommand();
			return a;
		}

		private IDbCommand CreateInsertCommand() {
			var cmd = DB.Instance.CreateCommand(INSERT_CLIENT);
			_insertConn = cmd.Connection;
			cmd.Parameters.Add(CreateNomParam(cmd));
			cmd.Parameters.Add(CreatePrenomParam(cmd));
			cmd.Parameters.Add(CreateTelephoneMaisonParam(cmd));
			cmd.Parameters.Add(CreateTelephoneCellulaireParam(cmd));
			cmd.Parameters.Add(CreateAutreTelephoneParam(cmd));
			return cmd;
		}

		private IDbCommand CreateUpdateCommand() {
			var cmd = DB.Instance.CreateCommand(UPDATE_CLIENT);
			cmd.Parameters.Add(CreateIDClientParam(cmd));
			cmd.Parameters.Add(CreateNomParam(cmd));
			cmd.Parameters.Add(CreatePrenomParam(cmd));
			cmd.Parameters.Add(CreateTelephoneMaisonParam(cmd));
			cmd.Parameters.Add(CreateTelephoneCellulaireParam(cmd));
			cmd.Parameters.Add(CreateAutreTelephoneParam(cmd));
			return cmd;
		}

		private IDbDataParameter CreateIDClientParam(IDbCommand cmd) {
			var paramID = cmd.CreateParameter();
			paramID.DbType = DbType.Int64;
			paramID.ParameterName = "@IDClient";
			paramID.SourceColumn = "IDClient";
			return paramID;
		}

		private IDbDataParameter CreateNomParam(IDbCommand cmd) {
			var paramNom = cmd.CreateParameter();
			paramNom.DbType = DbType.String;
			paramNom.ParameterName = "@Nom";
			paramNom.SourceColumn = "Nom";
			return paramNom;
		}

		private IDbDataParameter CreatePrenomParam(IDbCommand cmd) {
			var paramPrenom = cmd.CreateParameter();
			paramPrenom.DbType = DbType.String;
			paramPrenom.ParameterName = "@Prenom";
			paramPrenom.SourceColumn = "Prenom";
			return paramPrenom;
		}

		private IDbDataParameter CreateTelephoneMaisonParam(IDbCommand cmd) {
			var paramTelephoneMaison = cmd.CreateParameter();
			paramTelephoneMaison.DbType = DbType.String;
			paramTelephoneMaison.ParameterName = "@TelephoneMaison";
			paramTelephoneMaison.SourceColumn = "TelephoneMaison";
			return paramTelephoneMaison;
		}

		private IDbDataParameter CreateTelephoneCellulaireParam(IDbCommand cmd) {
			var paramTelephoneCellulaire = cmd.CreateParameter();
			paramTelephoneCellulaire.DbType = DbType.String;
			paramTelephoneCellulaire.ParameterName = "@TelephoneCellulaire";
			paramTelephoneCellulaire.SourceColumn = "TelephoneCellulaire";
			return paramTelephoneCellulaire;
		}

		private IDbDataParameter CreateAutreTelephoneParam(IDbCommand cmd) {
			var paramAutreTelephone = cmd.CreateParameter();
			paramAutreTelephone.DbType = DbType.String;
			paramAutreTelephone.ParameterName = "@AutreTelephone";
			paramAutreTelephone.SourceColumn = "AutreTelephone";
			return paramAutreTelephone;
		}

		public static ClientData Instance {
			get { return _instance;	}
		}
	}
}

