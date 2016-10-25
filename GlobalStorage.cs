using System;

namespace SASHotkeys
{
	public class GlobalStorage
	{
		static public GlobalStorage Instance {
			get {
				if (instance == null) {
					instance = new GlobalStorage ();
				}
				return instance;
			}
		}

		static GlobalStorage instance;

		GlobalStorage()
		{
			holdPropagade = SASHotkeyAction.CreateSASHotkeyAction (
					null, VesselAutopilot.AutopilotMode.Prograde);
		}

		public HotkeyAction holdPropagade;
	}
}

