using System;
using UnityEngine;

namespace SASHotkeys
{
	public class SASHotkeysConfig : GameParameters.CustomParameterNode
	{
		public override string Title { get { return "Global options"; } }
		public override GameParameters.GameMode GameMode { get { return GameParameters.GameMode.ANY; } }
		public override bool HasPresets { get { return false; } }
		public override string Section { get { return "SAS Hotkeys"; } }
		public override int SectionOrder { get { return 1; } }

		[GameParameters.CustomParameterUI("Auto enable", autoPersistance = false,
			toolTip = "Automatically enable SAS if a SAS mode hotkey is pressed.")]
		public bool autoEnable = true;

		public override void OnLoad (ConfigNode node)
		{
			Debug.Log (Constants.logPrefix + "Loading GUI.");
			Configuration.Instance.Load ();
			loadVariables ();
			loaded = true;
		}

		public override void OnSave (ConfigNode node)
		{
			Debug.Log (Constants.logPrefix + "Saving GUI.");
			if (!loaded) {
				Debug.Log (Constants.logPrefix + "Not yet initialized. Not saving.");
				return;
			}
			saveVariables ();
			Configuration.Instance.Save ();
		}

		private void loadVariables()
		{
			autoEnable = Configuration.Instance.AutoEnable;
		}

		private void saveVariables()
		{
			Configuration.Instance.AutoEnable = autoEnable;
		}

		private bool loaded = false;
	}
}

