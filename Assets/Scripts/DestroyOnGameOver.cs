using UnityEngine;
using System.Collections;

public class DestroyOnGameOver : MonoBehaviour {

    public GameObject destroyObject;

    float scale;
    void Start() {
        scale = 1;
    }

    void Update() {
        transform.localScale = new Vector3(scale, scale, 1);

        if (GameState.IsGameOver) {
            scale = Mathf.Lerp(scale, 0, Time.deltaTime * 5);

            if (scale < 0.01f) {
                Destroy(destroyObject);
            }
        }
    }
}
