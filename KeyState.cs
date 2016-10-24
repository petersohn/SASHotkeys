using System;

namespace SASHotkeys
{
	internal class KeyState {
		internal KeyState(KeyBinding keyBinding)
		{
			this.keyBinding = keyBinding;
		}

		internal bool isPressed() {
			bool state = keyBinding.GetKey ();
			bool result = state && !lastState;
			lastState = state;
			return result;
		}

		KeyBinding keyBinding;
		bool lastState = false;
	}
}

