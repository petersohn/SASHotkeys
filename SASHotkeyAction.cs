using System;

namespace SASHotkeys
{
	internal class SASHotkeyAction {
		static internal HotkeyAction CreateSASHotkeyAction(KeyBinding keyBinding, VesselAutopilot.AutopilotMode mode)
		{
			return new HotkeyAction (new SASHotkeyAction (mode).GetAction (), true, keyBinding);
		}

		internal SASHotkeyAction(VesselAutopilot.AutopilotMode mode)
		{
			this.mode = mode;
		}

		internal HotkeyAction.Action GetAction()
		{
			return Fire;
		}

		void Fire()
		{
			FlightGlobals.ActiveVessel.Autopilot.SetMode (mode);
		}

		VesselAutopilot.AutopilotMode mode;
	};
}

