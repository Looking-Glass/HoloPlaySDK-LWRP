//Copyright 2017-2019 Looking Glass Factory Inc.
//All rights reserved.
//Unauthorized copying or distribution of this file, and the source code contained herein, is strictly prohibited.

using UnityEngine;

namespace LookingGlass.Extras {
	public class MovementTest : MonoBehaviour {

		public Vector3 rotationPerSecond;

		void Update () {
			transform.Rotate(rotationPerSecond * Time.deltaTime);	
		}
	}
}