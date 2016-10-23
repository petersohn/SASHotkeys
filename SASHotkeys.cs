using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SASHotkeys
{
	[KSPAddon(KSPAddon.Startup.Flight, false)]
	public class SASHotkeysMod : MonoBehaviour
	{
		void Awake()
		{
			Debug.Log ("<<<< Awaking SasHotkeys mod >>>>");
		}
	}
}