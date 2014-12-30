using System;
using System.Data;

namespace CaroPoPman.Animaux.Visites
{
    public class VisiteData
    {
		private const string SELECT_VISITES = "SELECT * FROM Visites WHERE IDAnimal = {0}";
		private const string INSERT_VISITE = "INSERT INTO Visites (IDAnimal, DateVisite, Description, Commentaire) VALUES (@IDAnimal, @DateVisite, @Description, @Commentaire)";
		private const string UPDATE_VISITE = "UPDATE Visites SET DateVisite = @DateVisite, Description = @Description, Commentaire = @Commantaire WHERE IDAnimal = @IDAnimal";
		//private const string DELETE_VISITE = "DELETE FROM Visites WHERE IDAnimal = @IDAnimal";

		private static readonly VisiteData _instance = new VisiteData();

        public VisiteData() {}

		public DataTable ObtenirVisites(int idAnimal) {
			string sql = string.Format(SELECT_VISITES, idAnimal.ToString());
			var table = DB.Instance.GetDataTable(sql);
			return table;
		}

		public void UpdateVisites(DataTable table) {
			var adapter = CreateDataAdapter();
			adapter.Update(table.DataSet);
		}

		#region ADO.net objects

		private IDbDataAdapter CreateDataAdapter() {
			var adapter = DB.Instance.CreateDataAdapter();
			adapter.UpdateCommand = CreateUpdateCommand();
			adapter.InsertCommand = CreateInsertCommand();
			return adapter;
		}

		private IDbCommand CreateInsertCommand() {
			var cmd = DB.Instance.CreateCommand(UPDATE_VISITE);
			cmd.Parameters.Add(CreateIDAnimalParam(cmd));
			cmd.Parameters.Add(CreateDateVisiteParam(cmd));
			cmd.Parameters.Add(CreateDescriptionParam(cmd));
			cmd.Parameters.Add(CreateCommentaireParam(cmd));
			return cmd;
		}

		private IDbCommand CreateUpdateCommand() {
			var cmd = DB.Instance.CreateCommand(UPDATE_VISITE);
			cmd.Parameters.Add(CreateIDAnimalParam(cmd));
			cmd.Parameters.Add(CreateDateVisiteParam(cmd));
			cmd.Parameters.Add(CreateDescriptionParam(cmd));
			cmd.Parameters.Add(CreateCommentaireParam(cmd));
			return cmd;
		}

		private IDbDataParameter CreateIDAnimalParam(IDbCommand cmd) {
			var param = cmd.CreateParameter();
			param.DbType = DbType.Int64;
			param.ParameterName = "@IDAnimal";
			param.SourceColumn = "IDAnimal";
			return param;
		}

		private IDbDataParameter CreateDateVisiteParam(IDbCommand cmd) {
			var param = cmd.CreateParameter();
			param.DbType = DbType.DateTime;
			param.SourceColumn = "DateVisite";
			param.ParameterName = "@DateVisite";
			return param;
		}

		private IDbDataParameter CreateDescriptionParam(IDbCommand cmd) {
			var param = cmd.CreateParameter();
			param.DbType = DbType.String;
			param.SourceColumn = "Description";
			param.ParameterName = "@Description";
			return param;
		}

		private IDbDataParameter CreateCommentaireParam(IDbCommand cmd) {
			var param = cmd.CreateParameter();
			param.DbType = DbType.String;
			param.SourceColumn = "Commentaire";
			param.ParameterName = "@Commentaire";
			return param;
		}

		#endregion

		public static VisiteData Instance {
			get { return _instance; }
		}
    }
}

