using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Text))]
public class SetScoreUI : MonoBehaviour {
    public int player;
    private Text text;

    void OnEnable() {
        text = GetComponent<Text>();

        if (player == 0) {
            text.text = "Player 1\n\nGoals Scored\n" + GameState.Player1BallsHit + "\n\nSHOTS FIRED\n" + GameState.Player1BulletsFired;
        }
        else if (player == 1) {
            text.text = "Player 2\n\nGoals Scored\n" + GameState.Player2BallsHit + "\n\nSHOTS FIRED\n" + GameState.Player2BulletsFired;
        }
    }
}
