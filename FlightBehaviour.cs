using System;
using System.Collections.Generic;
using UnityEngine;

namespace SASHotkeys
{
	[KSPAddon (KSPAddon.Startup.Flight, false)]
	public class FlightBehaviour : MonoBehaviour
	{
		void Update ()
		{
			Vessel activeVessel = FlightGlobals.ActiveVessel;
			if (GlobalStorage.Instance.holdPropagade != null &&
				GlobalStorage.Instance.holdPropagade.isPressed ()) {
				activeVessel.Autopilot.SetMode (VesselAutopilot.AutopilotMode.Prograde);
			}
		}
	}
}