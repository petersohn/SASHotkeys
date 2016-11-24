using System;
using UnityEngine;

namespace SASHotkeys
{
	internal class SASHotkeys
	{
		internal static void InitializeHotkeyManager()
		{
			HotkeyManager.MainManager.Add("stabilityAssist",
				SASHotkeyAction.CreateSASHotkeyAction(
					new KeyBinding(), VesselAutopilot.AutopilotMode.StabilityAssist));
			HotkeyManager.MainManager.Add("holdPropagade",
				SASHotkeyAction.CreateSASHotkeyAction(
					new KeyBinding(), VesselAutopilot.AutopilotMode.Prograde));
			HotkeyManager.MainManager.Add("holdRetrograde",
				SASHotkeyAction.CreateSASHotkeyAction(
					new KeyBinding(), VesselAutopilot.AutopilotMode.Retrograde));
			HotkeyManager.MainManager.Add("holdNormal",
				SASHotkeyAction.CreateSASHotkeyAction(
					new KeyBinding(), VesselAutopilot.AutopilotMode.Normal));
			HotkeyManager.MainManager.Add("holdAntiNormal",
				SASHotkeyAction.CreateSASHotkeyAction(
					new KeyBinding(), VesselAutopilot.AutopilotMode.Antinormal));
			HotkeyManager.MainManager.Add("holdRadialIn",
				SASHotkeyAction.CreateSASHotkeyAction(
					new KeyBinding(), VesselAutopilot.AutopilotMode.RadialIn));
			HotkeyManager.MainManager.Add("holdRadialOut",
				SASHotkeyAction.CreateSASHotkeyAction(
					new KeyBinding(), VesselAutopilot.AutopilotMode.RadialOut));
			HotkeyManager.MainManager.Add("holdTarget",
				SASHotkeyAction.CreateSASHotkeyAction(
					new KeyBinding(), VesselAutopilot.AutopilotMode.Target));
			HotkeyManager.MainManager.Add("holdAntiTarget",
				SASHotkeyAction.CreateSASHotkeyAction(
					new KeyBinding(), VesselAutopilot.AutopilotMode.AntiTarget));
			HotkeyManager.MainManager.Add("holdNode",
				SASHotkeyAction.CreateSASHotkeyAction(
					new KeyBinding(), VesselAutopilot.AutopilotMode.Maneuver));
		}

		static readonly string saveFileName = KSPUtil.ApplicationRootPath + "GameData/SASHotkeys_settings.cfg";

		internal static void LoadHotkeys()
		{
			Debug.Log (Constants.logPrefix + "Loading from file.");
			ConfigNode configFileNode = ConfigNode.Load (saveFileName);
			if (configFileNode == null) {
				Debug.LogWarning (Constants.logPrefix + "Config file does not exist, using empty config.");
				configFileNode = new ConfigNode ();
			}
				
			ConfigNode hotkeysNode = GetOrCreateNode (configFileNode, "hotkeys");
			HotkeyManager.MainManager.Load (hotkeysNode);
		}

		internal static void SaveHotkeys()
		{
			Debug.Log (Constants.logPrefix + "Saving to file.");

			ConfigNode baseNode = new ConfigNode();
			ConfigNode hotkeysNode = baseNode.AddNode ("hotkeys");
			HotkeyManager.MainManager.Save (hotkeysNode);
			baseNode.Save (saveFileName);
		}

		static ConfigNode GetOrCreateNode(ConfigNode node, string name)
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
	}
}

