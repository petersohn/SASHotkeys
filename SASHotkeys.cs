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

		[GameParameters.CustomIntParameterUI ("Hold Propagade", autoPersistance = false, toolTip = "Hold Propagade")]
		public string someValue = "null";

		const string configName = "SASHotkeysConfig";
		const string valueName = "someValue";

		public override IList ValidValues (MemberInfo member)
		{
			ArrayList result = new ArrayList ();
			result.Add ("null");
			foreach (object keyCode in Enum.GetValues(typeof(KeyCode))) {
				result.Add (keyCode.ToString ());
			}
			return result;
		}

		void Initialize ()
		{
			Debug.Log ("Initializing SASHotkeys parameters");

			foreach (UrlDir.UrlConfig urlConfig in GameDatabase.Instance.GetConfigs(configName)) {
				configFile = urlConfig.parent;
				configNode = urlConfig.config;
				break;
			}

			if (configNode == null) {
				Debug.LogError ("Could not find SASHotkeys config file. Configurations will not be saved.");
				return;
			}
		}

		public override void OnLoad (ConfigNode node)
		{
			if (configNode == null) {
				Initialize ();
				if (configNode == null) {
					return;
				}
			}
			Debug.Log ("Loading SAS Hotkeys");
			configNode.TryGetValue (valueName, ref someValue);

			if (Main.Instance != null) {
				Main.Instance.holdPropagade = createKeyBinding (someValue);
			}
		}

		public override void OnSave (ConfigNode node)
		{
			if (configNode == null) {
				return;
			}
			Debug.Log ("Saving SAS Hotkeys");
			configNode.SetValue (valueName, someValue, true);
			configFile.SaveConfigs ();
		}

		static KeyBinding createKeyBinding (string code)
		{
			try {
				return new KeyBinding ((KeyCode)Enum.Parse (typeof(KeyCode), code));
			} catch (ArgumentException) {
				return null;
			}	
		}

		ConfigNode configNode;
		UrlDir.UrlFile configFile;
	}

	[KSPAddon (KSPAddon.Startup.MainMenu, true)]
	public class Main : MonoBehaviour
	{
		public static Main Instance { get; private set; }

		void Awake ()
		{
			Instance = this;
		}

		public KeyBinding holdPropagade;
	}

	class KeyState {
		public KeyState(KeyBinding keyBinding)
		{
			this.keyBinding = keyBinding;
		}

		public bool isPressed() {
			bool state = keyBinding.GetKey ();
			bool result = state && !lastState;
			lastState = state;
			return result;
		}

		KeyBinding keyBinding;
		bool lastState = false;
	}

	[KSPAddon (KSPAddon.Startup.Flight, false)]
	public class FlightBehaviour : MonoBehaviour
	{
		void Awake()
		{
			holdPropagadeState = new KeyState (Main.Instance.holdPropagade);
		}

		void Update ()
		{
			Vessel activeVessel = FlightGlobals.ActiveVessel;
			if (holdPropagadeState.isPressed ()) {
				activeVessel.Autopilot.SetMode (VesselAutopilot.AutopilotMode.Prograde);
			}
		}

		KeyState holdPropagadeState;
	}
}