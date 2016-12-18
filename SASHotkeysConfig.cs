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

		[GameParameters.CustomParameterUI("Continuous Trigger", autoPersistance = false,
			toolTip = "Continuously trigger the hotkey action while the button is pressed.")]
		public bool continuousTrigger = true;

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
			continuousTrigger = Configuration.Instance.ContinuousTrigger;
		}

		private void saveVariables()
		{
			Configuration.Instance.AutoEnable = autoEnable;
			if (Configuration.Instance.ContinuousTrigger != continuousTrigger) {
				Configuration.Instance.ContinuousTrigger = continuousTrigger;
				foreach (var element in HotkeyManager.HotkeyManager.MainManager.GetGroup(SASHotkeys.groupName)) {
					element.Value.EdgeTrigger = !continuousTrigger;
				}
			}
		}

		private bool loaded = false;
	}
}

