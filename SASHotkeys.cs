using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace SASHotkeys
{
	internal class Constants {
		public const string logPrefix = "<SASHotkeys>";
	}

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
		static readonly string saveFileName = KSPUtil.ApplicationRootPath + "GameData/SASHotkeys_settings.cfg";

		public override IList ValidValues (MemberInfo member)
		{
			ArrayList result = new ArrayList ();
			result.Add ("null");
			foreach (object keyCode in Enum.GetValues(typeof(KeyCode))) {
				result.Add (keyCode.ToString ());
			}
			return result;
		}

		void CreateKeyBindings()
		{
			if (Main.Instance != null) {
				Debug.Log (Constants.logPrefix + "Creating key bindings.");
				Main.Instance.holdPropagade = createKeyBinding (someValue);
			}
		}
			
		public override void OnLoad (ConfigNode node)
		{
			Debug.Log (Constants.logPrefix + "Loading from file.");
			configFileNode = ConfigNode.Load (saveFileName);
			if (configFileNode == null) {
				Debug.Log (Constants.logPrefix + "Config file does not exist, creating new one.");
				configFileNode = new ConfigNode ();
			}

			ConfigNode baseNode = GetOrCreateNode (configFileNode, "SASHotkeys");
			hotkeysNode = GetOrCreateNode (baseNode, "hotkeys");
			hotkeysNode.TryGetValue (valueName, ref someValue);
			CreateKeyBindings ();
		}

		public override void OnSave (ConfigNode node)
		{
			if (configFileNode == null) {
				Debug.Log (Constants.logPrefix + "SASHotkeys not initialized yet, not saving.");
				return;
			}
			Debug.Log (Constants.logPrefix + "Saving SAS Hotkeys");
			hotkeysNode.SetValue (valueName, someValue, true);
			configFileNode.Save (saveFileName);
			CreateKeyBindings ();
		}

		static ConfigNode GetOrCreateNode(ConfigNode node, string name)
		{
			ConfigNode result = node.GetNode (name);
			if (result == null) {
				result = new ConfigNode ();
				node.AddNode (name, result);
			}
			return result;
		}

		static KeyState createKeyBinding (string code)
		{
			try {
				return new KeyState(new KeyBinding ((KeyCode)Enum.Parse (typeof(KeyCode), code)));
			} catch (ArgumentException) {
				return null;
			}	
		}
			
		ConfigNode configFileNode;
		ConfigNode hotkeysNode;
	}

	[KSPAddon (KSPAddon.Startup.MainMenu, true)]
	public class Main : MonoBehaviour
	{
		public static Main Instance { get; private set; }

		void Awake ()
		{
			Instance = this;
		}

		internal KeyState holdPropagade;
	}

	internal class KeyState {
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
		void Update ()
		{
			Vessel activeVessel = FlightGlobals.ActiveVessel;
			if (Main.Instance.holdPropagade != null &&
					Main.Instance.holdPropagade.isPressed ()) {
				activeVessel.Autopilot.SetMode (VesselAutopilot.AutopilotMode.Prograde);
			}
		}
	}
}