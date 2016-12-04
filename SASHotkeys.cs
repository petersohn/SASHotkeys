using System;

namespace SASHotkeys
{
	internal class SASHotkeys
	{
		internal static void InitializeHotkeyManager()
		{
			HotkeyManager.MainManager.Add("SAS Hotkeys", "Stability assist",
				SASHotkeyAction.CreateSASHotkeyAction(
					new KeyBinding(), VesselAutopilot.AutopilotMode.StabilityAssist));
			HotkeyManager.MainManager.Add("SAS Hotkeys", "Hold propagade",
				SASHotkeyAction.CreateSASHotkeyAction(
					new KeyBinding(), VesselAutopilot.AutopilotMode.Prograde));
			HotkeyManager.MainManager.Add("SAS Hotkeys", "Hold retrograde",
				SASHotkeyAction.CreateSASHotkeyAction(
					new KeyBinding(), VesselAutopilot.AutopilotMode.Retrograde));
			HotkeyManager.MainManager.Add("SAS Hotkeys", "Hold normal",
				SASHotkeyAction.CreateSASHotkeyAction(
					new KeyBinding(), VesselAutopilot.AutopilotMode.Normal));
			HotkeyManager.MainManager.Add("SAS Hotkeys", "Hold anti-normal",
				SASHotkeyAction.CreateSASHotkeyAction(
					new KeyBinding(), VesselAutopilot.AutopilotMode.Antinormal));
			HotkeyManager.MainManager.Add("SAS Hotkeys", "Hold radial in",
				SASHotkeyAction.CreateSASHotkeyAction(
					new KeyBinding(), VesselAutopilot.AutopilotMode.RadialIn));
			HotkeyManager.MainManager.Add("SAS Hotkeys", "Hold radial out",
				SASHotkeyAction.CreateSASHotkeyAction(
					new KeyBinding(), VesselAutopilot.AutopilotMode.RadialOut));
			HotkeyManager.MainManager.Add("SAS Hotkeys", "Hold target",
				SASHotkeyAction.CreateSASHotkeyAction(
					new KeyBinding(), VesselAutopilot.AutopilotMode.Target));
			HotkeyManager.MainManager.Add("SAS Hotkeys", "Hold anti-target",
				SASHotkeyAction.CreateSASHotkeyAction(
					new KeyBinding(), VesselAutopilot.AutopilotMode.AntiTarget));
			HotkeyManager.MainManager.Add("SAS Hotkeys", "Hold maneuver node",
				SASHotkeyAction.CreateSASHotkeyAction(
					new KeyBinding(), VesselAutopilot.AutopilotMode.Maneuver));
		}


	}
}

