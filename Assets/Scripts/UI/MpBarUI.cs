﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MpBarUI : MonoBehaviour {
    public int player;
    public bool helpScreen;

    private Color baseColor;
    private Image image;

    void Start() {
        image = GetComponent<Image>();
        baseColor = image.color;
    }

    void Update() {
        if (helpScreen) {
            baseColor = image.color;
        }

        if (transform.localScale.x >= 1) {
            image.color = Color.Lerp(baseColor, Color.white, (Mathf.Sin(Time.time * 10 * Mathf.PI) + 1) * 0.5f);
        }
        else {
            if (transform.localScale.x >= 0.2f) {
                image.color = Color.Lerp(baseColor, Color.white, (Mathf.Sin(Time.time * 2 * Mathf.PI) + 1) * 0.5f - 0.5f);
            }
            else {
                image.color = baseColor;
            }
        }
    }
}
