using System;
using UnityEngine;

namespace SASHotkeys
{
	public class HotkeyAction : IConfigNode {
		public delegate void Action();

		public HotkeyAction(Action action, KeyBinding keyBinding = null)
		{
			this.keyBinding = keyBinding;
			this.action = action;
		}

		public KeyBinding KeyBinding
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

		public void Load(ConfigNode node) {
			if (keyBinding == null) {
				keyBinding = new KeyBinding ();
			}
			keyBinding.Load (node);
		}

		public void Save(ConfigNode node) {
			keyBinding.Save (node);
		}

		private KeyBinding keyBinding;
		private bool lastState = false;
		private Action action;
	}
}

