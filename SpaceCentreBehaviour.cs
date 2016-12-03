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
			Persistence.Load ();
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
			Persistence.Save ();
		}

		public void OnGUI()
		{
			if (settingsWindowVisible) {
				if (settingsWindow == null) {
					settingsWindow = new SettingsWindow (HotkeyManager.MainManager);
				}
				settingsWindow.Draw ();
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
			Persistence.Save ();
			settingsWindowVisible = false;
		}

		private bool settingsWindowVisible = false;
		private ApplicationLauncherButton toolbarButton;
		private SettingsWindow settingsWindow;
	}
}

