using UnityEngine;
using System.Collections;

using UnityStandardAssets.ImageEffects;

public class TimeController : MonoBehaviour {

    public static TimeController instance;

    public static void SlowDownTime(float time = 0.15f) {
        if (instance && instance.timeSlow == false) {
            instance.timeSlow = true;
            instance.StopAllCoroutines();
            instance.StartCoroutine(instance.returnToNormal(time));
        }
    }

    private MotionBlur motionBlur;

    private float scale;

    private bool timeSlow;

    void Start() {
        instance = this;
        motionBlur = GetComponent<MotionBlur>();
    }

    void Update() {
        scale = Mathf.Lerp(scale, timeSlow ? 1 : 0, Time.deltaTime * (timeSlow ? 15 : 5));

        motionBlur.blurAmount = Mathf.Lerp(0, 0.9f, scale);
        Time.timeScale = Mathf.Lerp(1, 0.2f, scale);
    }

    private IEnumerator returnToNormal(float delay) {
        yield return new WaitForSeconds(delay);
        timeSlow = false;
    }
}
