using System;

namespace CaroPoPman.Animaux
{
	public class Particularite
	{
		private readonly string _description;
		private bool _enabled;
		private bool _visible;

		public Particularite (string description, bool enabled)
		{
			_description = description;
			_enabled = enabled;
		}

		public string Description {
			get { return _description; }
		}

		public bool Enabled {
			get { return _enabled; }
			set { _enabled = value; }
		}

		public bool Visible {
			get { return _visible; }
			set { _visible = value; }
		}
	}
}

