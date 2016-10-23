using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SASHotkeys
{
	public class Parameters : GameParameters.CustomParameterNode
	{
		public override string Title { get { return "SAS Hotkeys"; } }

		public override GameParameters.GameMode GameMode { get { return GameParameters.GameMode.ANY; } }

		public override bool HasPresets { get { return false; } }

		public override string Section { get { return "SAS Hotkeys"; } }

		public override int SectionOrder { get { return 1; } }

		[GameParameters.CustomIntParameterUI ("Some Value", minValue = -100, maxValue = 100, stepSize = 1, autoPersistance = false, toolTip = "Some value")]
		public int someValue = 0;

		const string configName = "SASHotkeysConfig";
		const string valueName = "someValue";

		public Parameters ()
		{
			Debug.Log ("Initializing SASHotkeys parameters");

			foreach (UrlDir.UrlConfig urlConfig in GameDatabase.Instance.GetConfigs(configName)) {
				configFileName = urlConfig.parent.fullPath;
				globalConfigNode = urlConfig.config;
				break;
			}

			if (globalConfigNode == null) {
				Debug.LogError ("Could not find config file. Configurations will not be saved.");
				return;
			}
		}

		public override void OnLoad (ConfigNode node)
		{
			Debug.Log ("Loading SAS Hotkeys");
			globalConfigNode.TryGetValue (valueName, ref someValue);
		}

		public override void OnSave (ConfigNode node)
		{
			Debug.Log ("Saving SAS Hotkeys");
			if (globalConfigNode.HasValue (valueName)) {
				globalConfigNode.SetValue (valueName, someValue);
			} else {
				globalConfigNode.AddValue (valueName, someValue);
			}
			globalConfigNode.Save (configFileName);
		}

		ConfigNode globalConfigNode;
		string configFileName;
	}

	[KSPAddon (KSPAddon.Startup.MainMenu, true)]
	public class MainMenuBehaviour : MonoBehaviour
	{
		void Awake ()
		{
		}
	}

	[KSPAddon (KSPAddon.Startup.Flight, false)]
	public class FlightBehaviour : MonoBehaviour
	{
		void Awake ()
		{
		}
	}
}