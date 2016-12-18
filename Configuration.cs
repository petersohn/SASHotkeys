using System;
using UnityEngine;

namespace SASHotkeys
{
	public class Configuration
	{
		public static Configuration Instance {
			get {
				if (instance == null) {
					instance = new Configuration ();
					instance.Load ();
				}
				return instance;	
			}	
		}

		private static Configuration instance;

		public bool AutoEnable = true;
		public bool ContinuousTrigger = false;

		const string autoEnableName = "autoEnable";
		const string continuousTriggerName = "continuousTrigger";
		static readonly string saveFileName = KSPUtil.ApplicationRootPath + "GameData/SASHotkeys/Settings.cfg";

		public void Load ()
		{
			Debug.Log (Constants.logPrefix + "Loading.");
			configFileNode = ConfigNode.Load (saveFileName);
			if (configFileNode == null) {
				Debug.Log (Constants.logPrefix + "Config file not found. Creating empty config.");
				configFileNode = new ConfigNode ();
			}

			if (configFileNode.HasValue (autoEnableName)) {
				configFileNode.TryGetValue (autoEnableName, ref AutoEnable);
			}
			if (configFileNode.HasValue (continuousTriggerName)) {
				configFileNode.TryGetValue (continuousTriggerName, ref ContinuousTrigger);
			}
		}

		public void Save ()
		{
			Debug.Log (Constants.logPrefix + "Saving.");
			if (configFileNode == null) {
				Debug.Log (Constants.logPrefix + "Configuration not yet loaded. Not saving.");
				return;
			}
			configFileNode.SetValue (autoEnableName, AutoEnable, true);
			configFileNode.SetValue (continuousTriggerName, ContinuousTrigger, true);
			configFileNode.Save (saveFileName);
		}

		private static ConfigNode GetOrCreateNode(ConfigNode node, string name)
		{
			ConfigNode result = node.GetNode (name);
			if (result == null) {
				result = new ConfigNode ();
				node.AddNode (name, result);
			}
			return result;
		}

		private ConfigNode configFileNode;
	}
}

