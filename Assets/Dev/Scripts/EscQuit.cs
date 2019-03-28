using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LookingGlass.Extras {
	public class EscQuit : MonoBehaviour {

		void Update() {
			if (Input.GetKeyDown(KeyCode.Escape))
				Application.Quit();
		}
	}
}