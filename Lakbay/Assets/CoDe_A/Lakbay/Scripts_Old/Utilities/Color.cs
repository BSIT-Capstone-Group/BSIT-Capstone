using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoDe_A_Old.Lakbay.Utilities {
    public static class Color {
        public static readonly UnityEngine.Color BLACK = create("#000000"); 
        public static readonly UnityEngine.Color WHITE = create("#FFFFFF"); 
        public static readonly UnityEngine.Color BLUE = create("blue"); 
        public static readonly UnityEngine.Color YELLOW = create("yellow"); 
        public static readonly UnityEngine.Color LIGHT_YELLOW = create("lightyellow"); 

        public static UnityEngine.Color create(string hex) {
            UnityEngine.Color color = new UnityEngine.Color();
            ColorUtility.TryParseHtmlString(hex, out color);
            return color;

        }

        public static UnityEngine.Color create(int red, int green, int blue, int alpha) {
            return new UnityEngine.Color32((byte) red, (byte) green, (byte) blue, (byte) alpha);

        }

        public static UnityEngine.Color create(int red, int green, int blue) {
            return create(red, green, blue, 255);

        }

    }

}
