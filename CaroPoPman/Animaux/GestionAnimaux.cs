using System;

namespace CaroPoPman.Animaux
{
	public class GestionAnimaux
	{

		private readonly GestionAnimaux _instance = new GestionAnimaux();

		public GestionAnimaux ()
		{

		}

		public Animal CreerAnimal() {

		}

		public Animal ChargerAnimal() {

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

