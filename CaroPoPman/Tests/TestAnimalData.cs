using System;
using System.Data;
using NUnit.Framework;

using CaroPoPman.Animaux;

namespace CaroPoPman.Tests
{
	[TestFixture()]
    public class TestAnimalData
    {
        public TestAnimalData()
        {
        }

		[Test()]
		public void TestCRUD() {
			var x = AnimalData.Instance.ObtenirAnimal(-1);
			var row = x.Rows[0];
			row["Nom"] = "Belle";
			row["PrixBainSeul"] = 10.24;
			row["PrixToilettage"] = 99.99;
			row["Notes"] = "Chien laid";
			row["IDClient"] = 10;
			row["IDRace"] = 20;
			AnimalData.Instance.UpdateAnimal(x);

			var idAnimal = (long)row["IDAnimal"];
			x = AnimalData.Instance.ObtenirAnimal(idAnimal);
			Assert.AreEqual(row["Nom"], "Belle");
			Assert.AreEqual(row["PrixBainSeul"], 10.24);
			Assert.AreEqual(row["PrixToilettage"], 99.99);
			Assert.AreEqual(row["Notes"], "Chien laid");
			Assert.AreEqual(row["IDClient"], 10);
			Assert.AreEqual(row["IDRace"], 20);
			Assert.AreEqual(row["IDAnimal"], idAnimal);

			AnimalData.Instance.DeleteAnimal(x);

			x = AnimalData.Instance.ObtenirAnimal(idAnimal);
			Assert.AreEqual(row["Nom"], "");
		}

    }
}

