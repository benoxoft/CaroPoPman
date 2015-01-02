using System;
using System.Data;

namespace CaroPoPman.Animaux.Particularites
{
    public class ParticulariteData
    {

		#region SQL queries

		private const string SELECT_BASE_DATA = "SELECT * FROM Particularites";
		private const string INSERT_BASE_DATA = "INSERT INTO Particularites (Description, Visible) VALUES (@Description, @Visible)";
		private const string UPDATE_BASE_DATA = "UPDATE Particularites SET Description = @Description, Visible = @Visible WHERE IDParticularite = @IDParticularite";
		private const string DELETE_BASE_DATA = "DELETE FROM Particularites WHERE IDParticularite = @IDParticularite";

		private const string SELECT_DATA = "SELECT * FROM ParticulariteAnimal WHERE IDAnimal = {0}";
		private const string INSERT_NEW_DATA = "INSERT INTO ParticulariteAnimal (IDAnimal, IDParticularite) VALUES (@IDAnimal, @IDParticularite)";
		private const string DELETE_DATA = "DELETE FROM ParticulariteAnimal WHERE IDAnimal = {0}";

		private static readonly ParticulariteData _instance = new ParticulariteData();

		#endregion

		private readonly DataTable _model;

        private ParticulariteData() {
			string sql = string.Format(SELECT_DATA, -1);
			_model = DB.Instance.GetDataTable(sql);
		}

		public DataTable ObtenirBaseParticularites() {
			var table = DB.Instance.GetDataTable(SELECT_BASE_DATA);
			return table;
		}

		public void UpdateBaseParticularites(DataTable table) {
			var adapter = CreateBaseDataAdapter();

			adapter.InsertCommand.Connection.Open();
			adapter.UpdateCommand.Connection.Open();
			adapter.DeleteCommand.Connection.Open();

			try {
				adapter.Update(table.DataSet);
			} finally {
				adapter.InsertCommand.Connection.Close();
				adapter.UpdateCommand.Connection.Close();
				adapter.DeleteCommand.Connection.Close();
				adapter.InsertCommand.Connection.Dispose();
				adapter.UpdateCommand.Connection.Dispose();
				adapter.DeleteCommand.Connection.Dispose();
			}

			var indexes = ObtenirBaseParticularites();
			foreach (DataRow row in table.Rows) {
				if (row["IDParticularite"] != DBNull.Value) {
					continue;
				}

				foreach (DataRow baserow in indexes.Rows) {
					if (baserow["Description"].ToString() == row["Description"].ToString()) {
						row["IDParticularite"] = baserow["IDParticularite"];
						break;
					}
				}
			}
			table.AcceptChanges();
		}
			
		public DataTable ObtenirParticularites(int idAnimal) {
			var tableBase = DB.Instance.GetDataTable(SELECT_BASE_DATA);
			tableBase.Columns.Add("Enabled", typeof(bool));

			var table = DB.Instance.GetDataTable(string.Format(SELECT_DATA, idAnimal.ToString()));

			foreach (DataRow baserow in tableBase.Rows) {
				baserow["Enabled"] = false;
				foreach (DataRow row in table.Rows) {
					if (((long)row["IDParticularite"]) == ((long)baserow["IDParticularite"])) {
						baserow["Enabled"] = true;
						break;
					}
				}
			}

			tableBase.AcceptChanges();
			return tableBase;
		}

		public void UpdateParticularites(int idAnimal, DataTable table) {
			string sql = string.Format(DELETE_DATA, idAnimal.ToString());
			DB.Instance.ExecuteNonQuery(sql);

			var newDS = _model.DataSet.Copy();
			var newTable = newDS.Tables[0];
			foreach (DataRow row in table.Rows) {
				if ((bool)row["Enabled"]) {
					var newrow = newTable.NewRow();
					newrow["IDAnimal"] = idAnimal;
					newrow["IDParticularite"] = row["IDParticularite"];
					newTable.Rows.Add(newrow);
				}
			}
			var adapter = CreateDataAdapter();
			adapter.Update(newTable.DataSet);
		}

		#region ADO.net objects

		private IDbDataAdapter CreateDataAdapter() {
			var adapter = DB.Instance.CreateDataAdapter();
			adapter.InsertCommand = CreateInsertCommand();
			return adapter;
		}

		private IDbDataAdapter CreateBaseDataAdapter() {
			var adapter = DB.Instance.CreateDataAdapter();
			adapter.InsertCommand = CreateBaseInsertCommand();
			adapter.UpdateCommand = CreateBaseUpdateCommand();
			adapter.DeleteCommand = CreateBaseDeleteCommand();
			return adapter;
		}

		private IDbCommand CreateInsertCommand() {
			var cmd = DB.Instance.CreateCommand(INSERT_NEW_DATA);
			cmd.Parameters.Add(CreateIDAnimalParam(cmd));
			cmd.Parameters.Add(CreateIDParticularite(cmd));
			return cmd;
		}
			
		private IDbCommand CreateBaseInsertCommand() {
			var cmd = DB.Instance.CreateCommand(INSERT_BASE_DATA);
			cmd.Parameters.Add(CreateDescriptionParam(cmd));
			cmd.Parameters.Add(CreateVisibleParam(cmd));
			return cmd;
		}

		private IDbCommand CreateBaseUpdateCommand() {
			var cmd = DB.Instance.CreateCommand(UPDATE_BASE_DATA);
			cmd.Parameters.Add(CreateIDParticularite(cmd));
			cmd.Parameters.Add(CreateDescriptionParam(cmd));
			cmd.Parameters.Add(CreateVisibleParam(cmd));
			return cmd;
		}

		private IDbCommand CreateBaseDeleteCommand() {
			var cmd = DB.Instance.CreateCommand(DELETE_BASE_DATA);
			cmd.Parameters.Add(CreateIDParticularite(cmd));
			return cmd;
		}

		private IDbDataParameter CreateIDAnimalParam(IDbCommand cmd) {
			var idAnimalParam = cmd.CreateParameter();
			idAnimalParam.DbType = DbType.Int64;
			idAnimalParam.ParameterName = "@IDAnimal";
			idAnimalParam.SourceColumn = "IDAnimal";
			return idAnimalParam;
		}

		private IDbDataParameter CreateIDParticularite(IDbCommand cmd) {
			var paramID = cmd.CreateParameter();
			paramID.DbType = DbType.Int64;
			paramID.ParameterName = "@IDParticularite";
			paramID.SourceColumn = "IDParticularite";
			return paramID;
		}

		private IDbDataParameter CreateDescriptionParam(IDbCommand cmd) {
			var paramDesc = cmd.CreateParameter();
			paramDesc.DbType = DbType.String;
			paramDesc.ParameterName = "@Description";
			paramDesc.SourceColumn = "Description";
			return paramDesc;
		}

		private IDbDataParameter CreateVisibleParam(IDbCommand cmd) {
			var visibleParam = cmd.CreateParameter();
			visibleParam.DbType = DbType.Boolean;
			visibleParam.ParameterName = "@Visible";
			visibleParam.SourceColumn = "Visible";
			return visibleParam;
		}

		#endregion

		public static ParticulariteData Instance {
			get { return _instance;	}
		}
    }
}

