﻿using System;
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
			Debug.Log (Constants.logPrefix + "Creating key bindings.");
			GlobalStorage.Instance.holdPropagade = createKeyBinding (someValue);
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
}
