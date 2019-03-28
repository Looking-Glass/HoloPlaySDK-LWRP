using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LookingGlass.Extras {
	public class LoadResultsTest : MonoBehaviour {

		public void TestMyShit(LoadResults results) {
			Debug.Log(string.Format(
				"load calibration? {0}\nfind display? {1}", 
				results.calibrationFound, results.lkgDisplayFound));
		}
	}
}