using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HardModeUI : MonoBehaviour {

    public int player;

    void OnEnable() {
        Text text = GetComponent<Text>();
        text.enabled = false;
        if (player == 0) {
            if (GameState.WinCounter >= 3) {
                text.enabled = true;
                if (GameState.WinCounter > 3) {
                    text.text = " Hard Mode x" + (GameState.WinCounter - 2);
                }
                else {
                    text.text = " Hard Mode";
                }
            }
        }
        else if (player == 1) {
            if (GameState.WinCounter <= -3) {
                text.enabled = true;
                if (GameState.WinCounter < -3) {
                    text.text = "Hard Mode x" + (-GameState.WinCounter - 2) + " ";
                }
                else {
                    text.text = "Hard Mode ";
                }
            }
        }
    }
}
