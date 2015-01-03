using System;
using System.Data;
using NUnit.Framework;

using CaroPoPman.Animaux.Races;

namespace CaroPoPman.Tests
{
	[TestFixture()]
    public class TestRaceData
    {
        public TestRaceData() {}

		[Test()]
		public void TestCRUD() {
			var x = RaceData.Instance.ObtenirRaces();
			var row = x.NewRow();
			row["Description"] = "Berger Allemand";
			x.Rows.Add(row);
			row = x.NewRow();
			row["Description"] = "Golden Retriever";
			x.Rows.Add(row);
			row = x.NewRow();
			row["Description"] = "Petit chien laid";
			x.Rows.Add(row);
			RaceData.Instance.UpdateRaces(x);
			x = RaceData.Instance.ObtenirRaces();
			Assert.AreEqual(x.Rows.Count, 3);
			Assert.AreNotEqual(x.Rows[0]["IDRace"], DBNull.Value);
			Assert.AreNotEqual(x.Rows[1]["IDRace"], DBNull.Value);
			Assert.AreNotEqual(x.Rows[2]["IDRace"], DBNull.Value);
			Assert.AreEqual(x.Rows[0]["Description"], "Berger Allemand");
			Assert.AreEqual(x.Rows[1]["Description"], "Golden Retriever");
			Assert.AreEqual(x.Rows[2]["Description"], "Petit chien laid");

			x.Rows[2].Delete();
			x.Rows[1].Delete();
			x.Rows[0].Delete();
			RaceData.Instance.UpdateRaces(x);
			Assert.AreEqual(x.Rows.Count, 0);
			x = RaceData.Instance.ObtenirRaces();
			Assert.AreEqual(x.Rows.Count, 0);


		}

    }
}

