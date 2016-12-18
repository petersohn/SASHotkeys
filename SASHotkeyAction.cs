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
using HotkeyManager;

namespace SASHotkeys
{
	internal class SASHotkeyAction {
		internal static HotkeyAction CreateSASHotkeyAction(KeyBinding keyBinding, VesselAutopilot.AutopilotMode mode)
		{
			return new HotkeyAction (new SASHotkeyAction (mode).GetAction (), true, keyBinding);
		}

		internal static void EnableSAS()
		{
			FlightGlobals.ActiveVessel.ActionGroups.SetGroup(KSPActionGroup.SAS, true);
		}

		internal static void DisableSAS()
		{
			FlightGlobals.ActiveVessel.ActionGroups.SetGroup(KSPActionGroup.SAS, false);
		}

		internal SASHotkeyAction(VesselAutopilot.AutopilotMode mode)
		{
			this.mode = mode;
			this.configuration = Configuration.Instance;
		}

		internal HotkeyAction.Action GetAction()
		{
			return Fire;
		}

		private void Fire()
		{
			if (configuration.AutoEnable) {
				EnableSAS ();
			}
			FlightGlobals.ActiveVessel.Autopilot.Enable (mode);
		}

		private VesselAutopilot.AutopilotMode mode;
		private Configuration configuration;

	};
}

