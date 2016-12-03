using System;
using UnityEngine;

namespace SASHotkeys
{
	internal class Persistence
	{
		static readonly string saveFileName = KSPUtil.ApplicationRootPath + "GameData/SASHotkeys_settings.cfg";

		public static void Load()
		{
			Debug.Log (Constants.logPrefix + "Loading from file.");
			ConfigNode configFileNode = ConfigNode.Load (saveFileName);
			if (configFileNode == null) {
				Debug.LogWarning (Constants.logPrefix + "Config file does not exist, using empty config.");
				configFileNode = new ConfigNode ();
			}

			ConfigNode hotkeysNode = GetOrCreateNode (configFileNode, "hotkeys");
			HotkeyManager.MainManager.Load (hotkeysNode);

			ConfigNode settingsWindowNode = configFileNode.GetNode("settingsWindow");
			if (settingsWindowNode != null) {
				settingsWindowPosition.x = Convert.ToSingle(settingsWindowNode.GetValue ("x"));
				settingsWindowPosition.y = Convert.ToSingle(settingsWindowNode.GetValue ("y"));
			}
		}

		public static void Save()
		{
			Debug.Log (Constants.logPrefix + "Saving to file.");

			ConfigNode baseNode = new ConfigNode();
			ConfigNode hotkeysNode = baseNode.AddNode ("hotkeys");
			HotkeyManager.MainManager.Save (hotkeysNode);

			ConfigNode settingsWindowNode = baseNode.AddNode ("settingsWindow");
			settingsWindowNode.AddValue ("x", settingsWindowPosition.x);
			settingsWindowNode.AddValue ("y", settingsWindowPosition.y);

			baseNode.Save (saveFileName);
		}

		private static ConfigNode GetOrCreateNode(ConfigNode node, string name)
		{
			ConfigNode result = node.GetNode (name);
			if (result == null) {
				Debug.Log (Constants.logPrefix + "Node not found: " + name);
				result = new ConfigNode ();
				node.AddNode (name, result);
			} else {
				Debug.Log (Constants.logPrefix + "Node found: " + name);
			}
			return result;
		}


		public static Rect settingsWindowPosition = new Rect (
			(Screen.width - windowWidth) / 2, (Screen.height - windowHeight) / 2, windowWidth, windowHeight);

		private const int windowWidth = 400;
		private const int windowHeight = 500;
	}
}

