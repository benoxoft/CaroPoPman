using System;
using System.Data;
using NUnit.Framework;

using CaroPoPman.Animaux.Clients;

namespace CaroPoPman.Tests
{
	[TestFixture()]
    public class TestClientData
    {
        public TestClientData()
        {
        }

		[Test()]
		public void TestCRUDClient() {
			Console.WriteLine("TestCreateClient");
			var table = ClientData.Instance.ObtenirClient(-1);
			Assert.IsTrue(table.Rows.Count == 1);
			var row = table.Rows[0];
			row["Nom"] = "Pawq";
			row["Prenom"] = "Benn";
			row["TelephoneMaison"] = "555-555-5555";
			row["TelephoneCellulaire"] = "444-444-4444";
			row["AutreTelephone"] = "333-333-3333";
			ClientData.Instance.UpdateClientTable(table);
			Console.WriteLine(row["IDClient"]);

			Console.WriteLine("Client created!");
			long idclient = (long)row["IDClient"];
			table = ClientData.Instance.ObtenirClient(idclient);
			row = table.Rows[0];
			Console.WriteLine(row["Nom"]);
			Console.WriteLine(row["Prenom"]);
			Console.WriteLine(row["TelephoneMaison"]);
			Console.WriteLine(row["TelephoneCellulaire"]);
			Console.WriteLine(row["AutreTelephone"]);

			Assert.AreEqual(row["Nom"], "Pawq");
			Assert.AreEqual(row["Prenom"], "Benn");
			Assert.AreEqual(row["TelephoneMaison"], "555-555-5555");
			Assert.AreEqual(row["TelephoneCellulaire"], "444-444-4444");
			Assert.AreEqual(row["AutreTelephone"], "333-333-3333");

			row["Nom"] = "Monsieur";
			row["Prenom"] = "Le";
			row["TelephoneMaison"] = "666-666-6666";
			row["TelephoneCellulaire"] = "777-777-7777";
			row["AutreTelephone"] = "888-888-8888";
			ClientData.Instance.UpdateClientTable(table);

			Console.WriteLine("Modifying client");
			table = ClientData.Instance.ObtenirClient(idclient);
			row = table.Rows[0];
			Assert.AreEqual(row["Nom"], "Monsieur");
			Assert.AreEqual(row["Prenom"], "Le");
			Assert.AreEqual(row["TelephoneMaison"], "666-666-6666");
			Assert.AreEqual(row["TelephoneCellulaire"], "777-777-7777");
			Assert.AreEqual(row["AutreTelephone"], "888-888-8888");

			ClientData.Instance.DeleteClient(table);
			table = ClientData.Instance.ObtenirClient(idclient);
			row = table.Rows[0];
			Assert.AreEqual(row["Nom"], "");
			Assert.AreEqual(row["Prenom"], "");
			Assert.AreEqual(row["TelephoneMaison"], "");
			Assert.AreEqual(row["TelephoneCellulaire"], "");
			Assert.AreEqual(row["AutreTelephone"], "");
		}

		[Test()]
		public void TestManyOperations() {
			Console.WriteLine("Create client 1");
			var x1 = ClientData.Instance.ObtenirClient(-1);
			x1.Rows[0]["Nom"] = "Test1";
			ClientData.Instance.UpdateClientTable(x1);

			Console.WriteLine("Create client 2");
			var x2 = ClientData.Instance.ObtenirClient(-1);
			x2.Rows[0]["Nom"] = "Test2";
			ClientData.Instance.UpdateClientTable(x2);

			Console.WriteLine("Create client 3");
			var x3 = ClientData.Instance.ObtenirClient(-1);
			x3.Rows[0]["Nom"] = "Test3";
			ClientData.Instance.UpdateClientTable(x3);

			Console.WriteLine("Update client 1");
			x1.Rows[0]["Prenom"] = "Update1";
			ClientData.Instance.UpdateClientTable(x1);
			Console.WriteLine("Update client 2");
			x2.Rows[0]["Prenom"] = "Update2";
			ClientData.Instance.UpdateClientTable(x2);
			Console.WriteLine("Update client 2");
			x3.Rows[0]["Prenom"] = "Update3";
			ClientData.Instance.UpdateClientTable(x3);

			Console.WriteLine("Delete client 1");
			ClientData.Instance.DeleteClient(x1);
			Console.WriteLine("Delete client 2");
			ClientData.Instance.DeleteClient(x2);
			Console.WriteLine("Delete client 3");
			ClientData.Instance.DeleteClient(x3);
		}


		public void TestLotsOfOperations() {
			var x1 = ClientData.Instance.ObtenirClient(-1);
			x1.Rows[0]["Nom"] = "Test1";
			ClientData.Instance.UpdateClientTable(x1);

			for (int i = 0; i < 30000; i++) {
				x1.Rows[0]["Prenom"] = "Update1" + i.ToString();
				ClientData.Instance.UpdateClientTable(x1);
				//ClientData.Instance.DeleteClient(x1);
				//Console.WriteLine(i);
			}
		}
    }
}

