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
			SASHotkeys.Load ();
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
			SASHotkeys.Save ();
		}

		public void OnGUI()
		{
			if (settingsWindowVisible) {
				SASHotkeys.settingsWindowPosition = GUILayout.Window(1, SASHotkeys.settingsWindowPosition,
					DrawSettingsWindow, "SAS Hotkeys", HighLogic.Skin.window);
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
			settingsScrollPosition = GUILayout.BeginScrollView (settingsScrollPosition, false, true);
			foreach (var element in HotkeyManager.MainManager) {
				GUILayout.BeginHorizontal ();
				GUILayout.Label (element.Key);
				DrawSelectorButton (element.Value);
				if (GUILayout.Button (clearButtonText, ClearButtonWidth)) {
					element.Value.KeyBinding = new KeyBinding ();
				}
				GUILayout.EndHorizontal ();
			}
			GUILayout.EndScrollView ();
			GUI.DragWindow ();
		}

		private void DrawSelectorButton(HotkeyAction hotkeyAction)
		{
			if (currentAction == hotkeyAction) {
				if (GUILayout.Button ("...", HotkeyButtonWidth)) {
					currentAction = null;
				}
			} else {
				if (GUILayout.Button (hotkeyAction.KeyBinding.name, HotkeyButtonWidth)) {
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
			SASHotkeys.Save ();
			settingsWindowVisible = false;
		}

		private void CalculateKeyBindings()
		{
			var values = Enum.GetValues (typeof(KeyCode)).Cast<KeyCode> ();
			allKeyBindings = new List<KeyBinding> ();
			float maxWidth = 0.0f;
			foreach (KeyCode key in values) {
				String keyName = key.ToString ();
				// JoystickButtonX refers to buttons for any joystick, so filter them out.
				if (!keyName.StartsWith ("JoystickButton") && !keyName.StartsWith("Mouse")) {
					allKeyBindings.Add (new KeyBinding (key));
					maxWidth = Math.Max(maxWidth, GUI.skin.button.CalcSize(new GUIContent(keyName)).x);
				}
			}
			hotkeyButtonWidth = GUILayout.Width(maxWidth + buttonWidthThreshold);
		}

		private List<KeyBinding> AllKeyBindings
		{
			get {
				if (allKeyBindings == null) {
					CalculateKeyBindings ();
				}
				return allKeyBindings;
			}
		}
	
		private GUILayoutOption HotkeyButtonWidth
		{
			get {
				if (hotkeyButtonWidth == null) {
					CalculateKeyBindings ();
				}
				return hotkeyButtonWidth;
			}
		}

		private GUILayoutOption ClearButtonWidth
		{
			get {
				if (clearButtonWidth == null) {
					clearButtonWidth = GUILayout.Width(
						GUI.skin.button.CalcSize (new GUIContent (clearButtonText)).x + buttonWidthThreshold);
				}
				return clearButtonWidth;
			}
		}

		private const float buttonWidthThreshold = 10.0f;
		private const String clearButtonText = "Clear";

		private List<KeyBinding> allKeyBindings;
		private GUILayoutOption hotkeyButtonWidth;
		private GUILayoutOption clearButtonWidth;
		private bool settingsWindowVisible = false;
		private Vector2 settingsScrollPosition = new Vector2 (0, 0);
		private ApplicationLauncherButton toolbarButton;
		private HotkeyAction currentAction;
	}
}

