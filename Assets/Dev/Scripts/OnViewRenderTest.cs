using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LookingGlass.Extras {
	public class OnViewRenderTest : MonoBehaviour {

		public bool printView;
		string viewStr;

		public void PrintView(Holoplay hp, int view) {
			if (printView) {
				if (view == 0) {
					viewStr = "view:\n" + view + "\n";
				} else if (view < hp.quiltSettings.numViews) {
					viewStr += view + "\n";
				} else {
					viewStr += "finished";
					Debug.Log(viewStr);
					printView = false;
				}
			}
		}
	}
}