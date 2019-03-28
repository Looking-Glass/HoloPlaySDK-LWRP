//Copyright 2017-2019 Looking Glass Factory Inc.
//All rights reserved.
//Unauthorized copying or distribution of this file, and the source code contained herein, is strictly prohibited.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LookingGlass {
    public class ButtonTest : MonoBehaviour {

        public MeshRenderer[] buttons;
        public bool showGUI;
        Dictionary<ButtonType, string> buttonNames = new Dictionary<ButtonType, string>{
            { ButtonType.ONE, "Square / 1" },
            { ButtonType.TWO, "Left / 2" },
            { ButtonType.THREE, "Right / 3" },
            { ButtonType.FOUR, "Circle / 4" },
        };
        Color[] buttonColors = new Color[] {
            Color.HSVToRGB(0 / 8f, 0.7f, 1),
            Color.HSVToRGB(1 / 8f, 0.7f, 1),
            Color.HSVToRGB(2 / 8f, 0.7f, 1),
            Color.HSVToRGB(3 / 8f, 0.7f, 1),
        };

        void Update() {
            int i = 0;
            foreach (var b in buttonNames) {
                if (ButtonManager.GetButton(b.Key)) {
                    buttons[i].transform.localScale = Vector3.one * 1.2f;
                    buttons[i].material.color = buttonColors[i];
                } else {
                    float scale = buttons[i].transform.localScale.x;
                    buttons[i].transform.localScale = Vector3.one *
                        Mathf.MoveTowards(scale, 1f, Mathf.Max((scale - 1f) * Time.deltaTime / 0.1f, 0.001f));
                    buttons[i].material.color = Color.Lerp(buttonColors[i], Color.white, (1.2f - scale) / 0.2f);
                }
                i++;
            }
        }

        void OnGUI() {
            if (!showGUI) return;
            var boxStyle = new GUIStyle(GUI.skin.box);
            boxStyle.fontSize = 50;
            foreach (var b in buttonNames) {
                if (ButtonManager.GetButton(b.Key))
                    GUILayout.Box(b.Value, boxStyle);
            }
        }
    }
}
