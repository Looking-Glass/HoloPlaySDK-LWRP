//Copyright 2017-2019 Looking Glass Factory Inc.
//All rights reserved.
//Unauthorized copying or distribution of this file, and the source code contained herein, is strictly prohibited.

using System.Collections.Generic;
using UnityEngine;

namespace LookingGlass.Extras {
	public class FpsCounter : MonoBehaviour {

		public int queueSize = 15;
		
		Queue<float> times;
		float avgTime;

		void Start() {
			times = new Queue<float>();
		}

		void Update() {
			float time = Time.deltaTime * 1000f;
			times.Enqueue(time);
			if (times.Count > queueSize)
				times.Dequeue();
			avgTime = 0f;
			foreach(var t in times) {
				avgTime += t;
			}
			avgTime /= times.Count;
		}

		void OnGUI() {
			GUI.color = Color.white;
			var labelStyle = new GUIStyle(GUI.skin.label);
			labelStyle.fontSize = 50;
			labelStyle.alignment = TextAnchor.LowerRight;
			Rect rect = new Rect(20f, 20f, 300f, 200f);
			float fps = 1000f / avgTime;
			GUI.Label(rect, avgTime.ToString("##0.0") + "ms\n" + fps.ToString("##0.0") + "fps", labelStyle);
		}
	}
}