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
        if (transform.localScale.x >= 1) {
            image.color = Color.Lerp(baseColor, Color.white, (Mathf.Sin(Time.time * 10 * Mathf.PI) + 1) * 0.5f);
        }
        else {
            if (player == 0) {
                if (transform.localScale.x >= 0.25f) {
                    image.color = Color.Lerp(baseColor, Color.white, (Mathf.Sin(Time.time * 2 * Mathf.PI) + 1) * 0.5f - 0.5f);
                }
                else {
                    image.color = baseColor;
                }
            }
            else if (player == 1) {
                if (transform.localScale.x >= 0.2f) {
                    image.color = Color.Lerp(baseColor, Color.white, (Mathf.Sin(Time.time * 2 * Mathf.PI) + 1) * 0.5f - 0.5f);
                }
                else {
                    image.color = baseColor;
                }
            }
        }
    }
}
