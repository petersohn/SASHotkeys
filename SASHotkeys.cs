using System;
using System.Collections.Generic;
using UnityEngine;

namespace SASHotkeys
{
	[KSPAddon (KSPAddon.Startup.MainMenu, true)]
	public class Main : MonoBehaviour
	{
		public static Main Instance { get; private set; }

		void Awake ()
		{
			Instance = this;
		}

		internal KeyState holdPropagade;
	}

	internal class KeyState {
		public KeyState(KeyBinding keyBinding)
		{
			this.keyBinding = keyBinding;
		}

		public bool isPressed() {
			bool state = keyBinding.GetKey ();
			bool result = state && !lastState;
			lastState = state;
			return result;
		}

		KeyBinding keyBinding;
		bool lastState = false;
	}

	[KSPAddon (KSPAddon.Startup.Flight, false)]
	public class FlightBehaviour : MonoBehaviour
	{
		void Update ()
		{
			Vessel activeVessel = FlightGlobals.ActiveVessel;
			if (Main.Instance.holdPropagade != null &&
					Main.Instance.holdPropagade.isPressed ()) {
				activeVessel.Autopilot.SetMode (VesselAutopilot.AutopilotMode.Prograde);
			}
		}
	}
}