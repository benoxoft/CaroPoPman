using System;
using System.Data;
using NUnit.Framework;

using CaroPoPman.Animaux;
using CaroPoPman.Animaux.Particularites;

namespace CaroPoPman.Tests
{
	[TestFixture()]
    public class TestParticulariteData
    {
        public TestParticulariteData()
        {
        }

		[Test()]
		public void TestCRUD() {
			var b = ParticulariteData.Instance.ObtenirBaseParticularites();
			var row = b.NewRow();
			row["Description"] = "Chien laid";
			row["Visible"] = true;
			b.Rows.Add(row);
			row = b.NewRow();
			row["Description"] = "Chien puant";
			row["Visible"] = true;
			b.Rows.Add(row);
			ParticulariteData.Instance.UpdateBaseParticularites(b);
			Assert.AreNotEqual(b.Rows[0]["IDParticularite"], DBNull.Value);
			Assert.AreNotEqual(b.Rows[1]["IDParticularite"], DBNull.Value);

			var x = ParticulariteData.Instance.ObtenirParticularites(10);
			Assert.AreEqual(x.Rows.Count, 2);
			Assert.IsFalse((bool)x.Rows[0]["Enabled"]);
			Assert.IsFalse((bool)x.Rows[1]["Enabled"]);
			x.Rows[0]["Enabled"] = true;
			ParticulariteData.Instance.UpdateParticularites(10, x);

			x = ParticulariteData.Instance.ObtenirParticularites(10);
			Assert.AreEqual(x.Rows.Count, 2);
			Assert.IsTrue((bool)x.Rows[0]["Enabled"]);
			Assert.IsFalse((bool)x.Rows[1]["Enabled"]);

			x.Rows[0]["Enabled"] = false;
			x.Rows[1]["Enabled"] = false;
			ParticulariteData.Instance.UpdateParticularites(10, x);
			x = ParticulariteData.Instance.ObtenirParticularites(10);

			b = ParticulariteData.Instance.ObtenirBaseParticularites();
			b.Rows[1].Delete();
			b.Rows[0].Delete();
			ParticulariteData.Instance.UpdateBaseParticularites(b);

			b = ParticulariteData.Instance.ObtenirBaseParticularites();
			Assert.AreEqual(b.Rows.Count, 0);

		}
    }
}

