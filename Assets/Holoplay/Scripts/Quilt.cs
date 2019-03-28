//Copyright 2017-2019 Looking Glass Factory Inc.
//All rights reserved.
//Unauthorized copying or distribution of this file, and the source code contained herein, is strictly prohibited.

using System;
using UnityEngine;

namespace LookingGlass {
    public static class Quilt {
            
        // classes
        [Serializable]
        public struct Settings {
            [Range(256, 8192)] public int quiltWidth;
            [Range(256, 8192)] public int quiltHeight;
            [Range(64, 2048)] public int viewWidth;
            [Range(64, 2048)] public int viewHeight;
            [Range(1, 32)] public int viewRows;
            [Range(1, 32)] public int viewColumns;
            [Range(1, 128)] public int numViews;
            [System.NonSerialized] public int paddingHorizontal;
            [System.NonSerialized] public int paddingVertical;
            [System.NonSerialized] public float viewPortionHorizontal;
            [System.NonSerialized] public float viewPortionVertical;
            [Tooltip("To use the default aspect for the current Looking Glass, keep at -1")]
            public float aspect;
            [Tooltip("If custom aspect differs from current Looking Glass aspect, " +
                "this will toggle between overscan (zoom w/ crop) or letterbox (black borders)")]
            public bool overscan;

            public Settings(int viewWidth, int viewHeight, int numViews,
                int quiltWidth, int quiltHeight, int viewRows, 
                int viewColumns, float aspect = -1, bool overscan = false) : this() 
            {
                this.quiltWidth = quiltWidth;
                this.quiltHeight = quiltHeight;
                this.viewWidth = viewWidth;
                this.viewHeight = viewHeight;
                this.viewRows = viewRows;
                this.viewColumns = viewColumns;
                this.numViews = numViews;
                this.aspect = aspect;
                this.overscan = overscan;
                Setup(); 
            }
            public void Setup() {
                // viewRows = Mathf.Min(viewRows, quiltWidth);
                // viewColumns = Mathf.Min(viewColumns, quiltHeight);
                // numViews = Mathf.Max(numViews, viewRows);
                // numViews = Mathf.Max(numViews, viewColumns);
                // numViews = Mathf.Clamp(numViews, (viewColumns - 1) * viewRows, viewRows * viewColumns);
                // viewWidth = Mathf.Min(viewWidth, quiltWidth / viewRows);
                // viewHeight = Mathf.Min(viewHeight, quiltHeight / viewColumns);
                // viewRows = Mathf.Min(viewRows, numViews / viewColumns);
                // viewColumns = Mathf.Min(viewColumns, numViews / viewRows);
                viewPortionHorizontal = (float)viewRows * viewWidth / quiltWidth;
                viewPortionVertical = (float)viewColumns * viewHeight / quiltHeight;
                paddingHorizontal = quiltWidth - viewRows * viewWidth;
                paddingVertical = quiltHeight - viewColumns * viewHeight;
            }
            // todo: have an override that only takes view count, width, and height
            // and creates as square as possible quilt settings from that
        }
        public enum Preset {
            ExtraLow = 0,
            Standard = 1, 
            HiRes = 2, 
            UltraHi = 3,
            Automatic = -1,
            Custom = -2,
        }

        // variables
        public static readonly Settings[] presets = new Settings[] {
            new Settings(400, 240, 24, 1600, 1440, 4, 6), // extra low
            new Settings(512, 256, 32, 2048, 2048, 4, 8), // standard
            new Settings(819, 455, 45, 4096, 4096, 5, 9), // hi res
            new Settings(1280, 800, 48, 1280 * 6, 800 * 8, 6, 8), // ultra hi
        };

        // functions
        public static Settings GetPreset(Preset preset) {
            if (preset != Preset.Automatic) return presets[(int)preset];
            if (QualitySettings.lodBias > 2f) return presets[(int)Preset.UltraHi];
            if (QualitySettings.lodBias > 1f) return presets[(int)Preset.HiRes];
            if (QualitySettings.lodBias > 0.5f) return presets[(int)Preset.Standard];
            return presets[(int)Preset.ExtraLow];
        }
    }
}