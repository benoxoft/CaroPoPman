using System;
using System.Data;

namespace CaroPoPman.Animaux
{
    public class AnimalData
    {

		private const string SELECT_ANIMAL = "SELECT * FROM Animaux WHERE IDAnimal = {0}";
		private const string SELECT_CLIENT_ID = "SELECT IDClient FROM Animaux WHERE IDAnimal = {0}";

		private static readonly AnimalData _instance = new AnimalData();

        private AnimalData() {}

		public int ObtenirClientID(int animalID) {
			return 0;
		}

		public DataTable ObtenirAnimal(int animalID) {
			string sql = string.Format(SELECT_ANIMAL, animalID.ToString());
			var table = DB.Instance.GetDataTable(sql);
			return table;
		}

		public static AnimalData Instance {
			get { return _instance; }
		}
    }
}

