using System;

namespace CaroPoPman.Animaux.Particularites
{
    public class ParticulariteData
    {
		private static readonly ParticulariteData _instance = new ParticulariteData();

        private ParticulariteData() {}

		public static ParticulariteData Instance {
			get { return _instance;	}
		}
    }
}

