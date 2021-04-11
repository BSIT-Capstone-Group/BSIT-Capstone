using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {
    public static void pause() {
        Time.timeScale = 0.0f;
    }

    public static void resume() {
        Time.timeScale = 1.0f;
    }
}
