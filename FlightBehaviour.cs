using System;
using System.Collections.Generic;
using UnityEngine;

namespace SASHotkeys
{
	[KSPAddon (KSPAddon.Startup.Flight, false)]
	public class FlightBehaviour : MonoBehaviour
	{
		void Update ()
		{
			GlobalStorage.Instance.holdPropagade.Fire ();
		}
	}
}