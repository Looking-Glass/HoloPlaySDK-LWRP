//Copyright 2017-2019 Looking Glass Factory Inc.
//All rights reserved.
//Unauthorized copying or distribution of this file, and the source code contained herein, is strictly prohibited.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LookingGlass.Extras {
	public class MonitorTests : MonoBehaviour {

		public KeyCode[] displayKeys = new [] {
			KeyCode.Alpha1,
			KeyCode.Alpha2,
			KeyCode.Alpha3,
			KeyCode.Alpha4,
		};

		void Update() {
			
			int i = 0;
			foreach (var k in displayKeys) {
				if (Input.GetKeyDown(k) && Display.displays.Length > i) {
					Display.displays[i].Activate();
				}
				i++;	
			}
		}

		// void OnEnable() {
		// 	for (int i = 0; i < Display.displays.Length; i++) {
		// 		Display.displays[i].Activate();
		// 	}
		// }
	}
}
