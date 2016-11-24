using System;
using System.Collections.Generic;
using UnityEngine;

namespace SASHotkeys
{
	public class HotkeyManager
	{
		static public HotkeyManager MainManager {
			get {
				if (mainInstance == null) {
					mainInstance = new HotkeyManager ();
				}
				return mainInstance;
			}	
		}

		static private HotkeyManager mainInstance;

		public HotkeyManager ()
		{
		}

		public void Fire()
		{
			foreach (HotkeyAction hotkeyAction in hotkeyActions.Values) {
				hotkeyAction.Fire ();
			}
		}

		public void Add(String name, HotkeyAction hotkeyAction)
		{
			if (!hotkeyActions.ContainsKey (name)) {
				hotkeyActions.Add (name, hotkeyAction);
			}
		}

		public void Remove(String name)
		{
			hotkeyActions.Remove (name);
		}

		public Dictionary<String, HotkeyAction>.Enumerator GetEnumerator()
		{
			return hotkeyActions.GetEnumerator ();
		}

		public int Count { get { return hotkeyActions.Count; } }

		public void Save(ConfigNode node)
		{
			foreach (KeyValuePair<String, HotkeyAction> element in hotkeyActions) {
				Debug.Log (Constants.logPrefix + "Saving hotkey: " + element.Key + ": " +
					element.Value.KeyBinding.name);
				ConfigNode keyNode = new ConfigNode ();
				element.Value.Save (keyNode);
				node.AddNode (element.Key, keyNode);
			}
		}

		public void Load(ConfigNode node)
		{
			foreach (KeyValuePair<String, HotkeyAction> element in hotkeyActions) {
				Debug.Log (Constants.logPrefix + "Loading hotkey: " + element.Key);
				ConfigNode keyNode = node.GetNode (element.Key);
				if (keyNode != null) {
					element.Value.Load (keyNode);
				}
				Debug.Log (Constants.logPrefix + "Loaded hotkey: " + element.Value.KeyBinding.name);
			}
		}

		private Dictionary<String, HotkeyAction> hotkeyActions = new Dictionary<string, HotkeyAction>();
	}
}

