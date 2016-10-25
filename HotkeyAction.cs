using System;
using UnityEngine;

namespace SASHotkeys
{
	public class HotkeyAction {
		public delegate void Action();

		public HotkeyAction(Action action, KeyBinding keyBinding = null)
		{
			this.keyBinding = keyBinding;
			this.action = action;
		}

		internal KeyBinding KeyBinding
		{
			get { return keyBinding; }
			set { keyBinding = value; }
		}

		internal void Fire() {
			bool state = keyBinding.GetKey ();
			if (state && !lastState) {
				action ();
			}
			lastState = state;
		}

		internal void Load(ConfigNode node) {
			if (keyBinding == null) {
				keyBinding = new KeyBinding ();
			}
			keyBinding.Load (node);
		}

		internal void Save(ConfigNode node) {
			keyBinding.Save (node);
		}

		KeyBinding keyBinding;
		bool lastState = false;
		Action action;
	}

	internal class SASHotkeyAction {
		static internal HotkeyAction CreateSASHotkeyAction(KeyBinding keyBinding, VesselAutopilot.AutopilotMode mode)
		{
			return new HotkeyAction (new SASHotkeyAction (mode).GetAction (), keyBinding);
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

