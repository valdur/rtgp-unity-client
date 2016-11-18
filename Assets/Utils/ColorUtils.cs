using UnityEngine;
using System.Collections;

public static class ColorUtils {

	public static float GetLuminosity(Color color) {
        return 0.299f * color.r + 0.587f * color.g + 0.114f * color.b;
    }

    public static Color GetTextColorForBackground(Color color) {
        return GetLuminosity(color) > .5f ? Color.black : Color.white;
    }
}
