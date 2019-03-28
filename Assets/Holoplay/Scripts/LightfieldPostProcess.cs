//Copyright 2017-2019 Looking Glass Factory Inc.
//All rights reserved.
//Unauthorized copying or distribution of this file, and the source code contained herein, is strictly prohibited.

using UnityEngine;

namespace LookingGlass {
	[ExecuteInEditMode]
	public class LightfieldPostProcess : MonoBehaviour {

		public Holoplay holoplay;
		public bool lwrp;

		void OnRenderImage(RenderTexture src, RenderTexture dest) {
			Graphics.Blit(holoplay.quiltRT, dest, holoplay.lightfieldMat);
			// Graphics.Blit(Holoplay.quiltRT, dest);
		}

		// just a little hack to get lwrp support working
		void OnGUI() {
			if (!lwrp) return;
			var oldRt = RenderTexture.active; 
			var tempRT = RenderTexture.GetTemporary(holoplay.cal.screenWidth, holoplay.cal.screenHeight);
			Graphics.Blit(holoplay.quiltRT, tempRT, holoplay.lightfieldMat);
			RenderTexture.active = oldRt;
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), tempRT);
			tempRT.Release();
		}
	}
}
