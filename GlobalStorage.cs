using System;

namespace SASHotkeys
{
	internal class GlobalStorage
	{
		static internal GlobalStorage Instance {
			get {
				if (instance == null) {
					instance = new GlobalStorage ();
				}
				return instance;
			}
		}

		static GlobalStorage instance;

		internal KeyState holdPropagade;
	}
}

