using System;
using UnityEngine;

namespace SASHotkeys
{
	internal class KeyState {
		internal static KeyState FromString(string value) {
			try {
				return new KeyState(new KeyBinding ((KeyCode)Enum.Parse (typeof(KeyCode), value)));
			} catch (ArgumentException) {
				return null;
			}
		}

		internal KeyState(KeyBinding keyBinding = null)
		{
			this.keyBinding = keyBinding;
		}

		internal KeyBinding KeyBinding { get { return keyBinding; } }

		internal bool isPressed() {
			bool state = keyBinding.GetKey ();
			bool result = state && !lastState;
			lastState = state;
			return result;
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
	}
}

