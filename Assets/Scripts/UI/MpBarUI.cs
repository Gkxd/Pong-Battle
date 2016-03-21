using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MpBarUI : MonoBehaviour {
    public int player;

    private Color baseColor;
    private Image image;

    void Start() {
        image = GetComponent<Image>();
        baseColor = image.color;
    }

    void Update() {
        if (player == 0) {
            if (GameState.Player1Mp == 100) {
                image.color = Color.Lerp(baseColor, Color.white, (Mathf.Sin(Time.time * 10 * Mathf.PI) + 1) * 0.5f);
            }
            else if (GameState.Player1Mp >= 25) {
                image.color = Color.Lerp(baseColor, Color.white, (Mathf.Sin(Time.time * 2 * Mathf.PI) + 1) * 0.5f - 0.5f);
            }
            else {
                image.color = baseColor;
            }
        }
        else if (player == 1) {
            if (GameState.Player2Mp == 100) {
                image.color = Color.Lerp(baseColor, Color.white, (Mathf.Sin(Time.time * 10 * Mathf.PI) + 1) * 0.5f);
            }
            else if (GameState.Player2Mp >= 20) {
                image.color = Color.Lerp(baseColor, Color.white, (Mathf.Sin(Time.time * 2 * Mathf.PI) + 1) * 0.5f - 0.5f);
            }
            else {
                image.color = baseColor;
            }
        }
    }
}
