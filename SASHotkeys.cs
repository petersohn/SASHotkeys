using System;
using UnityEngine;
using HotkeyManager;

namespace SASHotkeys
{
	[KSPAddon (KSPAddon.Startup.SpaceCentre, false)]
	internal class SASHotkeys : MonoBehaviour
	{
		public void Awake()
		{
			InitializeHotkeys ();
		}

		private static void InitializeHotkeys()
		{
			HotkeyManager.HotkeyManager.MainManager.Add("SAS Hotkeys", "Stability assist",
				SASHotkeyAction.CreateSASHotkeyAction(
					new KeyBinding(), VesselAutopilot.AutopilotMode.StabilityAssist));
			HotkeyManager.HotkeyManager.MainManager.Add("SAS Hotkeys", "Hold propagade",
				SASHotkeyAction.CreateSASHotkeyAction(
					new KeyBinding(), VesselAutopilot.AutopilotMode.Prograde));
			HotkeyManager.HotkeyManager.MainManager.Add("SAS Hotkeys", "Hold retrograde",
				SASHotkeyAction.CreateSASHotkeyAction(
					new KeyBinding(), VesselAutopilot.AutopilotMode.Retrograde));
			HotkeyManager.HotkeyManager.MainManager.Add("SAS Hotkeys", "Hold normal",
				SASHotkeyAction.CreateSASHotkeyAction(
					new KeyBinding(), VesselAutopilot.AutopilotMode.Normal));
			HotkeyManager.HotkeyManager.MainManager.Add("SAS Hotkeys", "Hold anti-normal",
				SASHotkeyAction.CreateSASHotkeyAction(
					new KeyBinding(), VesselAutopilot.AutopilotMode.Antinormal));
			HotkeyManager.HotkeyManager.MainManager.Add("SAS Hotkeys", "Hold radial in",
				SASHotkeyAction.CreateSASHotkeyAction(
					new KeyBinding(), VesselAutopilot.AutopilotMode.RadialIn));
			HotkeyManager.HotkeyManager.MainManager.Add("SAS Hotkeys", "Hold radial out",
				SASHotkeyAction.CreateSASHotkeyAction(
					new KeyBinding(), VesselAutopilot.AutopilotMode.RadialOut));
			HotkeyManager.HotkeyManager.MainManager.Add("SAS Hotkeys", "Hold target",
				SASHotkeyAction.CreateSASHotkeyAction(
					new KeyBinding(), VesselAutopilot.AutopilotMode.Target));
			HotkeyManager.HotkeyManager.MainManager.Add("SAS Hotkeys", "Hold anti-target",
				SASHotkeyAction.CreateSASHotkeyAction(
					new KeyBinding(), VesselAutopilot.AutopilotMode.AntiTarget));
			HotkeyManager.HotkeyManager.MainManager.Add("SAS Hotkeys", "Hold maneuver node",
				SASHotkeyAction.CreateSASHotkeyAction(
					new KeyBinding(), VesselAutopilot.AutopilotMode.Maneuver));
		}


	}
}

