using UnityEngine;
using System.Collections;

public class SfxManager : MonoBehaviour {

    private static SfxManager instance;

    public GameObject hurtSoundEffect;
    public GameObject missileSpawn;
    public GameObject ballHitPlayer;
    public GameObject ballHitWall;
    public GameObject pinkSpecial;
    public GameObject pinkUltimate;
    public GameObject blueSpecial;
    public GameObject blueUltimate;
    public GameObject ultimateCharged;
    public GameObject uiSwitch;
    public GameObject playerDeath;
    public GameObject getPoint;

    public static void PlaySfxHurt() {
        if (instance) {
            Instantiate(instance.hurtSoundEffect);
        }
    }
    public static void PlaySfxMissileSpawn() {
        if (instance) {
            Instantiate(instance.missileSpawn);
        }
    }
    public static void PlaySfxBallHitPlayer() {
        if (instance) {
            Instantiate(instance.ballHitPlayer);
        }
    }
    public static void PlaySfxBallHitWall(float volume = 1) {
        if (instance) {
            GameObject sfx = (GameObject)Instantiate(instance.ballHitWall);
            if (volume < 1) {
                sfx.GetComponent<AudioSource>().volume = volume;
            }
        }
    }

    public static void PlaySfxPinkSpecial() {
        if (instance) {
            Instantiate(instance.pinkSpecial);
        }
    }
    public static void PlaySfxPinkUltimate() {
        if (instance) {
            Instantiate(instance.pinkUltimate);
        }
    }
    public static void PlaySfxBlueSpecial() {
        if (instance) {
            Instantiate(instance.blueSpecial);
        }
    }
    public static void PlaySfxBlueUltimate() {
        if (instance) {
            Instantiate(instance.blueUltimate);
        }
    }

    public static void PlaySfxUltimateCharged() {
        if (instance) {
            Instantiate(instance.ultimateCharged);
        }
    }

    public static void PlaySfxUiSwitch() {
        if (instance) {
            Instantiate(instance.uiSwitch);
        }
    }

    public static void PlaySfxPlayerDeath() {
        if (instance) {
            Instantiate(instance.playerDeath);
        }
    }

    public static void PlaySfxGetPoint() {
        if (instance) {
            Instantiate(instance.getPoint);
        }
    }

    void Start() {
        instance = this;
    }
}
