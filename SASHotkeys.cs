using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
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

		[GameParameters.CustomIntParameterUI ("Some Value", autoPersistance = false, toolTip = "Some value")]
		public string someValue = "null";

		const string configName = "SASHotkeysConfig";
		const string valueName = "someValue";

		public override IList ValidValues(MemberInfo member)
		{
			ArrayList result = new ArrayList ();
			result.Add ("null");
			foreach (object keyCode in Enum.GetValues(typeof(KeyCode))) {
				result.Add(keyCode.ToString());
			}
			return result;
		}

		public Parameters ()
		{
			Debug.Log ("Initializing SASHotkeys parameters");

			foreach (UrlDir.UrlConfig urlConfig in GameDatabase.Instance.GetConfigs(configName)) {
				configFileName = urlConfig.parent.fullPath;
				globalConfigNode = urlConfig.config;
				break;
			}

			if (globalConfigNode == null) {
				Debug.LogError ("Could not find SASHotkeys config file. Configurations will not be saved.");
				return;
			}
		}

		public override void OnLoad (ConfigNode node)
		{
			if (globalConfigNode == null) {
				Debug.LogWarning ("SASHotkeys configuration file is missing.");
				return;
			}
			Debug.Log ("Loading SAS Hotkeys");
			globalConfigNode.TryGetValue (valueName, ref someValue);
		}

		public override void OnSave (ConfigNode node)
		{
			if (globalConfigNode == null) {
				Debug.LogWarning ("SASHotkeys configuration file is missing.");
				return;
			}
			Debug.Log ("Saving SAS Hotkeys");
			globalConfigNode.SetValue (valueName, someValue, true);
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