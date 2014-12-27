using System;

namespace CaroPoPman.Animaux.Visites
{
    public class VisiteData
    {
		private static readonly VisiteData _instance = new VisiteData();

        public VisiteData() {}

		public static VisiteData Instance {
			get { return _instance; }
		}
    }
}

