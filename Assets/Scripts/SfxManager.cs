﻿using UnityEngine;
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
    public static void PlaySfxBallHitWall() {
        if (instance) {
            Instantiate(instance.ballHitWall);
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

    void Start() {
        instance = this;
    }
}