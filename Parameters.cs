using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace SASHotkeys
{
	public class Parameters : GameParameters.CustomParameterNode
	{
		public override string Title { get { return "SAS Hotkeys (effects are applied after scene change)"; } }

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

		public override void OnLoad (ConfigNode node)
		{
			Debug.Log (Constants.logPrefix + "Loading from file.");
			configFileNode = ConfigNode.Load (saveFileName);
			if (configFileNode == null) {
				Debug.LogWarning (Constants.logPrefix + "Config file does not exist, using empty config.");
				configFileNode = new ConfigNode ();
			}

			ConfigNode baseNode = GetOrCreateNode (configFileNode, "SASHotkeys");
			hotkeysNode = GetOrCreateNode (baseNode, "hotkeys");
			GlobalStorage.Instance.holdPropagade = LoadKeyState (hotkeysNode, valueName, out someValue);
		}

		public override void OnSave (ConfigNode node)
		{
			if (configFileNode == null) {
				Debug.Log (Constants.logPrefix + "SASHotkeys not initialized yet, not saving.");
				return;
			}
			Debug.Log (Constants.logPrefix + "Saving SAS Hotkeys");
			GlobalStorage.Instance.holdPropagade = SaveKeyState (hotkeysNode, valueName, someValue);
			configFileNode.Save (saveFileName);
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

		static KeyState LoadKeyState(ConfigNode node, string name, out string keyStateName)
		{
			ConfigNode keyStateNode = node.GetNode (name);
			if (keyStateNode == null) {
				keyStateName = "null";
				return null;
			}
			KeyState keyState = new KeyState ();
			keyState.Load (keyStateNode);
			keyStateName = keyState.KeyBinding.name;
			return keyState;
		}

		static KeyState SaveKeyState(ConfigNode node, string name, String keyStateName)
		{
			Debug.LogFormat (Constants.logPrefix + "Saving key {0}", keyStateName);
			KeyState keyState = KeyState.FromString (keyStateName);
			if (keyState != null) {
				ConfigNode keyStateNode = new ConfigNode ();
				keyState.Save (keyStateNode);
				node.SetNode (name, keyStateNode, true);
			} else {
				node.RemoveNodes (name);
			}
			return keyState;
		}
			
		ConfigNode configFileNode;
		ConfigNode hotkeysNode;
	}
}

