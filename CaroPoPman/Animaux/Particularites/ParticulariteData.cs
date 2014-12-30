using System;
using System.Data;

namespace CaroPoPman.Animaux.Particularites
{
    public class ParticulariteData
    {
		private const string SELECT_BASE_DATA = "SELECT * FROM Particularites";

		private const string SELECT_DATA = "SELECT * FROM ParticulariteAnimal WHERE IDAnimal = {0}";
		private const string INSERT_NEW_DATA = "INSERT INTO ParticulariteAnimal (IDAnimal, IDParticularite) VALUES (@IDAnimal, @IDParticularite)";
		private const string DELETE_DATA = "DELETE FROM ParticulariteAnimal WHERE IDAnimal = {0}";

		private static readonly ParticulariteData _instance = new ParticulariteData();

		private readonly DataTable _model;

        private ParticulariteData() {
			string sql = string.Format(SELECT_DATA, -1);
			_model = DB.Instance.GetDataTable(sql);
		}

		public DataTable ObtenirParticularites(int idAnimal) {
			var tableBase = DB.Instance.GetDataTable(SELECT_BASE_DATA);
			tableBase.Columns.Add("Enabled", typeof(bool));

			var table = DB.Instance.GetDataTable(string.Format(SELECT_DATA, idAnimal.ToString()));

			foreach (DataRow baserow in tableBase.Rows) {
				baserow["Enabled"] = false;
				foreach (DataRow row in table.Rows) {
					if (row["IDParticularite"] == baserow["IDParticularite"]) {
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

			var newTable = _model.Copy();
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

		private IDbCommand CreateInsertCommand() {
			var cmd = DB.Instance.CreateCommand(INSERT_NEW_DATA);
			cmd.Parameters.Add(CreateIDAnimalParam(cmd));
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
			var idParticularite = cmd.CreateParameter();
			idParticularite.DbType = DbType.Int64;
			idParticularite.ParameterName = "@IDParticularite";
			idParticularite.SourceColumn = "IDParticularite";
			return idParticularite;
		}

		#endregion

		public static ParticulariteData Instance {
			get { return _instance;	}
		}
    }
}

