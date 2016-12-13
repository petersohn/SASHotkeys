/*
Copyright (C) 2016  Péter Szabados

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

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
			HotkeyManager.HotkeyManager.MainManager.Add("SAS Hotkeys", "Enable SAS",
				new HotkeyAction (SASHotkeyAction.EnableSAS, true, new KeyBinding()));
			HotkeyManager.HotkeyManager.MainManager.Add("SAS Hotkeys", "Disable SAS",
				new HotkeyAction (SASHotkeyAction.DisableSAS, true, new KeyBinding()));
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

