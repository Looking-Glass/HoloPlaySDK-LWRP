using UnityEngine;

namespace LookingGlass.Extras {
	public class CalibrationLoadTest : MonoBehaviour {

		void Start() {
			byte[] buf = new byte[8000];
			string calibrationStr;
			Plugin.ReadCalibrations(buf);
			calibrationStr = System.Text.Encoding.ASCII.GetString(buf);
			Debug.Log(calibrationStr);
		}

		public void LoadCalibrations(LoadResults results) {
			string resultsStr = "calibration attempted: " + results.attempted + "\n" +
				"loaded cal: " + results.calibrationFound + "\n" +
				"found lkg: " + results.lkgDisplayFound;
			Debug.Log(resultsStr);
			for (int i = 0; i < Plugin.CalibrationCount(); i++) {
				Calibration cal = new Calibration(i);
				// string serialName = "serial: " + cal.GetSerial() + "\n" +
				// 	"lkgName: " + cal.GetLKGName();
				// Debug.Log(serialName);
				string calStuff = "w:" + cal.screenWidth + "h:" + cal.screenHeight;
				calStuff += "model:" + cal.model;
				Debug.Log(calStuff);
			}
		}
	}
}