using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KSP.UI.Screens;

namespace SASHotkeys
{
	[KSPAddon (KSPAddon.Startup.SpaceCentre, false)]
	public class SpaceCentreBehaviour : MonoBehaviour
	{
		public SpaceCentreBehaviour ()
		{
		}

		public void Awake()
		{
			SASHotkeys.InitializeHotkeyManager ();
		}

		public void Start()
		{
			SASHotkeys.LoadHotkeys ();
			if (toolbarButton == null) {
				toolbarButton = ApplicationLauncher.Instance.AddModApplication (
					OnToolbarOn, OnToolbarOff, null, null, null, null,
					ApplicationLauncher.AppScenes.SPACECENTER,
					GameDatabase.Instance.GetTexture ("SASHotkeys/toolbar", false));
			}
		}

		public void OnDestroy()
		{
			ApplicationLauncher.Instance.RemoveModApplication (toolbarButton);
			SASHotkeys.SaveHotkeys ();
		}

		public void OnGUI()
		{
			if (settingsWindowVisible) {
				GUILayout.Window(1, settingsWindowPosition,
					DrawSettingsWindow, "SAS Hotkeys");
			}
		}

		private void DrawSettingsWindow(int windowID)
		{
			if (currentAction != null) {
				foreach (KeyBinding keyBinding in AllKeyBindings) {
					if (keyBinding.GetKey ()) {
						currentAction.KeyBinding = keyBinding;
						currentAction = null;
						break;
					}
				}
			}
			GUILayout.BeginVertical ();
			foreach (var element in HotkeyManager.MainManager) {
				GUILayout.BeginHorizontal ();
				GUILayout.Label (element.Key);
				DrawSelectorButton (element.Value);
				if (GUILayout.Button ("Clear")) {
					element.Value.KeyBinding = new KeyBinding ();
				}
				GUILayout.EndHorizontal ();
			}
			GUILayout.EndVertical ();
		}

		private void DrawSelectorButton(HotkeyAction hotkeyAction)
		{
			if (currentAction == hotkeyAction) {
				if (GUILayout.Button ("...")) {
					currentAction = null;
				}
			} else {
				if (GUILayout.Button (hotkeyAction.KeyBinding.name)) {
					currentAction = hotkeyAction;
				}
			}
		}

		private void OnToolbarOn()
		{
			Debug.Log (Constants.logPrefix + "Toolbar switched on.");
			settingsWindowVisible = true;
		}

		private void OnToolbarOff()
		{
			Debug.Log (Constants.logPrefix + "Toolbar switched off.");
			SASHotkeys.SaveHotkeys ();
			settingsWindowVisible = false;
		}

		private List<KeyBinding> AllKeyBindings
		{
			get {
				if (allKeyBindings == null) {
					var values = Enum.GetValues (typeof(KeyCode)).Cast<KeyCode> ();
					allKeyBindings = new List<KeyBinding> ();
					foreach (KeyCode key in values) {
						String keyName = key.ToString ();
						// JoystickButtonX refers to buttons for any joystick, so filter them out.
						if (!keyName.StartsWith ("JoystickButton") && !keyName.StartsWith("Mouse")) {
							allKeyBindings.Add (new KeyBinding (key));
						}
					}
				}
				return allKeyBindings;
			}
		}

		private List<KeyBinding> allKeyBindings;
		private bool settingsWindowVisible = false;
		private Rect settingsWindowPosition = new Rect (Screen.width / 2 - 100, Screen.height / 2 - 200, 200, 400);
		private ApplicationLauncherButton toolbarButton;
		private HotkeyAction currentAction;
	}
}

