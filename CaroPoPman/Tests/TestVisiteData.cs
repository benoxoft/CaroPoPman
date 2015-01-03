using System;
using System.Data;
using NUnit.Framework;

using CaroPoPman.Animaux.Visites;

namespace CaroPoPman.Tests
{
	[TestFixture()]
    public class TestVisiteData
    {
        public TestVisiteData()
        {
        }

		[Test()]
		public void TestCRUD() {
			var x = VisiteData.Instance.ObtenirVisites(10);
			Assert.AreEqual(x.Rows.Count, 0);
			var row = x.NewRow();
			var date1 = DateTime.Now.AddHours(-1);
			row["IDAnimal"] = 10;
			row["DateVisite"] = date1;
			row["Description"] = "Visite avec son chiend laid";
			row["Commentaire"] = "Mon Dieu qu'il est laid";
			x.Rows.Add(row);
			row = x.NewRow();
			var date2 = DateTime.Now;
			row["IDAnimal"] = 10;
			row["DateVisite"] = date2;
			row["Description"] = "Visite avec son chiend laid";
			row["Commentaire"] = "Mon Dieu qu'il est laid";
			x.Rows.Add(row);
			VisiteData.Instance.UpdateVisites(10, x);

			x = VisiteData.Instance.ObtenirVisites(10);
			Assert.AreEqual(x.Rows.Count, 2);
			row = x.Rows[0];
			Assert.AreEqual(row["DateVisite"], date1);
			Assert.AreEqual(row["Description"], "Visite avec son chiend laid");
			Assert.AreEqual(row["Commentaire"], "Mon Dieu qu'il est laid");
			row = x.Rows[1];
			Assert.AreEqual(row["DateVisite"], date2);
			Assert.AreEqual(row["Description"], "Visite avec son chiend laid");
			Assert.AreEqual(row["Commentaire"], "Mon Dieu qu'il est laid");

			x.Rows[1].Delete();
			x.Rows[0].Delete();
			VisiteData.Instance.UpdateVisites(10, x);
			Assert.AreEqual(x.Rows.Count, 0);
			x = VisiteData.Instance.ObtenirVisites(10);
			Assert.AreEqual(x.Rows.Count, 0);

		}
    }
}

