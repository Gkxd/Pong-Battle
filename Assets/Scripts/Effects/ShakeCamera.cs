using UnityEngine;
using System.Collections;

using UsefulThings;

public class ShakeCamera : MonoBehaviour {

    void Update() {
        transform.position = new Vector3(0, 0, -10) + CameraShake.CameraShakeOffset;
    }
}
