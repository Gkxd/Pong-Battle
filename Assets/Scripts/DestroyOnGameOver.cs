using UnityEngine;
using System.Collections;

public class DestroyOnGameOver : MonoBehaviour {

    public GameObject destroyObject;
    public TrailRenderer trail;

    private float trailWidth;

    private float scale;

    void Start() {
        scale = 1;
        if (trail) {
            trailWidth = trail.startWidth;
        }
    }

    void Update() {
        transform.localScale = new Vector3(scale, scale, 1);

        if (trail) {
            trail.startWidth = trailWidth * scale;
        }

        if (GameState.IsGameOver) {
            scale = Mathf.Lerp(scale, 0, Time.deltaTime * 5);

            if (scale < 0.01f) {
                Destroy(destroyObject);
            }
        }
    }
}
