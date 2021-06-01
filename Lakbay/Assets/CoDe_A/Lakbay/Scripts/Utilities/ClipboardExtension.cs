using UnityEngine;

// namespace CoDe_A.Lakbay.Utilities {
    // Source: https://thatfrenchgamedev.com/785/unity-2018-how-to-copy-string-to-clipboard/
    public static class ClipboardExtension {
        /// <summary>
        /// Puts the string into the Clipboard.
        /// </summary>
        public static void CopyToClipboard(this string str) {
            GUIUtility.systemCopyBuffer = str;

        }

    }

// }