using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class DarkenBackgroundUltimate : MonoBehaviour {

    public static float LastUltTime = -100;

    public Gradient backgroundColor;

    private Camera camera;

    private float targetColorTime;
    private float colorTime;

	// Use this for initialization
	void Start () {
        camera = GetComponent<Camera>();
        targetColorTime = targetColorTime = 1;
	}
	
	// Update is called once per frame
    void Update() {
        if (Time.time - LastUltTime < 8) {
            targetColorTime = 0;
        }
        else {
            targetColorTime = 1;
        }

        colorTime = Mathf.Lerp(colorTime, targetColorTime, Time.deltaTime * 10);

        camera.backgroundColor = backgroundColor.Evaluate(colorTime);
	}
}
