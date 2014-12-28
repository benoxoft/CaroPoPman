using CaroPoPman;
using System;
using System.Data;
using NUnit.Framework;

namespace TestCaroPoPman
{
	[TestFixture()]
    public class TestDB
    {
        public TestDB()
        {
        }

		[Test()]
		public void TestExecuteNonQuery() {
			Console.WriteLine("HELLO!");
			var ret = DB.Instance.ExecuteNonQuery("INSERT INTO Clients (Nom, Prenom, TelephoneMaison, TelephoneCellulaire, AutreTelephone) VALUES ('Pawq', 'Benn', '555-555-5555', '', '')");
			DB.Instance.ExecuteNonQuery(string.Format("DELETE FROM Clients WHERE IDClient={0}", ret));
		}

		[Test()]
		public void TestGetDataTable() {
			string sql = "SELECT * FROM sqlite_master";
			var table = DB.Instance.GetDataTable(sql);
			Console.WriteLine(table.Columns[0].ColumnName);
			foreach (DataRow row in table.Rows) {
				Console.WriteLine(row[1].ToString());
			}
		}

		[Test()]
		public void TestGetTableSchema() {
			string sql = "SELECT * FROM Clients WHERE IDClient = -1";
			var table = DB.Instance.GetDataTable(sql);
			foreach (DataColumn col in table.Columns) {
				Console.WriteLine(col.ColumnName);
			}
		}
    }
}

