using System;
using CaroPoPman.Animaux.Clients;

namespace CaroPoPman.Animaux
{
	public class GestionAnimaux
	{

		private readonly GestionAnimaux _instance = new GestionAnimaux();

		public GestionAnimaux ()
		{

		}

		public Animal CreerAnimal() {
			var clientTable = ClientData.Instance.ObtenirClient(-1);
			var client = new Client(clientTable);
			//var animal = new Animal(
			return null;
		}

		public Animal ChargerAnimal() {
			return null;
		}

		public void SauvegarderAnimal() {

		}

		public void SupprimerAnimal() {

		}

		public GestionAnimaux Instance { 
			get {
				return _instance;
			}
		}
			
	}
}

