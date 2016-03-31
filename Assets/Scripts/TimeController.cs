using UnityEngine;
using System.Collections;

using UnityStandardAssets.ImageEffects;

public class TimeController : MonoBehaviour {

    public static TimeController instance;

    public static void SlowDownTime() {
        if (instance && instance.timeSlow == false) {
            instance.timeSlow = true;
            instance.StartCoroutine(instance.returnToNormal());
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

        motionBlur.blurAmount = Mathf.Lerp(0, 0.8f, scale);
        Time.timeScale = Mathf.Lerp(1, 0.2f, scale);
    }

    private IEnumerator returnToNormal() {
        yield return new WaitForSeconds(0.3f);
        timeSlow = false;
    }
}
